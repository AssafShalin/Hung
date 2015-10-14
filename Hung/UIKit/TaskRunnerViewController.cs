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
    abstract class TaskRunnerViewController : ViewController
    {
        private StackPanel taskContainer;
        private iButton button;
        public string TaskTitle { get; set; }
        public string ContinueButtonText { get; set; }
        public TaskRunnerViewController(ViewControllerContext context) : base(context)
        {
            this.tasks = this.GetTasks();
            TaskTitle = "Runnig";
            ContinueButtonText = "Continue";
        }
        public abstract TaskRunnerTasks GetTasks();

        public override System.Windows.Controls.UserControl CreateView()
        {
            this.nib = new Nibs.TaskRunner();
            return nib;
        }

        public override void AfterCreate()
        {
            this.taskContainer = GetView().FindName("TasksContainer") as StackPanel;
            this.button = GetView().FindName("Continue") as iButton;
            this.button.Label = this.ContinueButtonText;
            this.button.OnClick += OnContinueClick;
        }

        public virtual void OnContinueClick(object sender, EventArgs e)
        {

        }

        public virtual void OnTasksFinished(bool results)
        {

        }

        public override void BeforeShow()
        {
            this.context.NavigationViewController.SetRightButtonText("");
            this.context.NavigationViewController.SetTitle(this.TaskTitle);
        }
        public override void OnShow()
        {
            (new Thread(PerformTasks)).Start();
        }

        private void PerformTasks()
        {
            int i = 0;
            var totalResults = true;
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
                totalResults &= results;
                var status = results ? TaskElement.StatusEnum.Success : TaskElement.StatusEnum.Fail;
                this.nib.Dispatcher.Invoke(() =>
                {
                    taskView.Status = status;
                    UIAnimations.AnimateTransform(new Point(0, 0), new Point(0, -10 * i), taskView);
                });
                i++;
            }
            this.nib.Dispatcher.Invoke(() =>
            {
                this.button.Visibility = Visibility.Visible;
                UIAnimations.FlowDown(this.button,0);
                this.OnTasksFinished(totalResults);
            });
        }
        private TaskElement CreateTaskView(string description)
        {
            var taskView = new TaskElement();
            taskView.Height = 90;
            taskView.Label = description;
            taskContainer.Children.Add(taskView);
            Grid grid = taskView.FindName("Container") as Grid;
            UIAnimations.FlowUp(taskView, 0);
            return taskView;
        }

        public override Grid GetView()
        {
            return this.nib.FindName("TaskRunnerGrid") as Grid;
        }

        public TaskRunnerTasks tasks { get; set; }
    }
}
