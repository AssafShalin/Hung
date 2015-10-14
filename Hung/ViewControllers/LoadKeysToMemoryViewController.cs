using HLL.ViewControllers.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HLL.ViewControllers
{
    class LoadKeysToMemoryViewController : TaskRunnerViewController
    {
        private string path;
        public LoadKeysToMemoryViewController(ViewControllerContext context, string path) : base(context)
        {
            this.path = path;
            this.TaskTitle = "Load Keys";
        }
        public override void OnContinueClick(object sender, EventArgs e)
        {
            MessageBox.Show(path);
        }
        
        public override TaskRunnerTasks GetTasks()
        {
            var tasks = new TaskRunnerTasks();
            tasks.AddTask("Drink Coffee", DoSomethingElse);
            tasks.AddTask("Smoke Weed", DoSomethingElse);
            tasks.AddTask("Think Different", DoSomethingElse);
            tasks.AddTask("Drink Beer", DoSomethingElse);
            tasks.AddTask("Make Difference", DoSomethingElse);
            tasks.AddTask("Get Laid", DoSomethingElse);
            tasks.AddTask("Die Peacefully", DoSomthing);
            return tasks;
        }

        public bool DoSomthing()
        {
            Thread.Sleep(1000);
            return false;
        }
        public bool DoSomethingElse()
        {
            Thread.Sleep(700);
            return true;
        }


        
    }
}
