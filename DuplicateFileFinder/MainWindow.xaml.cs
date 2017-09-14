using System.Windows;
using System.Windows.Forms;

namespace DuplicateFileFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void AddSearchPath(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dia = new FolderBrowserDialog();
            dia.ShowNewFolderButton = false;
            var res = dia.ShowDialog();
            if(res == System.Windows.Forms.DialogResult.OK)
            {
                var vm = DataContext as MainViewModel;
                if (!vm.SearchPaths.Contains(dia.SelectedPath))
                    vm.SearchPaths.Add(dia.SelectedPath);
            }
        }

        private void ShowDetails(object sender, RoutedEventArgs e)
        {
            var btn = sender as System.Windows.Controls.Button;
            var item = btn.DataContext as SearchItem;
            PreviewWindow displayWindow = new PreviewWindow(item);
            displayWindow.Show();
        }
    }
}
