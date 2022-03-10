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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZhWpfLibrary.UserControls
{
    /// <summary>
    /// ToastControl.xaml 的交互逻辑
    /// </summary>
    public partial class ToastControl : Window
    {
        public ToastControl()
        {
            InitializeComponent();
            Left = System.Windows.SystemParameters.WorkArea.Right;
        }

        public bool IsAutoClose { get; set; }

        public TimeSpan CloseTimeSpan { get; set; }

        public ImageSource ToastImage
        {
            get { return (ImageSource)GetValue(ToastImageProperty); }
            set { SetValue(ToastImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToastImageProperty =
            DependencyProperty.Register("ToastImage", typeof(ImageSource), typeof(ToastControl), new PropertyMetadata(null));



        public string ToastContent
        {
            get { return (string)GetValue(ToastContentProperty); }
            set { SetValue(ToastContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ToastContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToastContentProperty =
            DependencyProperty.Register("ToastContent", typeof(string), typeof(ToastControl), new PropertyMetadata(""));



        public string ToastTitle
        {
            get { return (string)GetValue(ToastTitleProperty); }
            set { SetValue(ToastTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ToastTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToastTitleProperty =
            DependencyProperty.Register("ToastTitle", typeof(string), typeof(ToastControl), new PropertyMetadata(""));


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                AccelerationRatio = 0.5,
                DecelerationRatio = 0.5,
                To = System.Windows.SystemParameters.WorkArea.Right - this.ActualWidth,
                Duration = new Duration(new TimeSpan(0, 0, 1)),
            };
            this.BeginAnimation(Window.LeftProperty, doubleAnimation);

            if (IsAutoClose)
            {
                Task.Factory.StartNew(async () =>
                {
                    await Task.Delay(CloseTimeSpan);
                    this.Dispatcher.Invoke(() => {
                        this.CloseToast();
                    });

                });
            }
        }


        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CloseToast();
        }


        public void CloseToast()
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                SpeedRatio = 2,
                To = 0,
                Duration = new Duration(new TimeSpan(0, 0, 1)),
            };
            doubleAnimation.Completed += DoubleAnimation_Completed;
            this.BeginAnimation(Window.OpacityProperty, doubleAnimation);
        }

        public void TopAnimation(double toTop)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                SpeedRatio = 2,
                AccelerationRatio = 0.5,
                DecelerationRatio = 0.5,
                To = toTop,
                Duration = new Duration(new TimeSpan(0, 0, 1)),
            };

            this.BeginAnimation(Window.TopProperty, doubleAnimation);
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
