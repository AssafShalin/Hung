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
    /// Interaction logic for iButton.xaml
    /// </summary>
    public partial class iButton : UserControl
    {
        public event EventHandler OnClick;
        public String Label
        {
            get { return (String)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string),
              typeof(iButton), new PropertyMetadata(""));
        private SolidColorBrush iBlue;
        private SolidColorBrush white;
        public iButton()
        {
            this.iBlue = new SolidColorBrush(Color.FromRgb(0x15, 0x7e, 0xfb));
            this.white = new SolidColorBrush(Color.FromRgb(0xff, 0xff, 0xff));
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.ButtonBorder.Background = iBlue;
            this.ButtonLabel.Foreground = white;
        }

        private void ButtonBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.ButtonBorder.Background = white;
            this.ButtonLabel.Foreground = iBlue;
            if (this.OnClick != null)
                this.OnClick(this, null);
        }
    }
}
