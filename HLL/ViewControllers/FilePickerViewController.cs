using HLL.Controls;
using HLL.ViewControllers.Adapters;
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
        private iButton select;
        private string path;
        
        public FilePickerViewController(ViewControllerContext context, string path="") : base(context)
        {
            this.path = path;
        }

        void folderListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.folderListView.SelectedItem != null)
            {
                var selectedFolder = ((FolderListItem)this.folderListView.SelectedItem).FolderName;
                
                var newPath = this.path == "" ? selectedFolder : this.path + @"\" + selectedFolder;
                newPath = newPath.Replace(@"\\", @"\");
                var info = new FileInfo(newPath);
                newPath = info.FullName;

                var newFolder = FolderListItem.CreateForDirectory(newPath);
                if(newFolder.Count > 0) {
                    var newViewController = new FilePickerViewController(this.context, newPath);
                    this.context.NavigationViewController.PushView(newViewController);
                }
                
            }
        }
        public override void BeforeShow()
        {
            var pathLabel = path.Length > 40 ? path.Substring(0, 20) + "..." + path.Substring(path.Length-20, 20) : path;
            this.selectedPathLabel.Content = pathLabel;
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
            this.select = this.GetView().FindName("SelectButton") as iButton;
            this.folderListView.SelectionChanged += folderListView_SelectionChanged;
            this.select.OnClick += select_OnClick;
        }

        void select_OnClick(object sender, EventArgs e)
        {
            this.context.NavigationViewController.PushView(new TaskRunnerViewController(context, new LoadKeysFromDiskTask(this.path)));
        }
        public override Grid GetView()
        {
            return this.nib.FindName("FilePickerGrid") as Grid;
        }
    }
}
