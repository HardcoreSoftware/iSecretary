using System.IO;

namespace IOInteraction
{
    public class DirectoryCreator
    {
        public static void EnsureExistance(string folder)
        {
            var parts = folder.Split('\\');
            var tmp = "";

            for (var i = 0; i < parts.Length; i++)
            {
                tmp += parts[i];
                if (parts[i].Length > 0)
                {
                    tmp += "\\";
                    if (i > 0)
                    {
                        if (!Directory.Exists(tmp))
                        {
                            Directory.CreateDirectory(tmp);
                        }
                    }
                }
            }
        }
    }
}
