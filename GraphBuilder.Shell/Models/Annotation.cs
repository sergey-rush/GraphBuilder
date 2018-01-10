using System;
using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace GraphBuilder.Shell.Models
{
    public class Annotation : Adorner
    {
        private Brush _fill;
        private Pen _pen;
        private FormattedText _text;
        private string _annotationText;


        public Annotation(UIElement adornedElement, string annotationText): base(adornedElement)
        {
            _annotationText = annotationText;
            _fill = new SolidColorBrush(Colors.Red);
            _fill.Freeze();
            _pen = new Pen(Brushes.LightSteelBlue, 3.0);
            _pen.Freeze();
            _text = new FormattedText(_annotationText, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                new Typeface("Verdana"), 11.0, Brushes.Black);
            IsHitTestVisible = false;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Rect adornedRect = new Rect(base.AdornedElement.RenderSize);
            drawingContext.DrawRectangle(_fill, _pen, adornedRect);
            drawingContext.DrawText(_text, new Point(0, 0));
        }

    }
}