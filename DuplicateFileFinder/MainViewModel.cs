using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DuplicateFileFinder
{
    public class MainViewModel : BaseModel
    {
        private string statusText;
        private int progressPercentage;
        private int noOfFiles;
        private double totalSize;

        public SearchCriteriaModel SearchCriteriaSelection { get; set; }

        public FileTypeModel FileTypeSelection { get; set; }

        public ObservableCollection<SearchItem> SearchItems { get; set; }

        public ObservableCollection<string> SearchPaths { get; set; }
       
        public string StatusText
        {
            get { return this.statusText; }
            set
            {
                if (value != this.statusText)
                {
                    this.statusText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int ProgressPercentage
        {
            get { return this.progressPercentage; }
            set
            {
                if (value != this.progressPercentage)
                {
                    this.progressPercentage = value;
                    NotifyPropertyChanged();
                }
            }
        }
                
        public MainViewModel()
        {
            InitializeModels();
        }

        private void InitializeModels()
        {
            canExecuteStartSearchCommand = true;
            canExecuteDeleteDuplicateCommand = true;
            canExecuteDeleteSelectedCommand = true;
            SearchCriteriaSelection = new SearchCriteriaModel();
            FileTypeSelection = new FileTypeModel();
            worker = new BackgroundWorker();
            SearchPaths = new ObservableCollection<string>();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            SearchItems = new ObservableCollection<SearchItem>();
        }

        #region BackgroundWorker
        private BackgroundWorker worker;

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            worker.ReportProgress(5);
            StatusText = "Worker is started.";
            #region Images
            if(FileTypeSelection.Images)
            {
                var engine = new ImageSearchEngine();
                var allFiles=  engine.FindAllImages(SearchPaths, worker);
                if (allFiles == null || allFiles.Length == 0)
                {
                    StatusText = "Worker is completed. Could not find any file with macthing criteria";
                }

                Comparator.FindDuplicateImages(allFiles, SearchCriteriaSelection, worker);


            }
            #endregion

        }


        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusText = "Worker is completed.";
            if (SearchItems.Count==0)
            {
                StatusText += " No duplicate found.";               
            }

            StatusText += " No of duplicate files are " + noOfFiles + ". Occupied disk space is " +  totalSize +" (KB)";
            ProgressPercentage = 100;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(e.UserState!= null)
            {
                var item = e.UserState as SearchItem;
                if (item != null)
                {
                    ++noOfFiles;
                    totalSize += double.Parse(item.LeftItemSize);
                    SearchItems.Add(item);
                }
            }
            else
            {
                ProgressPercentage = e.ProgressPercentage;
            }           
        }

        #endregion

        #region Commands

        private ICommand startSearchCommand;
        public ICommand StartSearchCommand
        {
            get
            {
                return startSearchCommand ?? (startSearchCommand = new CommandHandler(() => ExecuteStartSearchCommand(), canExecuteStartSearchCommand));
            }
        }

        private bool canExecuteStartSearchCommand;
        
        private void ExecuteStartSearchCommand()
        {
            if (SearchPaths.Count==0)
            {
                StatusText = "Search path can not be empty. Select a path and try again.";
                return;
            }
            if(!worker.IsBusy)
            {
                noOfFiles = 0;
                totalSize = 0;
                SearchItems.Clear();
                worker.RunWorkerAsync();
            }
        }

        private ICommand deleteSelectedCommand;
        public ICommand DeleteSelectedCommand
        {
            get
            {
                return deleteSelectedCommand ?? (deleteSelectedCommand = new CommandHandler(() => ExecuteDeleteSelectedCommand(), canExecuteDeleteSelectedCommand));
            }
        }

        private bool canExecuteDeleteSelectedCommand;

        private void ExecuteDeleteSelectedCommand()
        {            
            var destinationPath = Path.Combine(Path.GetTempPath(), "DuplicateFinder " + DateTime.Now.ToString("dd_MM_yyyy ss_mm_HH"));
            if (!Directory.Exists(destinationPath))
                Directory.CreateDirectory(destinationPath);

            var logFileName = Path.Combine(destinationPath, "delete.log");
            if(!File.Exists(logFileName))
            {
                 File.CreateText(logFileName).Close();
            }

            using (StreamWriter logFile = new StreamWriter(logFileName))
            {
                if (SearchItems.Count == 0)
                {
                    StatusText = "Nothing to delete as of now. Search again and try";
                    return;
                }
                foreach (var item in SearchItems)
                {
                    if (item.LeftItemDelete)
                    {
                        var destFile = Path.Combine(destinationPath, item.LeftItemName);
                        File.Move(item.LeftItemPath, destFile);
                        logFile.WriteLine(string.Format("{0} : Moved from {1} to {2} ", DateTime.Now.ToString(), item.LeftItemPath, destFile));
                    }

                    if (item.RightItemDelete)
                    {
                        var destFile = Path.Combine(destinationPath, item.RightItemName);
                        File.Move(item.RightItemPath, destFile);
                        logFile.WriteLine(string.Format("{0} : Moved from {1} to {2} ", DateTime.Now.ToString(), item.RightItemPath, destFile));

                    }
                }
            }
            StatusText = "Deleted successfully. Review log file " + logFileName;
        }

        private ICommand deleteDuplicateCommand;
        public ICommand DeleteDuplicateCommand
        {
            get
            {
                return deleteDuplicateCommand ?? (deleteDuplicateCommand = new CommandHandler(() => ExecuteDeleteDuplicateCommand(), canExecuteDeleteDuplicateCommand));
            }
        }

        private bool canExecuteDeleteDuplicateCommand;

        private void ExecuteDeleteDuplicateCommand()
        {
            if(SearchItems.Count==0)
            {
                StatusText = "Nothing to delete as of now. Search again and try";
                return;
            }


        }
        #endregion

    }
}
