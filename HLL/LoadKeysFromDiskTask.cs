using HLL.ViewControllers.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HLL
{
    class LoadKeysFromDiskTask : TaskRunnerTasks
    {
        public LoadKeysFromDiskTask(string path)
        {
            this.TaskTitle = "Loading Keys";
            AddTask("Do Something", DoSomthing);
            AddTask("Think Different", DoSomethingElse);
        }

        public bool DoSomthing()
        {
            Thread.Sleep(1000);
            return true;
        }
        public bool DoSomethingElse()
        {
            Thread.Sleep(1000);
            return false;
        }

    }
}
