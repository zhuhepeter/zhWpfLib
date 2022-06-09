using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ZhWpfLibrary.Behaviors.Drag
{
    public class CanvasChildDragBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.MouseDown += AssociatedObject_MouseDown;
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            this.AssociatedObject.MouseUp += AssociatedObject_MouseUp;
        }


        private Canvas _parentCanvas;

        private Point _relativeSelfPoint;

        public bool IsCanOutBound { get; set; }

        private void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_parentCanvas == null)
            {
                _parentCanvas = VisualTreeHelper.GetParent(this.AssociatedObject) as Canvas;
            }
            _relativeSelfPoint = e.GetPosition(this.AssociatedObject);
            this.AssociatedObject.CaptureMouse();

            e.Handled = true;
        }

        private void AssociatedObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_parentCanvas == null)
            {
                return;
            }

            if (!this.AssociatedObject.IsMouseCaptured)
            {
                return;
            }

            Point point = e.GetPosition(_parentCanvas);

            double left = point.X - _relativeSelfPoint.X;
            double top = point.Y - _relativeSelfPoint.Y;

            if (!IsCanOutBound)
            {
                if (left < 0)
                {
                    left = 0;
                }

                if (top < 0)
                {
                    top = 0;
                }

                if (top + this.AssociatedObject.ActualHeight >= _parentCanvas.ActualHeight)
                {
                    top = _parentCanvas.ActualHeight - this.AssociatedObject.ActualHeight;
                }

                if (left + this.AssociatedObject.ActualWidth >= _parentCanvas.ActualWidth)
                {
                    left = _parentCanvas.ActualWidth - this.AssociatedObject.ActualWidth;
                }
            }



            Canvas.SetLeft(this.AssociatedObject, left);
            Canvas.SetTop(this.AssociatedObject, top);
            e.Handled = true;
        }

        private void AssociatedObject_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.AssociatedObject.ReleaseMouseCapture();

            e.Handled = true;
        }


        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
            this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            this.AssociatedObject.MouseUp -= AssociatedObject_MouseUp;
        }

    }
}
