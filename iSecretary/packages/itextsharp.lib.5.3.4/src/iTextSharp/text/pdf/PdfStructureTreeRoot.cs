using System;
using System.Collections.Generic;
using iTextSharp.text.pdf.interfaces;

/*
 * $Id: PdfStructureTreeRoot.cs 475 2012-11-12 09:25:23Z eugenemark $
 *
 * This file is part of the iText project.
 * Copyright (c) 1998-2012 1T3XT BVBA
 * Authors: Bruno Lowagie, Paulo Soares, et al.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License version 3
 * as published by the Free Software Foundation with the addition of the
 * following permission added to Section 15 as permitted in Section 7(a):
 * FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY 1T3XT,
 * 1T3XT DISCLAIMS THE WARRANTY OF NON INFRINGEMENT OF THIRD PARTY RIGHTS.
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU Affero General Public License for more details.
 * You should have received a copy of the GNU Affero General Public License
 * along with this program; if not, see http://www.gnu.org/licenses or write to
 * the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
 * Boston, MA, 02110-1301 USA, or download the license from the following URL:
 * http://itextpdf.com/terms-of-use/
 *
 * The interactive user interfaces in modified source and object code versions
 * of this program must display Appropriate Legal Notices, as required under
 * Section 5 of the GNU Affero General Public License.
 *
 * In accordance with Section 7(b) of the GNU Affero General Public License,
 * you must retain the producer line in every PDF that is created or manipulated
 * using iText.
 *
 * You can be released from the requirements of the license by purchasing
 * a commercial license. Buying such a license is mandatory as soon as you
 * develop commercial activities involving the iText software without
 * disclosing the source code of your own applications.
 * These activities include: offering paid services to customers as an ASP,
 * serving PDFs on the fly in a web application, shipping iText with a closed
 * source product.
 *
 * For more information, please contact iText Software Corp. at this
 * address: sales@itextpdf.com
 */
namespace iTextSharp.text.pdf {

    /**
    * The structure tree root corresponds to the highest hierarchy level in a tagged PDF.
    * @author Paulo Soares
    */
    public class PdfStructureTreeRoot : PdfDictionary, IPdfStructureElement {
        
        private Dictionary<int, PdfObject> parentTree = new Dictionary<int,PdfObject>();
        private PdfIndirectReference reference;
        private PdfDictionary classMap;
        private Dictionary<PdfName, PdfObject> classes;

        /**
        * Holds value of property writer.
        */
        private PdfWriter writer;
        
        /** Creates a new instance of PdfStructureTreeRoot */
        internal PdfStructureTreeRoot(PdfWriter writer) : base(PdfName.STRUCTTREEROOT) {
            this.writer = writer;
            reference = writer.PdfIndirectReference;
        }
        
        /**
        * Maps the user tags to the standard tags. The mapping will allow a standard application to make some sense of the tagged
        * document whatever the user tags may be.
        * @param used the user tag
        * @param standard the standard tag
        */    
        public void MapRole(PdfName used, PdfName standard) {
            PdfDictionary rm = (PdfDictionary)Get(PdfName.ROLEMAP);
            if (rm == null) {
                rm = new PdfDictionary();
                Put(PdfName.ROLEMAP, rm);
            }
            rm.Put(used, standard);
        }
        
        public void MapClass(PdfName name, PdfObject obj) {
            if (classMap == null) {
                classMap = new PdfDictionary();
                classes = new Dictionary<PdfName, PdfObject>();
            }
            classes.Add(name,obj);
        }

        public PdfObject GetMappedClass(PdfName name) {
            if (classes == null)
                return null;
            return classes[name];
        }


        /**
        * Gets the writer.
        * @return the writer
        */
        public PdfWriter Writer {
            get {
                return this.writer;
            }
        }

        /**
        * Gets the reference this object will be written to.
        * @return the reference this object will be written to
        */    
        public PdfIndirectReference Reference {
            get {
                return this.reference;
            }
        }
        
