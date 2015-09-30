using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HLL
{
    
    static class UIAnimations
    {
        const double ANIMATION_DURATION_DEFAULT = 0.5;



        public static void PopSwapViews(UIElement currentView, UIElement lastView, Action action = null, double animationDuration = 0.3)
        {
            lastView.Visibility = Visibility.Visible;
            currentView.Opacity = 0;
            DoubleAnimation enterAnimation = new DoubleAnimation(-300, 0, new Duration(TimeSpan.FromSeconds(animationDuration)));
            DoubleAnimation exitAnimation = new DoubleAnimation(0, 300, new Duration(TimeSpan.FromSeconds(animationDuration)));
            enterAnimation.EasingFunction = new ExponentialEase();
            TranslateTransform enterTranslateTransform = new TranslateTransform(-300, 0);
            TranslateTransform exitTranslateTransform = new TranslateTransform(300, 0);
            currentView.RenderTransform = exitTranslateTransform;
            lastView.RenderTransform = enterTranslateTransform;
            enterTranslateTransform.BeginAnimation(TranslateTransform.XProperty, enterAnimation);
            exitTranslateTransform.BeginAnimation(TranslateTransform.XProperty, exitAnimation);
            AnimateOpacity(currentView, 0, false,animationDuration);
            AnimateOpacity(lastView, 0, true, animationDuration);
            enterAnimation.Completed += (object sender, EventArgs e) =>
            {
                currentView.Visibility = Visibility.Hidden;
                if (action != null) action();
            };
        }

        public static void PushSwapViews(UIElement currentView, UIElement lastView, Action action = null, double animationDuration = 0.3)
        {
            currentView.Visibility = Visibility.Visible;
            lastView.Opacity = 0;
            DoubleAnimation enterAnimation = new DoubleAnimation(0, -300, new Duration(TimeSpan.FromSeconds(animationDuration)));
            DoubleAnimation exitAnimation = new DoubleAnimation(300, 0, new Duration(TimeSpan.FromSeconds(animationDuration)));
            enterAnimation.EasingFunction = new ExponentialEase();
            TranslateTransform enterTranslateTransform = new TranslateTransform(-300, 0);
            TranslateTransform exitTranslateTransform = new TranslateTransform(300, 0);
            currentView.RenderTransform = exitTranslateTransform;
            lastView.RenderTransform = enterTranslateTransform;
            enterAnimation.Completed += (object sender, EventArgs e) =>
            {
                lastView.Visibility = Visibility.Hidden;
                if (action != null) action();
            };
            enterTranslateTransform.BeginAnimation(TranslateTransform.XProperty, enterAnimation);
            exitTranslateTransform.BeginAnimation(TranslateTransform.XProperty, exitAnimation);
            
            AnimateOpacity(currentView, 0, true,animationDuration);
            AnimateOpacity(lastView, 0, false,animationDuration);
            

        }



        public static void AnimateOpacity(UIElement control, double delay, bool fadeIn=true, double animationDuration = ANIMATION_DURATION_DEFAULT)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation(fadeIn ? 0 : 1,fadeIn ? 1 : 0, new Duration(TimeSpan.FromSeconds(animationDuration)));
            opacityAnimation.BeginTime = TimeSpan.FromSeconds(delay);
            control.BeginAnimation(Control.OpacityProperty, opacityAnimation);
        }
        public static void FlowIn(UIElement control, double delay, double animationDuration = ANIMATION_DURATION_DEFAULT)
        {
            
            DoubleAnimation xAnimation = new DoubleAnimation(-30, 0, new Duration(TimeSpan.FromSeconds(animationDuration)));
            xAnimation.EasingFunction = new ExponentialEase();
            xAnimation.BeginTime = TimeSpan.FromSeconds(delay);
            TranslateTransform translateTransform = new TranslateTransform(30, 0);
            control.RenderTransform = translateTransform;
            translateTransform.BeginAnimation(TranslateTransform.XProperty, xAnimation);
            AnimateOpacity(control, delay, true ,animationDuration);
            
        }
        public static void FlowDown(UIElement control, double delay, double animationDuration = ANIMATION_DURATION_DEFAULT) 
        {
            DoubleAnimation xAnimation = new DoubleAnimation(-30, 0, new Duration(TimeSpan.FromSeconds(animationDuration)));
            xAnimation.EasingFunction = new ExponentialEase();
            xAnimation.BeginTime = TimeSpan.FromSeconds(delay);
            TranslateTransform translateTransform = new TranslateTransform(0, 30);
            control.RenderTransform = translateTransform;
            translateTransform.BeginAnimation(TranslateTransform.YProperty, xAnimation);
            AnimateOpacity(control, delay,true ,animationDuration);
        }

        public static void FlowUpAndAway(UIElement control, double delay, double animationDuration = ANIMATION_DURATION_DEFAULT) 
        {
            DoubleAnimation xAnimation = new DoubleAnimation(0, -30, new Duration(TimeSpan.FromSeconds(animationDuration)));
            xAnimation.EasingFunction = new ExponentialEase();
            xAnimation.BeginTime = TimeSpan.FromSeconds(delay);
            TranslateTransform translateTransform = new TranslateTransform(0, 30);
            control.RenderTransform = translateTransform;
            translateTransform.BeginAnimation(TranslateTransform.YProperty, xAnimation);
            AnimateOpacity(control, delay, false, animationDuration);
        }
        public static void PopInNavigationBar(Grid navBar)
        {
            navBar.Visibility = Visibility.Visible;
            FlowDown(navBar, 0,0.3);
        }
        public static void PopOutNavigationBar(Grid navBar)
        {
            FlowUpAndAway(navBar, 0, 0.3);
        }
        public static void AnimateEntrance(Grid window)
        {
            var timeout = 0.0;
            var c = FindVisualChildren<Control>(window);
            foreach (var element in FindVisualChildren<Control>(window)) element.Opacity = 0;
            foreach (var element in FindVisualChildren<Image>(window)) element.Opacity = 0;
            foreach(var control in FindVisualChildren<Label>(window))
            {
                FlowIn(control, timeout);
                timeout += 0.1;
            }
            timeout = 0.1;
            foreach (var control in FindVisualChildren<Image>(window))
            {

                FlowDown(control, timeout);
                timeout += 0.1;
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }
                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }


    }
}
