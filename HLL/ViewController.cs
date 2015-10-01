using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HLL
{
    abstract class ViewController
    {
        protected UserControl nib;
        protected ViewControllerContext context;
        public ViewController(ViewControllerContext context)
        {
            this.NavigationBarExists = true;
            this.context = context;

        }
        public UserControl GetNib()
        {
            return this.nib;
        }
        public abstract UserControl CreateView();

        public virtual void AfterCreate()
        {

        }
        public abstract Grid GetView();

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
