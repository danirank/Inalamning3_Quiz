using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace QuizInlamning3.Services
{
    public static class FileHelper
    {
        static string AppName = "QuizInlamning3";
        public static string GetAppLocalFolderPath(string filename)
        {
            
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folderPath = Path.Combine(appDataPath, "QuizInlamning3");

            Directory.CreateDirectory(folderPath);

            return Path.Combine(folderPath, filename);
        }


        static string GetAppDataRoot()
        {
            string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string root = Path.Combine(local, AppName);
            Directory.CreateDirectory(root);
            return root;
        }

        static string ContentDataRoot() =>
            Path.Combine(AppContext.BaseDirectory, "Data");

        // Bygger hel AppData-sökväg från relativ del, t.ex. "Images/Csharp/a.png"
        static string InAppData(string relative)
        {
            string full = Path.Combine(GetAppDataRoot(), relative);
            Directory.CreateDirectory(Path.GetDirectoryName(full));
            return full;
        }

        public static void EnsureDataSeeded()
        {
            string srcRoot = ContentDataRoot();
            if (!Directory.Exists(srcRoot)) return;

            // Normalisera base så vi kan använda Substring
            srcRoot = Path.GetFullPath(srcRoot)
                .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                + Path.DirectorySeparatorChar;

            foreach (var src in Directory.GetFiles(srcRoot, "*.*", SearchOption.AllDirectories))
            {
                string full = Path.GetFullPath(src);
                if (!full.StartsWith(srcRoot, StringComparison.OrdinalIgnoreCase))
                    continue;

                string relative = full.Substring(srcRoot.Length); // t.ex. "Images/Csharp/a.png"
                string dst = InAppData(relative);

                bool needsCopy = !File.Exists(dst) ||
                                 File.GetLastWriteTimeUtc(full) > File.GetLastWriteTimeUtc(dst);

                if (needsCopy)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(dst));
                    File.Copy(full, dst, overwrite: true);
                }
            }
        }


    }
}
