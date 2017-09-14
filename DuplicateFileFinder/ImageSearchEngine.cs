using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFileFinder
{
    class ImageSearchEngine
    {
        private string[] searchPattern = { "*.jpg", "*.png" };

        internal FileInfo[] FindAllImages(ObservableCollection<string> searchPaths, BackgroundWorker worker)
        {   

            var tmpMap = new Dictionary<string, FileInfo>();
            foreach (var pattern in searchPattern)
            {
                foreach (var folderPath in searchPaths)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
                    var filesFound = dirInfo.GetFiles(pattern, SearchOption.AllDirectories);
                    foreach (var item in filesFound)
                    {
                        if (!tmpMap.ContainsKey(item.FullName))
                        {
                            tmpMap.Add(item.FullName, item);
                        }
                    }
                }             
                
                
            }
            worker.ReportProgress(35);
            var vals = tmpMap.Values.ToArray<FileInfo>();
            return vals;
            
        }

    }
}
