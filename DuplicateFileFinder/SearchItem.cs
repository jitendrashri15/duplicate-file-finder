using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFileFinder
{
    public class SearchItem : BaseModel
    {

        public string LeftItemName
        {
            get
            {
                return leftItemName;
            }

            set
            {
                if (value != leftItemName)
                {
                    leftItemName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string LeftItemPath
        {
            get
            {
                return leftItemPath;
            }

            set
            {
                if (value != leftItemPath)
                {
                    leftItemPath = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string LeftItemSize
        {
            get
            {
                return leftItemSize;
            }

            set
            {
                if (value != leftItemSize)
                {
                    leftItemSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string LeftItemCreationDate
        {
            get
            {
                return leftItemCreationDate;
            }

            set
            {
                if (value != leftItemCreationDate)
                {
                    leftItemCreationDate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool LeftItemDelete
        {
            get
            {
                return leftItemDelete;
            }

            set
            {
                if (value != leftItemDelete)
                {
                    leftItemDelete = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string RightItemName
        {
            get
            {
                return rightItemName;
            }

            set
            {
                if (value != rightItemName)
                {
                    rightItemName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string RightItemPath
        {
            get
            {
                return rightItemPath;
            }

            set
            {
                if (value != rightItemPath)
                {
                    rightItemPath = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string RightItemSize
        {
            get
            {
                return rightItemSize;
            }

            set
            {
                if (value != rightItemSize)
                {
                    rightItemSize = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        public string RightItemCreationDate
        {
            get
            {
                return rightItemCreationDate;
            }

            set
            {
                if (value != rightItemCreationDate)
                {
                    rightItemCreationDate = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public bool RightItemDelete
        {
            get
            {
                return rightItemDelete;
            }

            set
            {
                if (value != rightItemDelete)
                {
                    rightItemDelete = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #region Private fields
        private string leftItemName;
        private string leftItemPath;
        private string leftItemSize;
        private string leftItemCreationDate;
        private bool leftItemDelete;
        private string rightItemName;
        private string rightItemPath;
        private string rightItemSize;
        private string rightItemCreationDate;
        private bool rightItemDelete;
        #endregion       
    }
}
