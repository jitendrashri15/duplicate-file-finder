using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DuplicateFileFinder
{
    public class FileTypeModel : BaseModel
    {      

        public bool Images
        {
            get
            {
                return images;
            }

            set
            {
                if (value != images)
                {
                    images = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool Videos
        {
            get
            {
                return videos;
            }

            set
            {
                if (value != videos)
                {
                    videos = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool Others
        {
            get
            {
                return others;
            }

            set
            {
                if (value != others)
                {
                    others = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public FileTypeModel()
        {
            Images = true;            
        }

        #region Private fields
        private bool images;
        private bool videos;
        private bool others;
        #endregion       
    }
}
