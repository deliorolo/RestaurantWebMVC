using System;
using System.Collections.Generic;
using System.IO;

namespace CodeLibrary.AccessoryCode
{
    public static class FilesDownload
    {
        /// <summary>
        /// It gets a List of strings that holds the names of files based on the list of FileInfo
        /// </summary>
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

        /// <summary>
        /// It gets the info from the directory (and creates it) for sold products files 
        /// </summary>
        public static DirectoryInfo GetDirectoryProductsSoldList()
        {
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Products Sold List");
            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Products Sold List");

            return directory;
        }

    }
}