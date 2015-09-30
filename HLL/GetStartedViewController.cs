using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HLL
{
    class GetStartedViewController : ViewController
    {
        private Label getStarted;
        public GetStartedViewController(Grid view, ViewControllerContext context) : base(view, context)
        {
            this.NavigationBarExists = false;
            this.getStarted = (Label)view.FindName("GetStartedButton");
            getStarted.MouseDown += getStarted_MouseDown;
        }

        void getStarted_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.context.NavigationViewController.PushView(this.context.StoryBoard.GetViewController("FilePicker"));
        }
        public override void BeforeShow()
        {

        }
        public override void OnShow()
        {
            UIAnimations.AnimateEntrance(this.GetView());
        }
    }
}
