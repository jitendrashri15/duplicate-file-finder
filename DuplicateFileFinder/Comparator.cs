using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFileFinder
{
    internal static class Comparator
    {
        internal static void FindDuplicateImages(FileInfo[] allFiles, SearchCriteriaModel searchCriteriaSelection, BackgroundWorker worker)
        {
            for (int i = 0; i < allFiles.Length - 1; i++)
            {
                for (int j = i + 1; j < allFiles.Length; j++)
                {
                    bool res = FilesAreEqual(allFiles[i], allFiles[j], searchCriteriaSelection);
                    if (res)
                    {
                        var leftItem = allFiles[i];
                        var rightItem = allFiles[j];

                        SearchItem item = new SearchItem()
                        {
                            LeftItemName = leftItem.Name,
                            LeftItemPath = leftItem.FullName,
                            LeftItemCreationDate = leftItem.CreationTime.ToString(),
                            LeftItemSize = (leftItem.Length / 1024).ToString(),

                            RightItemName = rightItem.Name,
                            RightItemPath = rightItem.FullName,
                            RightItemCreationDate = rightItem.CreationTime.ToString(),
                            RightItemSize = (rightItem.Length / 1024).ToString()
                        };
                        worker.ReportProgress(0, item);
                    }
                }
            }

        }

        private static bool FilesAreEqual(FileInfo leftItem, FileInfo rightItem, SearchCriteriaModel searchCriteriaSelection)
        {
            bool isSuccess = false;
            if (searchCriteriaSelection.Name)
            {
                if (leftItem.Name == rightItem.Name)
                    isSuccess = true;
                else
                    return false;
            }

            if (searchCriteriaSelection.Size)
            {
                if (leftItem.Length == rightItem.Length)
                    isSuccess = true;
                else
                    return false;
            }

            if (searchCriteriaSelection.DateCreated)
            {
                if (leftItem.CreationTime == rightItem.CreationTime)
                    isSuccess = true;
                else
                    return false;
            }

            return isSuccess;
        }
    }
}
