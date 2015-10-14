using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLL.ViewControllers.Adapters
{
    class TaskRunnerTasks
    {
        private List<Tuple<string, Func<bool>>> tasks = new List<Tuple<string, Func<bool>>>();

        public void AddTask(string description, Func<bool> task)
        {
            tasks.Add(Tuple.Create<string,Func<bool>>(description,task));
        }



        public List<Tuple<string, Func<bool>>> GetTasks()
        {
            return tasks;
        }
    }
}
