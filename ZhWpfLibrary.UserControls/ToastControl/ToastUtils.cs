using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ZhWpfLibrary.UserControls
{
    public static class ToastUtils
    {
        private static double _topFrom;

        private static Thickness _margin = new Thickness(0, 20, 0, 0);

        private static object toastLock = new object();

        private static List<ToastControl> toastControls = new List<ToastControl>();


        public static void Show(Window owner, string title, string content, ToastEnum toastEnum = ToastEnum.Infomation)
        {
            ToastControl toast = new ToastControl();
            toast.ToastTitle = title;
            toast.ToastContent = content;
            toast.Owner = owner;
            ImageSource image = null;
            switch (toastEnum)
            {
                case ToastEnum.Alert:
                    image = new BitmapImage(new Uri("/ZhWpfLibrary.Assets;component/Images/alert.png", UriKind.RelativeOrAbsolute));
                    break;
                case ToastEnum.Error:
                    image = new BitmapImage(new Uri("/ZhWpfLibrary.Assets;component/Images/error.png", UriKind.RelativeOrAbsolute));
                    break;
                case ToastEnum.Infomation:
                    image = new BitmapImage(new Uri("/ZhWpfLibrary.Assets;component/Images/infomation.png", UriKind.RelativeOrAbsolute));
                    break;
                case ToastEnum.Success:
                    image = new BitmapImage(new Uri("/ZhWpfLibrary.Assets;component/Images/success.png", UriKind.RelativeOrAbsolute));
                    break;
            }

            Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => {
                toast.ToastImage = image;
            }));

            toast.Loaded += Toast_Loaded;
            toast.Closed += Toast_Closed;

            lock (toastLock)
            {
                toastControls.Add(toast);
            }
            toast.Show();


        }


        public static void Show(Window owner, string title, string content, bool isAutoClose, TimeSpan closeTimeSpan, ToastEnum toastEnum = ToastEnum.Infomation)
        {
            ToastControl toast = new ToastControl();
            toast.ToastTitle = title;
            toast.ToastContent = content;
            toast.IsAutoClose = isAutoClose;
            toast.CloseTimeSpan = closeTimeSpan;
            toast.Owner = owner;

            ImageSource image = null;
            switch (toastEnum)
            {
                case ToastEnum.Alert:
                    image = new BitmapImage(new Uri("/ZhWpfLibrary.Assets;component/Images/alert.png", UriKind.RelativeOrAbsolute));
                    break;
                case ToastEnum.Error:
                    image = new BitmapImage(new Uri("/ZhWpfLibrary.Assets;component/Images/error.png", UriKind.RelativeOrAbsolute));
                    break;
                case ToastEnum.Infomation:
                    image = new BitmapImage(new Uri("/ZhWpfLibrary.Assets;component/Images/infomation.png", UriKind.RelativeOrAbsolute));
                    break;
                case ToastEnum.Success:
                    image = new BitmapImage(new Uri("/ZhWpfLibrary.Assets;component/Images/success.png", UriKind.RelativeOrAbsolute));
                    break;
            }

            Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
            {
                toast.ToastImage = image;
            }));

            toast.Loaded += Toast_Loaded;
            toast.Closed += Toast_Closed;

            lock (toastLock)
            {
                toastControls.Add(toast);
            }
            toast.Show();

        }


        private static void Toast_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var toast = sender as ToastControl;
            lock (toastLock)
            {
                _topFrom += (toast.ActualHeight + _margin.Top);
                toast.Top = _topFrom;
            }
        }


        private static void Toast_Closed(object sender, EventArgs e)
        {
            lock (toastLock)
            {
                var toastControl = sender as ToastControl;
                toastControl.Loaded -= Toast_Loaded;
                toastControl.Closed -= Toast_Closed;
                toastControls.Remove(toastControl);


                _topFrom = 0;
                if (toastControls.Count == 0)
                {
                    return;
                }

                foreach (var item in toastControls)
                {
                    _topFrom += (item.ActualHeight + _margin.Top);
                    if (item.Top == _topFrom)
                    {
                        continue;
                    }
                    item.TopAnimation(_topFrom);
                }

            }
        }
    }

}
