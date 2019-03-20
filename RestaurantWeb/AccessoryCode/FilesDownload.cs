using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RestaurantWeb.AccessoryCode
{
    public static class FilesDownload
    {
        public static List<string> GetListFilesNames(ICollection<FileInfo> files)
        {
            List<string> fileNames = new List<string>();

            foreach (FileInfo file in files)
            {
                string name = "";
                name = file.Name;
                fileNames.Add(name);

            }

            return fileNames;
        }

        public static DirectoryInfo GetDirectoryProductsSoldList()
        {
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Products Sold List");
            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Products Sold List");

            return directory;
        }

    }
}