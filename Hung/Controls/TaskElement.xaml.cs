using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HLL.Controls
{
    
    /// <summary>
    /// Interaction logic for TaskElement.xaml
    /// </summary>
    public partial class TaskElement : UserControl
    {
        public enum  StatusEnum {Run, Success, Fail};
        private StatusEnum status;
        public TaskElement()
        {
            InitializeComponent();
            this.progress = this.FindName("Progress") as Image;
            this.success = this.FindName("Success") as Image;
            this.fail = this.FindName("Fail") as Image;
        }
        public String Label
        {
            get { return (String)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        private void HideAll()
        {
            this.progress.Visibility = Visibility.Hidden;
            this.success.Visibility = Visibility.Hidden;
            this.fail.Visibility = Visibility.Hidden;
        }

        public StatusEnum Status
        {
            set {
                status = value;
                switch(value)
                {
                    case StatusEnum.Fail:
                        HideAll();
                        this.fail.Visibility = Visibility.Visible;
                        break;
                    case StatusEnum.Success:
                        HideAll();
                        this.success.Visibility = Visibility.Visible;
                        break;
                    case StatusEnum.Run:
                        HideAll();
                        this.progress.Visibility = Visibility.Visible;
                        break;
                }
            }
            get
            {
                return this.status;
            }
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string),
              typeof(TaskElement), new PropertyMetadata(""));
        private Image progress;
        private Image success;
        private Image fail;


    }
}
