using HLL.Controls;
using HLL.ViewControllers.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HLL.ViewControllers
{
    class TaskRunnerViewController : ViewController
    {
        private StackPanel taskContainer;
        private iButton button;
        public TaskRunnerViewController(ViewControllerContext context, TaskRunnerTasks tasks) : base(context)
        {
            this.tasks = tasks;
        }
        public override System.Windows.Controls.UserControl CreateView()
        {
            this.nib = new Nibs.TaskRunner();
            return nib;
        }

        public override void AfterCreate()
        {
            
            this.taskContainer = GetView().FindName("TasksContainer") as StackPanel;
            this.button = GetView().FindName("Continue") as iButton;
        }
        public override void BeforeShow()
        {
            this.context.NavigationViewController.SetRightButtonText("");
            this.context.NavigationViewController.SetTitle(this.tasks.TaskTitle);
        }
        public override void OnShow()
        {
            (new Thread(PerformTasks)).Start();
        }

        public void PerformTasks()
        {
            foreach(var task in this.tasks.GetTasks())
            {
                var desc = task.Item1;
                var action = task.Item2;
                TaskElement taskView = null;
                var waitForUI = true;
                this.nib.Dispatcher.Invoke(() =>
                {
                    taskView = CreateTaskView(desc);
                    waitForUI = false;
                });
                while (waitForUI) { }
                var results = action();
                var status = results ? TaskElement.StatusEnum.Success : TaskElement.StatusEnum.Fail;
                this.nib.Dispatcher.Invoke(() =>
                {
                    taskView.Status = status;
                });
            }
            this.nib.Dispatcher.Invoke(() =>
            {
                this.button.Visibility = Visibility.Visible;
                UIAnimations.FlowDown(this.button,0);
            });
        }
        private TaskElement CreateTaskView(string description)
        {
            var taskView = new TaskElement();
            taskView.Height = 60;
            taskView.Label = description;
            taskContainer.Children.Add(taskView);
            Grid grid = taskView.FindName("Container") as Grid;
            UIAnimations.FlowDown(taskView, 0);
            return taskView;
        }

        public override Grid GetView()
        {
            return this.nib.FindName("TaskRunnerGrid") as Grid;
        }

        public TaskRunnerTasks tasks { get; set; }
    }
}
