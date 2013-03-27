using System.Collections.Generic;
using System.IO;
using System.Linq;
using IOInteraction;

namespace EmailDataMiner
{
    public class DataWriter
    {
        public static void WriteAll(string directory, string filename, List<string> goodResults, List<string> badResults)
        {
            DirectoryCreator.EnsureExistance(directory);

            // create a writer and open the file
            TextWriter tw = new StreamWriter(directory + filename);

            tw.WriteLine("");
            tw.WriteLine("--- Good results: {0} ---", goodResults.Count);
            tw.WriteLine("");
            foreach (var result in goodResults)
            {
                // write a line of text to the file
                tw.WriteLine(result);
            }


            var domains = goodResults.Where(x => x.Contains('@')).Select(x => x.Split('@')[1]).Distinct().ToList();
            domains.Sort();
            tw.WriteLine("");
            tw.WriteLine("--- Domains: {0} ---", domains.Count);
            tw.WriteLine("");
            foreach (var result in domains)
            {
                // write a line of text to the file
                tw.WriteLine(result);
            }




            tw.WriteLine("");
            tw.WriteLine("--- Bad results: {0} ---", badResults.Count);
            tw.WriteLine("");
            foreach (var result in badResults)
            {
                // write a line of text to the file
                tw.WriteLine(result);
            }

            // close the stream
            tw.Close();

        }
    }
}