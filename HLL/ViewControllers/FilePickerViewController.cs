using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HLL.ViewControllers
{
    class FilePickerViewController : ViewController
    {
        private ListView folderListView;
        private Label selectedPathLabel;

        private string path;
        
        public FilePickerViewController(ViewControllerContext context, string path="") : base(context)
        {
            this.path = path;
        }

        void folderListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.folderListView.SelectedItem != null)
            {
                var selectedFolder = ((FolderListItem)this.folderListView.SelectedItem).Text;
                
                var newPath = this.path == "" ? selectedFolder : this.path + @"\" + selectedFolder;
                newPath = newPath.Replace(@"\\", @"\");
                var info = new FileInfo(newPath);
                newPath = info.FullName;
                var newViewController = new FilePickerViewController(this.context, newPath);
                this.context.NavigationViewController.PushView(newViewController);
            }
        }
        public override void BeforeShow()
        {
            this.selectedPathLabel.Content = path;
            this.context.NavigationViewController.SetRightButtonText("");
            if (path == "")
                this.context.NavigationViewController.SetTitle("Select Folder");
            else
            {
                var title = Path.GetFileName(path);
                title = title == "" ? path : title;
                this.context.NavigationViewController.SetTitle(title);
            }
                
            this.folderListView.ItemsSource = FolderListItem.CreateForDirectory(path);
            
        }

        public override UserControl CreateView()
        {
            this.nib = new Nibs.FilePicker();
            return this.nib;
        }
        public override void AfterCreate()
        {
            this.folderListView = this.GetView().FindName("FolderListView") as ListView;
            this.selectedPathLabel = this.GetView().FindName("SelectedPath") as Label;
            this.folderListView.SelectionChanged += folderListView_SelectionChanged;
        }
        public override Grid GetView()
        {
            return this.nib.FindName("FilePickerGrid") as Grid;
        }
    }
}
