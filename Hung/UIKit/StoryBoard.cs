using HLL.ViewControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HLL
{
    class StoryBoard
    {
        Dictionary<string, ViewController> stringIndexViews = new Dictionary<string, ViewController>();
        Dictionary<int, ViewController> intIndexViews = new Dictionary<int, ViewController>();
        
        public StoryBoard(Window window, NavigationViewController navigationViewController)
        {
            ViewControllerContext context = new ViewControllerContext(this, navigationViewController);
            AddView(0, "GetStarted", new GetStartedViewController(context));
            AddView(1, "FilePicker", new FilePickerViewController(context));


        }
        
        private void AddView(int index, string name, ViewController view)
        {
            stringIndexViews[name] = view;
            intIndexViews[index] = view;
        }
        public ViewController GetViewController(string viewControllerId) 
        {
            return stringIndexViews[viewControllerId];
        }
        public ViewController GetViewController(int index)
        {
            return intIndexViews[index];
        }
        
    }

}
