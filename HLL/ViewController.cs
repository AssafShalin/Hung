using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HLL
{
    class ViewController
    {
        private Grid view;
        protected ViewControllerContext context;
        public ViewController(Grid view, ViewControllerContext context)
        {
            this.view = view;
            this.NavigationBarExists = true;
            this.context = context;

        }
        public Grid GetView()
        {
            return view;
        }

        public virtual void OnShow()
        {

        }
        public virtual void OnHide()
        {

        }
        public virtual void BeforeShow()
        {

        }
        
        public virtual void BeforeHide()
        {

        }
        public bool NavigationBarExists { get; set; }
    }
}
