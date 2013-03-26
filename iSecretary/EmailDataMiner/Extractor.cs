using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using Data;
using IOInteraction;

namespace EmailDataMiner
{
    public class Extractor
    {
        public static List<string> GetEmailAddresses(string emailExportDirectory, out List<string> badFiles)
        {

            DirectoryCreator.EnsureExistance(emailExportDirectory);
            badFiles = new List<string>();

            var results = new List<string>();

            const string ext = "*.html";

            var possiblyNotallFiles = Directory.GetFiles(emailExportDirectory, ext, SearchOption.AllDirectories); //directoryInfo.GetFiles(ext);

            if (!possiblyNotallFiles.Any())
            {
                Console.WriteLine("No {0} files found in {1}", ext, emailExportDirectory);
                return new List<string>();
            }

            var filesToParse = possiblyNotallFiles.Where(x => !x.ToLower().EndsWith("index.html"));

            var toParse = filesToParse as string[] ?? filesToParse.ToArray();

            var count = toParse.Count();
            var index = 0;
            var bad = 0;
            var cap = 10;

            foreach (var file in toParse)
            {
                if (index > cap)
                {
                    break;
                }

                var htmlDoc = new HtmlAgilityPack.HtmlDocument();

                try
                {
                    htmlDoc.Load(file);
                }
                catch (IOException)
                {
                    Console.WriteLine("Please close any target CSV files.");
                    return new List<string>();
                }

                if (htmlDoc.DocumentNode != null)
                {
                    var bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

                    if (bodyNode != null)
                    {
                        var node = bodyNode.SelectSingleNode("//html[1]//body[1]//table[1]//tr[2]//td[1]");

                        Debug.WriteLine("{0} / {1} - bad: {2}", index, count, bad);

                        if (node == null || node.InnerText == "" || !node.InnerText.ToLower().Contains("from:"))
                        {
                            //throw new NotImplementedException();
                            badFiles.Add(file);
                            bad++;
                        }
                        else
                        {
                            results.Add(node.InnerHtml.Replace("<div class=\"headerdisplayname\" style=\"display:inline;\">From: </div>", ""));
                        }
                    }
                }
                index++;
            }

            if (!results.Any())
            {
                Console.WriteLine("No data was imported.");
            }

            results = ProcessRawMatches(results);

            results = RemoveDuplicates(results);


            return results;
        }

        private static List<string> RemoveDuplicates(List<string> results)
        {
            var processed = new List<string>();

            foreach (var result in results.Where(result => !processed.Contains(result)))
            {
                processed.Add(result);
            }

            return processed;
        }

        private static List<string> ProcessRawMatches(IEnumerable<string> results)
        {
            var processed = new List<string>();
            foreach (var result in results)
            {
                var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(result);

                if (htmlDoc.DocumentNode.LastChild.Name.Contains('@'))
                {
                    processed.Add(htmlDoc.DocumentNode.LastChild.Name);
                }
                else if (htmlDoc.DocumentNode.LastChild.InnerText.Contains('@'))
                {
                    processed.Add(htmlDoc.DocumentNode.LastChild.InnerText);
                }
                else
                {
                    throw new NotImplementedException();
                }

                //var x = result.Replace("</", " ").Replace("&lt;/", " ");

                //var parts = x.Split(' ');

                //var cleanedParts = parts.Select(part => part.Replace("&quot;", "").Replace("&gt;", "").Replace("&lt;", "").Replace("<", "").Replace(">", "")).ToList();

                //var candidates = new List<string>();
                //foreach (var cleanedPart in cleanedParts.Where(cleanedPart => cleanedPart.Contains("@") && !candidates.Contains(cleanedPart)))
                //{
                //    candidates.Add(cleanedPart);
                //}
                //if (!candidates.Any() || candidates.Count > 1)
                //{
                //    throw new NotImplementedException();
                //}
                //processed.Add(candidates.First());
            }
            return processed;
        }

        static List<string> DirSearch(string sDir)
        {
            var dirs = new List<string>();
            foreach (var d in Directory.GetDirectories(sDir))
            {
                dirs.AddRange(Directory.GetFiles(d));
                dirs.AddRange(DirSearch(d));
            }
            return dirs;
        }
    }
}
