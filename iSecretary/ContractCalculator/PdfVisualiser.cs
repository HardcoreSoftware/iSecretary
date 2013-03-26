using System.Diagnostics;

namespace UserInterface
{
    public class PdfVisualiser
    {
        public static void Show(string filename)
        {
            var startInfo = new ProcessStartInfo(filename)
                {
                    WindowStyle = ProcessWindowStyle.Normal
                };
            Process.Start(startInfo);
        }       
    }
}