        internal void SetPageMark(int page, PdfIndirectReference struc) {
            PdfArray ar;
            if (!parentTree.ContainsKey(page)) {
                ar = new PdfArray();
                parentTree[page] = ar;
            }
            else
                ar = (PdfArray)parentTree[page];
            ar.Add(struc);
        }
        
        internal void AddPageMark(int newPage, PdfIndirectReference struc) {
            int integer = newPage;
            PdfArray oldAr = (PdfArray)parentTree[integer];
            if (oldAr == null) {
                oldAr = new PdfArray();
                parentTree.Add(integer, oldAr);
                oldAr.Add(struc);
                return;
            }
            for (int i = 0; i < oldAr.Size; ++i) {
                PdfIndirectReference refer = (PdfIndirectReference)oldAr[i];
                if (refer.Number == struc.Number)
                    return;
            }
            oldAr.Add(struc);
        }

        private void NodeProcess(PdfDictionary struc, PdfIndirectReference reference) {
            PdfObject obj = struc.Get(PdfName.K);
            if (obj != null && obj.IsArray() && !((PdfArray)obj)[0].IsNumber()) {
                PdfArray ar = (PdfArray)obj;
                for (int k = 0; k < ar.Size; ++k) {
                    PdfDictionary dictionary = ar.GetAsDict(k);
                    if (dictionary == null)
                        continue;
                    if (!PdfName.STRUCTELEM.Equals(dictionary.Get(PdfName.TYPE)))
                        continue;
                    PdfStructureElement e = (PdfStructureElement)dictionary;
                    ar[k] = e.Reference;
                    NodeProcess(e, e.Reference);
                }
            }
            if (reference != null)
                writer.AddToBody(struc, reference);
        }
        
        internal void BuildTree() {
            Dictionary<int, PdfIndirectReference> numTree = new Dictionary<int,PdfIndirectReference>();
            foreach (int i in parentTree.Keys) {
                PdfArray ar = (PdfArray)parentTree[i];
                numTree[i] = writer.AddToBody(ar).IndirectReference;
            }
            PdfDictionary dicTree = PdfNumberTree.WriteTree(numTree, writer);
            if (dicTree != null)
                Put(PdfName.PARENTTREE, writer.AddToBody(dicTree).IndirectReference);
            if (classMap != null) {
                foreach (KeyValuePair<PdfName,PdfObject> entry in classes) {
                    PdfObject value = entry.Value;
                    if (value.IsDictionary())
                        classMap.Put(entry.Key, writer.AddToBody(value).IndirectReference);
                    else if (value.IsArray()) {
                        PdfArray newArray = new PdfArray();
                        PdfArray array = (PdfArray)value;
                        for (int i = 0; i < array.Size; ++i) {
                            if (array[i].IsDictionary())
                                newArray.Add(writer.AddToBody(array.GetAsDict(i)).IndirectReference);
                        }
                        classMap.Put(entry.Key,newArray);
                    }
                }
                Put(PdfName.CLASSMAP, writer.AddToBody(classMap).IndirectReference);
            }
            NodeProcess(this, reference);
        }


        /**
         * Gets the first entarance of attribute.
         * @returns PdfObject
         * @since 5.3.4
         */
        public PdfObject GetAttribute(PdfName name) {
            PdfDictionary attr = GetAsDict(PdfName.A);
            if (attr != null) {
                if (attr.Contains(name))
                    return attr.Get(name);
            }
            return null;
        }

        /**
         * Sets the attribute value.
         * @since 5.3.4
         */
        public void SetAttribute(PdfName name, PdfObject obj) {
            PdfDictionary attr = GetAsDict(PdfName.A);
            if (attr == null) {
                attr = new PdfDictionary();
                Put(PdfName.A, attr);
            }
            attr.Put(name, obj);
        }
    }
}
