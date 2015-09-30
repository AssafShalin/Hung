using System;
using System.Collections.Generic;
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
                 var dirs = Directory.GetDirectories(path);
                foreach(var dir in dirs) 
                {
                    list.Add(new FolderListItem(dir));
                }
            }
            return list;

            
        }
    }
}
