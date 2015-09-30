using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HLL
{
    class FilePickerViewController : ViewController
    {
        private ListView folderListView;
        private Label selectedPathLabel;

        private string path;
        
        public FilePickerViewController(Grid view, ViewControllerContext context, string path="") : base(view, context)
        {
            this.folderListView = this.GetView().FindName("FolderListView") as ListView;
            this.selectedPathLabel = this.GetView().FindName("SelectedPath") as Label;
            this.folderListView.SelectionChanged += folderListView_SelectionChanged;
            this.path = path;
        }

        void folderListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.folderListView.SelectedItem != null)
            {
                var selectedFolder = ((FolderListItem)this.folderListView.SelectedItem).Text;

                var newPath = this.path == "" ? selectedFolder : this.path + @"\" + selectedFolder;
                var newViewController = new FilePickerViewController(this.GetView(), this.context, newPath);
                this.context.NavigationViewController.PushView(newViewController);
            }
        }
        public override void BeforeShow()
        {
            this.selectedPathLabel.Content = path;
            this.context.NavigationViewController.SetRightButtonText("");
            this.context.NavigationViewController.SetTitle("Select Folder");
            this.folderListView.ItemsSource = FolderListItem.CreateForDirectory(path);
            
        }
    }
}
