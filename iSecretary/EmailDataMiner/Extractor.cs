﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Data;
using IOInteraction;

namespace EmailDataMiner
{
    public class Extractor
    {
        public static List<string> GetEmailAddresses(string emailExportDirectory)
        {

            DirectoryCreator.EnsureExistance(emailExportDirectory);

            var results = new List<string>();

            var directoryInfo = new DirectoryInfo(emailExportDirectory);

            const string ext = "*.html";

            if (!directoryInfo.GetFiles(ext).Any())
            {
                Console.WriteLine("No {0} files found in {1}", ext, emailExportDirectory);
                return new List<string>();
            }

            foreach (var file in directoryInfo.GetFiles(ext))
            {
                var htmlDoc = new HtmlAgilityPack.HtmlDocument();

                try
                {
                    htmlDoc.Load(file.FullName);
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

                        if (node == null || node.InnerText == "" || !node.InnerText.ToLower().Contains("from:"))
                        {
                            throw new NotImplementedException();
                        }

                        results.Add(file.FullName, node.InnerHtml.Replace("<div class=\"headerdisplayname\" style=\"display:inline;\">From: </div>",""));
                    }
                }
            }

            if (!results.Any())
            {
                Console.WriteLine("No data was imported.");
            }

            results = ProcessRawMatches(results);

            return results;
        }

        private static List<string> ProcessRawMatches(List<string> results)
        {
            foreach (var result in results)
            {
                

            }
        }
    }
}
