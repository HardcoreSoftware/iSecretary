using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace EmailDataMiner
{
    public class DataPostProcesser
    {
        public static List<string> ProcessRawMatches(IEnumerable<string> results, out List<string> failures)
        {
            failures = new List<string>();

            var processedFiles = new List<string>();
            foreach (var result in results)
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(result);

                string final;

                if (htmlDoc.DocumentNode.LastChild.Name.Contains('@'))
                {
                    final = htmlDoc.DocumentNode.LastChild.Name;
                }
                else if (htmlDoc.DocumentNode.LastChild.InnerText.Contains('@'))
                {
                    final = htmlDoc.DocumentNode.LastChild.InnerText;
                }
                else
                {
                    failures.Add(result);
                    final = null;
                }
                if (final != null)
                {
                    if (final.Contains("&"))
                    {
                        final = final.Split(' ').First(x => x.Contains("@"));
                        final = final.Replace("&lt;", "").Replace("&gt;", "").Replace("&quot;","");
                    }
                    processedFiles.Add(final);
                }
            }
            return processedFiles;
        }
    }
}