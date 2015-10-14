using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HLL.ViewControllers
{
    class GetStartedViewController : ViewController
    {
        private Label getStarted;
        public GetStartedViewController(ViewControllerContext context) : base(context)
        {
        }

        void getStarted_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.context.NavigationViewController.PushView(this.context.StoryBoard.GetViewController("FilePicker"));
        }
        public override void BeforeShow()
        {

        }
        public override void AfterCreate()
        {
            this.NavigationBarExists = false;
            this.getStarted = this.GetView().FindName("GetStartedButton") as Label;
            getStarted.MouseDown += getStarted_MouseDown;
        }
        public override void OnShow()
        {
            UIAnimations.AnimateEntrance(this.GetView());
        }

        public override UserControl CreateView()
        {
            this.nib = new Nibs.GetStarted();
            return nib;
        }

        

        public override Grid GetView()
        {
            return this.nib.FindName("GetStartedGrid") as Grid;
        }
    }
}
