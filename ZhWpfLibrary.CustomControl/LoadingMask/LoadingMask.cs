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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZhWpfLibrary.CustomControl
{
    /// <summary>
    /// create by zhuhe 20220308 
    /// </summary>
    public class LoadingMask : ContentControl
    {
        static LoadingMask()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LoadingMask), new FrameworkPropertyMetadata(typeof(LoadingMask)));
        }


        public Brush LoadingColor
        {
            get { return (Brush)GetValue(LoadingColorProperty); }
            set { SetValue(LoadingColorProperty, value); }
        }


        public static readonly DependencyProperty LoadingColorProperty = DependencyProperty.Register("LoadingColor", typeof(Brush), typeof(LoadingMask), new FrameworkPropertyMetadata((Brush)new BrushConverter().ConvertFromString("#409eff")));



        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }


        public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register("IsLoading", typeof(bool), typeof(LoadingMask), new PropertyMetadata(false));


    }
}
