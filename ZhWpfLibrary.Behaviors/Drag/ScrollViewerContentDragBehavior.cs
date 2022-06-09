using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ZhWpfLibrary.Behaviors.Drag
{
    public class ScrollViewerContentDragBehavior : Behavior<FrameworkElement>
    {
        private ScrollViewer _parentScrollViewer;

        private Point _beginPosition;

        private Point _scrollOffset;

        protected override void OnAttached()
        {
            _parentScrollViewer = LogicalTreeHelper.GetParent(this.AssociatedObject) as ScrollViewer;
            if (_parentScrollViewer == null)
            {
                return;
            }
            this.AssociatedObject.MouseDown += AssociatedObject_MouseDown;
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            this.AssociatedObject.MouseUp += AssociatedObject_MouseUp;
            base.OnAttached();
        }



        private void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _beginPosition = e.GetPosition(_parentScrollViewer);
            _scrollOffset = new Point();
            _scrollOffset.X = _parentScrollViewer.HorizontalOffset;
            _scrollOffset.Y = _parentScrollViewer.VerticalOffset;

            this.AssociatedObject.CaptureMouse();

        }


        private void AssociatedObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!this.AssociatedObject.IsMouseCaptured)
            {
                return;
            }

            Point _nowPosition = e.GetPosition(_parentScrollViewer);
            double horizontalOffset = _scrollOffset.X - (_nowPosition.X - _beginPosition.X);
            double verticalOffset = _scrollOffset.Y - (_nowPosition.Y - _beginPosition.Y);
            _parentScrollViewer.ScrollToHorizontalOffset(horizontalOffset);
            _parentScrollViewer.ScrollToVerticalOffset(verticalOffset);
        }

        private void AssociatedObject_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.AssociatedObject.ReleaseMouseCapture();
        }


        protected override void OnDetaching()
        {
            if (_parentScrollViewer != null)
            {
                this.AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
                this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
                this.AssociatedObject.MouseUp -= AssociatedObject_MouseUp;
            }
            base.OnDetaching();
        }

    }
}
