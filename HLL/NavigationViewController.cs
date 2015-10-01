using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HLL
{
    class NavigationViewController
    {
        private Grid navigationBar;
        private Label leftButton;
        private Label rightButton;
        private Stack<ViewController> views = new Stack<ViewController>();
        public delegate void buttonAction();
        private string defaultLeftButtonText = "< Back";
        private string defaultRightButtonText = "Done";

        private buttonAction leftButtonAction;
        private buttonAction rightButtonAction;
        private Label title;
        private Window canvas;
        private Grid canvasGrid;
        public NavigationViewController(Window canvas, Grid navigationBar)
        {
            this.canvas = canvas;
            this.canvasGrid = canvas.FindName("Canvas") as Grid;
            this.navigationBar = navigationBar;
            this.leftButton = (Label)navigationBar.FindName("LeftButtonLabel");
            this.rightButton = (Label)navigationBar.FindName("RightButtonLabel");
            this.title = (Label)navigationBar.FindName("Title");
            ((StackPanel)(navigationBar.FindName("LeftButton"))).MouseDown += leftButton_MouseDown;
            ((StackPanel)(navigationBar.FindName("RightButton"))).MouseDown += rightButton_MouseDown;
        }

        void rightButton_MouseDown(object sender, EventArgs e)
        {
            if (rightButtonAction != null)
            {
                rightButtonAction();
            }
            else
            {
                //Do deafult operation
            }
        }

        void leftButton_MouseDown(object sender, EventArgs e)
        {
            if (leftButtonAction != null)
            {
                leftButtonAction();
            }
            else
            {
                this.PopView();
            }

        }
        
        public void SetTitle(string title)
        {
            if (title.Length > 15) {
                title = title.Substring(0, 15);
                title.Trim();
                title += "...";
            }
            this.title.Content = title;
        }
        public void SetLeftButtonText(string text)
        {
            leftButton.Content = text;
        }
        public void SetRightButtonText(string text)
        {
            rightButton.Content = text;
        }
        public void HideNavigationBar()
        {

        }
        public void SetLeftButtonAction(buttonAction action)
        {
            this.leftButtonAction = action;
        }
        public void SetRightButtonAction(buttonAction action)
        {
            this.rightButtonAction = action;
        }

        private void HandleNavigationBar(ViewController view)
        {
            if (view.NavigationBarExists)
            {
                if(!this.views.Peek().NavigationBarExists)
                    UIAnimations.PopInNavigationBar(this.navigationBar);
            }
            else
            {
                UIAnimations.PopOutNavigationBar(this.navigationBar);
            }
        }
        public void PopView()
        {
            if(views.Count <= 1)
            {
                return;
            }
            var currentView = views.Pop();
            var lastView = views.Peek();
            ResetViewToDefaults(lastView);
            HandleNavigationBar(lastView);
            currentView.BeforeHide();
            lastView.BeforeShow();
            UIAnimations.PopSwapViews(currentView.GetView(), lastView.GetView(), () =>
            {
                lastView.OnHide();
                currentView.OnShow();
                this.canvasGrid.Children.Remove(lastView.GetNib());
                
            });
            

            
        }
        private void ResetViewToDefaults(ViewController destinationView)
        {
            if(destinationView.NavigationBarExists)
            {
                SetLeftButtonText(defaultLeftButtonText);
                SetRightButtonText(defaultRightButtonText);
                SetTitle("");
                leftButtonAction = null;
                rightButtonAction = null;
            }
        }

        public void AddViewToWindow(ViewController view)
        {
            var nib = view.CreateView();
            var grid = view.GetView();
            grid.Visibility = Visibility.Hidden;
            canvasGrid.Children.Add(nib);
            view.AfterCreate();
        }
        public void PushView(ViewController view) 
        {
            AddViewToWindow(view);
            HandleNavigationBar(view);
            if (views.Count == 0)
            {
                views.Push(view);
                ResetViewToDefaults(view);
                view.GetView().Visibility = Visibility.Visible;
                view.OnShow();
            }
            else
            {
                ViewController lastView = views.Peek();
                views.Push(view);
                lastView.BeforeHide();
                ResetViewToDefaults(view);
                view.BeforeShow();
                
                UIAnimations.PushSwapViews(view.GetView(), lastView.GetView(), () => {
                    lastView.OnHide();
                    view.OnShow();
                });
                

            }
            
            
        }


    }
}
