using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DuplicateFileFinder
{
    public class SearchCriteriaModel : BaseModel
    {      

        public bool Name
        {
            get
            {
                return name;
            }

            set
            {
                if (value != name)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool DateCreated
        {
            get
            {
                return dateCreated;
            }

            set
            {
                if (value != dateCreated)
                {
                    dateCreated = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool Size
        {
            get
            {
                return size;
            }

            set
            {
                if (value != size)
                {
                    size = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public SearchCriteriaModel()
        {
            Name = true;
            DateCreated = false;
            Size = true;
        }

        #region Private fields
        private bool name;
        private bool dateCreated;
        private bool size;
        #endregion

    }
}
