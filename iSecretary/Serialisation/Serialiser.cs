using System;
using System.IO;
using System.Xml.Serialization;

namespace Serialisation
{
    public class Serialiser
    {
        public static void ObjectToXml(object obj, string targetFolder, string filename)
        {
            try
            {
                var ser = new XmlSerializer(obj.GetType());
                var fs = File.Open(targetFolder + filename, FileMode.Create, FileAccess.Write, FileShare.None);
                ser.Serialize(fs, obj);
                fs.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Could Not Serialize object to " + targetFolder, ex);
            }
        }
    }
}
