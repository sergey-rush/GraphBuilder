using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using GraphBuilder.Shell.Models;
using Thumb = System.Windows.Controls.Primitives.Thumb;

namespace GraphBuilder.Shell
{
    public class ZoomBox : Control
    {
        private Thumb zoomThumb;
        private Canvas zoomCanvas;
        private Slider zoomSlider;
        private ScaleTransform scaleTransform;
        private VirtualCanvas _canvas;

        public ScrollViewer ScrollViewer
        {
            get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
            set { SetValue(ScrollViewerProperty, value); }
        }

        public static readonly DependencyProperty ScrollViewerProperty =
            DependencyProperty.Register("ScrollViewer", typeof(ScrollViewer), typeof(ZoomBox));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (ScrollViewer == null)
                return;

            VirtualCanvas virtualCanvas = ScrollViewer.Content as VirtualCanvas;
            if (virtualCanvas == null)
                throw new Exception("DesignerCanvas must not be null!");
            _canvas = virtualCanvas;

            zoomThumb = Template.FindName("PART_ZoomThumb", this) as Thumb;
            if (zoomThumb == null)
                throw new Exception("PART_ZoomThumb template is missing!");

            zoomCanvas = Template.FindName("PART_ZoomCanvas", this) as Canvas;
            if (zoomCanvas == null)
                throw new Exception("PART_ZoomCanvas template is missing!");

            zoomSlider = Template.FindName("PART_ZoomSlider", this) as Slider;
            if (zoomSlider == null)
                throw new Exception("PART_ZoomSlider template is missing!");

            virtualCanvas.ContentCanvas.LayoutUpdated += new EventHandler(DesignerCanvas_LayoutUpdated);

            zoomThumb.DragDelta += new DragDeltaEventHandler(Thumb_DragDelta);

            zoomSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(ZoomSlider_ValueChanged);

            scaleTransform = new ScaleTransform();
            _canvas.LayoutTransform = scaleTransform;
        }

        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double scale = e.NewValue / e.OldValue;

            double halfViewportHeight = ScrollViewer.ViewportHeight / 2;
            double newVerticalOffset = ((ScrollViewer.VerticalOffset + halfViewportHeight) * scale - halfViewportHeight);

            double halfViewportWidth = ScrollViewer.ViewportWidth / 2;
            double newHorizontalOffset = ((ScrollViewer.HorizontalOffset + halfViewportWidth) * scale - halfViewportWidth);

            scaleTransform.ScaleX *= scale;
            scaleTransform.ScaleY *= scale;

            ScrollViewer.ScrollToHorizontalOffset(newHorizontalOffset);
            ScrollViewer.ScrollToVerticalOffset(newVerticalOffset);
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double scale, xOffset, yOffset;
            InvalidateScale(out scale, out xOffset, out yOffset);

            ScrollViewer.ScrollToHorizontalOffset(ScrollViewer.HorizontalOffset + e.HorizontalChange / scale);
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset + e.VerticalChange / scale);
        }

        private void DesignerCanvas_LayoutUpdated(object sender, EventArgs e)
        {
            double scale, xOffset, yOffset;
            InvalidateScale(out scale, out xOffset, out yOffset);

            zoomThumb.Width = ScrollViewer.ViewportWidth * scale;
            zoomThumb.Height = ScrollViewer.ViewportHeight * scale;

            Canvas.SetLeft(zoomThumb, xOffset + ScrollViewer.HorizontalOffset * scale);
            Canvas.SetTop(zoomThumb, yOffset + ScrollViewer.VerticalOffset * scale);
            
            //Canvas.SetLeft(zoomThumb, ScrollViewer.HorizontalOffset * scale);
            //Canvas.SetTop(zoomThumb, ScrollViewer.VerticalOffset * scale);
        }

        private void InvalidateScale(out double scale, out double xOffset, out double yOffset)
        {
            
            double w = _canvas.ActualWidth * scaleTransform.ScaleX;
            double h = _canvas.ActualHeight * scaleTransform.ScaleY;

            

            // zoom canvas size
            double x = zoomCanvas.ActualWidth;
            double y = zoomCanvas.ActualHeight;

            double scaleX = x / w;
            double scaleY = y / h;

            scale = (scaleX < scaleY) ? scaleX : scaleY;

            xOffset = (x - scale * w) / 2;
            yOffset = (y - scale * h) / 2;
        }
    }
}