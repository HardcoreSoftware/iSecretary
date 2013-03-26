using System.Diagnostics;

namespace UserInterface
{
    public class DirectoryVisualiser
    {
        public static void ShowDirectory(string directory)
        {
            Process.Start(directory);
        }
        
        public static void ShowFile(string filename)
        {
            var args = string.Format("/Select, {0}", filename);
            var pfi = new ProcessStartInfo("Explorer.exe", args);
            Process.Start(pfi);
        }
    }
}