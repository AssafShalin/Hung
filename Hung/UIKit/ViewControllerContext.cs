using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLL
{
    class ViewControllerContext
    {
        private StoryBoard _storyBoard;
        public StoryBoard StoryBoard{ get {return _storyBoard; }}
        public Models.Models Models { get; set; }

        private NavigationViewController _navigationViewController;
        public NavigationViewController NavigationViewController { get { return _navigationViewController; } }
        public ViewControllerContext(StoryBoard storyBoard, NavigationViewController navigationViewController)
        {
            this._storyBoard = storyBoard;
            this._navigationViewController = navigationViewController;
        }
    }
}
