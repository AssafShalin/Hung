using System;
using System.Collections.Generic;
using System.IO;
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
using WpfAnimatedGif;

namespace HLL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NavigationViewController navigationViewController;
        StoryBoard storyBoard;
        public MainWindow()
        {
            InitializeComponent();
            navigationViewController = new NavigationViewController(this, this.NavigationBar);
            storyBoard = new StoryBoard(this,navigationViewController);
            
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("pack://application:,,,/Hung/");
            var types = Fonts.GetTypefaces(uri, "./Fonts");
            navigationViewController.PushView(storyBoard.GetViewController(0));
        }


    }
}
