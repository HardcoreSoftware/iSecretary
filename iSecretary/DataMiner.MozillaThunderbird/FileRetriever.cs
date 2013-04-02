using System.IO;
using IOInteraction;

namespace DataMiner.MozillaThunderbird
{
    public class FileRetriever
    {
        public static string[] GetFiles(string emailExportDirectory, string Ext)
        {
            DirectoryCreator.EnsureExistance(emailExportDirectory);
            return Directory.GetFiles(emailExportDirectory, Ext, SearchOption.AllDirectories);
        }
    }
}