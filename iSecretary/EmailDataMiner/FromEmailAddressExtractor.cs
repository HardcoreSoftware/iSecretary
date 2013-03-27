using System;
using System.Diagnostics;
using System.IO;

namespace EmailDataMiner
{
    public class FromEmailAddressExtractor
    {
        public static string Retrieve(string file,  out bool isGood)
        {
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();

            try
            {
                htmlDoc.Load(file);
            }
            catch (IOException)
            {
                Console.WriteLine("Please close any target CSV files.");
                isGood = false;
                return null;
            }

            if (htmlDoc.DocumentNode != null)
            {
                var bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

                if (bodyNode != null)
                {
                    var node = bodyNode.SelectSingleNode("//html[1]//body[1]//table[1]//tr[2]//td[1]");

                    if (node == null || node.InnerText == "" || !node.InnerText.ToLower().Contains("from:"))
                    {
                        //throw new NotImplementedException();
                        isGood = false;
                        return file;
                    }
                    isGood = true;
                    return node.InnerHtml.Replace("<div class=\"headerdisplayname\" style=\"display:inline;\">From: </div>", "");
                }
            }
            isGood = false;
            return null;
        }
    }
}