using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLL
{
    class FolderListItem
    {
        
        public FolderListItem(string text)
        {
            this.Text = text;
        }

        public string Text { get; set; }

        public static List<FolderListItem> CreateForDirectory(string path = "") 
        {
            List<FolderListItem> list = new List<FolderListItem>();
            if(path == "")
            {
                var drives = DriveInfo.GetDrives();
                foreach(var drive in drives) 
                {
                    list.Add(new FolderListItem(drive.Name));
                }
            }
            else
            {
                try
                {
                    var dirs = Directory.GetDirectories(path);
                    foreach (var dir in dirs)
                    {
                        var dirName = Path.GetFileName(dir);
                        list.Add(new FolderListItem(dirName));
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            return list;

            
        }
    }
}
