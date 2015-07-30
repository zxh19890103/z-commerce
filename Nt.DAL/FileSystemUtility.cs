using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Nt.DAL
{
    public class FileSystemUtility
    {
        public static void DeleteFile(string filePathOnDisk)
        {
            if (File.Exists(filePathOnDisk))
                File.Delete(filePathOnDisk);
        }

        public static void DeleteFiles(string directory, string searchPattern)
        {
            if (!Directory.Exists(directory))
                return;
            foreach (var f in Directory.GetFiles(directory, searchPattern,
                SearchOption.AllDirectories))
            {
                File.Delete(f);
            }
        }
    }
}
