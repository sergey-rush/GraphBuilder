using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Xml.Serialization;
using GraphBuilder.Shell.ViewModels;

namespace GraphBuilder.Shell.Models
{
    [SerializableAttribute()]
    [XmlType(AnonymousType = true)]
    [DisplayName("Связь")]
    [Description("Имя объекта")]
    public class Link : Element
    {
        [Category("Система")]
        [DisplayName("№"), Description("Id внешней системы")]
        public string Id { get; set; }

        [Browsable(false)]
        public ElementState ElementState { get; set; }

        private bool _isMain = true;
        /// <summary>
        /// Defines a direct line among others
        /// The main line is the only line directly and straightforward connected to both nodes
        /// any other connection lines are connected under angle
        /// </summary>
        [Browsable(false)]
        public bool IsMain { get { return _isMain; } set { _isMain = value; } }

        private Path _path;
        [Browsable(false)]
        public Path Path
        {
            get
            {
                if (_path == null)
                {
                    _path = new Path();
                }
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        [Browsable(false)]
        public Canvas LabelCanvas { get; private set; }
        
        private ArrowEnds _arrowEnds;

        [Category("Вид")]
        [DisplayName("Стрелка"), Description("Указывает, какой конец линии имеет стрелку")]
        public ArrowEnds ArrowEnds
        {
            get { return _arrowEnds; }
            set
            {
                if (_arrowEnds != value)
                {
                    _arrowEnds = value;
                    SetArrowEnd(_arrowEnds);
                }
            }
        }

        public void SetArrowEnd(ArrowEnds arrowEnds)
        {
            //if (Line != null)
            //{
            //    if (arrowEnds == ArrowEnds.Start)
            //    {
            //        EndArrowCanvas.Children.Clear();
            //        StartArrowCanvas.Children.Clear();
            //        DrawArrow(StartArrowCanvas, EndSymbol.FilledTriangle, StartPoint, EndPoint);
            //    }

            //    if (arrowEnds == ArrowEnds.End)
            //    {
            //        EndArrowCanvas.Children.Clear();
            //        StartArrowCanvas.Children.Clear();
            //        DrawArrow(EndArrowCanvas, EndSymbol.FilledTriangle, EndPoint, StartPoint);
            //    }

            //    if (arrowEnds == ArrowEnds.Both)
            //    {
            //        EndArrowCanvas.Children.Clear();
            //        StartArrowCanvas.Children.Clear();
            //        DrawArrow(StartArrowCanvas, EndSymbol.FilledTriangle, StartPoint, EndPoint);
            //        DrawArrow(EndArrowCanvas, EndSymbol.FilledTriangle, EndPoint, StartPoint);
            //    }

            //    if (arrowEnds == ArrowEnds.None)
            //    {
            //        EndArrowCanvas.Children.Clear();
            //        StartArrowCanvas.Children.Clear();
            //    }
            //}
        }

        public void DrawLinkArrow(Point startPoint, Point endPoint)
        {
            Debug.WriteLine("StartPoint X:{0} Y:{1} EndPoint X:{2} Y:{3}", startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);

            double arrowLength = 15;
            double arrowWidth = 6;
            double distance = 1.1;
            
            Point centerPoint = new Point(startPoint.X + ((endPoint.X - startPoint.X) / distance), startPoint.Y + ((endPoint.Y - startPoint.Y) / distance));
            Debug.WriteLine("CenterPoint X:{0} Y:{1}", centerPoint.X, centerPoint.Y);

            PathFigure arrowFigure = new PathFigure();
            arrowFigure.StartPoint = centerPoint;

            Point leftPoint = new Point(centerPoint.X + arrowWidth, centerPoint.Y + arrowLength);
            Point rightPoint = new Point(centerPoint.X - arrowWidth, centerPoint.Y + arrowLength);

            LineSegment seg1 = new LineSegment();
            seg1.Point = leftPoint;
            arrowFigure.Segments.Add(seg1);

            LineSegment seg2 = new LineSegment();
            seg2.Point = rightPoint;
            arrowFigure.Segments.Add(seg2);

            LineSegment seg3 = new LineSegment();
            seg3.Point = centerPoint;
            arrowFigure.Segments.Add(seg3);

            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(arrowFigure);

            RotateTransform transform = new RotateTransform();
            double theta = Math.Atan2((endPoint.Y - startPoint.Y), (endPoint.X - startPoint.X)) * 180 / Math.PI;
            transform.Angle = theta + 90;
            transform.CenterX = centerPoint.X;
            transform.CenterY = centerPoint.Y;
            pathGeometry.Transform = transform;

            GeometryGroup arrowGeometry = new GeometryGroup();
            arrowGeometry.Children.Add(pathGeometry);

            LineGeometry lineGeometry = new LineGeometry();
            lineGeometry.StartPoint = startPoint;
            lineGeometry.EndPoint = endPoint;
            arrowGeometry.Children.Add(lineGeometry);
            
            Path.Data = arrowGeometry;
            Path.StrokeThickness = StrokeThickness;
            Path.Stroke = Path.Fill = new SolidColorBrush(LineColor);
        }

        private Color _lineColor = Colors.Black;
        [Category("Вид")]
        [DisplayName("Цвет связи"), Description("Задает цвет линии")]
        public Color LineColor
        {
            get { return _lineColor; }
            set
            {
                if (_lineColor != value)
                {
                    _lineColor = value;
                    SetShapeColor(_lineColor);
                }
            }
        }

        public void SetShapeColor(Color color)
        {
            //if (Line != null)
            //{
            //    Line.Stroke = new SolidColorBrush(color);
            //}
        }

        private double _strokeThickness = 1.0;
        [Category("Вид")]
        [DisplayName("Толщина линии"), Description("Задает толщину линии связи")]
        public double StrokeThickness
        {
            get { return _strokeThickness; }
            set
            {
                if (_strokeThickness != value)
                {
                    _strokeThickness = value;
                    SetBorderThickness(_strokeThickness);
                }
            }
        }

        public void SetBorderThickness(double thickness)
        {
            //if (Line != null)
            //{
            //    Line.StrokeThickness = thickness;
            //}
        }

        [XmlIgnore]
        public Brush Fill { get; set; }
        [XmlIgnore]
        public Brush Stroke { get; set; }
        public string Label { get; set; }
        public Arrow Arrow { get; set; }

        [Browsable(false)]
        public Guid NodeFrom { get; set; }

        [Browsable(false)]
        public Guid NodeTo { get; set; }

        private Point _startPoint;
        [Browsable(false)]
        public Point StartPoint
        {
            get { return _startPoint; }
            set
            {
                _startPoint = value;
                SetStartPoint(_startPoint);
            }
        }

        public void SetStartPoint(Point startPoint)
        {
            //if (Line != null)
            //{
            //    Line.X1 = startPoint.X;
            //    Line.Y1 = startPoint.Y;
            //    SetArrowEnd(ArrowEnds);
            //}
        }

        private Point _endPoint;

        [Browsable(false)]
        public Point EndPoint
        {
            get { return _endPoint; }
            set
            {
                _endPoint = value;
                SetEndPoint(_endPoint);
            }
        }

        public void SetEndPoint(Point endPoint)
        {
            //if (Line != null)
            //{
            //    Line.X2 = endPoint.X;
            //    Line.Y2 = endPoint.Y;
            //    SetArrowEnd(ArrowEnds);
            //}
        }

        /// <summary>
        /// Describes a relation between couple of nodes
        /// </summary>
        public Link()
        {
            UId = Guid.NewGuid();
            Id = UId.ToString("D");
        }
        
        private ContextMenu CreateContextMenu()
        {
            ContextMenu contextMenu = new ContextMenu();

            //MenuItem cardMenuItem = new MenuItem();
            //cardMenuItem.Header = "Карты";
            //cardMenuItem.Click += CardMenuItem_Click;
            //contextMenu.Items.Add(cardMenuItem);

            //Separator separator = new Separator();
            //contextMenu.Items.Add(separator);

            MenuItem propMenuItem = new MenuItem();
            propMenuItem.Header = "Свойства";
            propMenuItem.Click += PropMenuItem_Click;
            contextMenu.Items.Add(propMenuItem);

            return contextMenu;
        }

        private void CardMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //NodeWindow nodeWindow = new NodeWindow(this);
            //nodeWindow.Show();
        }

        private void PropMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Bootstrap.Current.PropertyViewModel.SelectedObject = this;
        }

        private bool _selected;

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                ToggleSelection(_selected);
            }
        }

        public void ToggleSelection(bool selected)
        {
            if (selected)
            {
                DropShadowEffect effect = new DropShadowEffect();
                effect.Opacity = 0.8;
                effect.ShadowDepth = 3;
                effect.Direction = 270;
                Element.Effect = effect;
            }
            else
            {
                Element.Effect = null;
            }
        }

        private UIElement _element;

        [XmlIgnore]
        public UIElement Element
        {
            get { return _element; }
            set { _element = value; }
        }

        [XmlIgnore]
        public VirtualCanvas VirtualCanvas { get; set; }

        public UIElement CreateLink()
        {
            if (_element == null)
            {
                DrawLinkArrow(StartPoint, EndPoint);
                _element = Path;
                _element.Uid = UId.ToString("D");
            }
            
            return _element;
        }

        public void UpdateElement()
        {
            DrawLinkArrow(StartPoint, EndPoint);
        }


        public void DisposeElement()
        {
            _element = null;
        }

        VirtualCanvas _parent;
        Typeface _typeface;
        double _fontSize;

        public Size MeasureText(VirtualCanvas parent, string label)
        {
            if (_parent != parent)
            {
                FontFamily fontFamily = (FontFamily)parent.GetValue(TextBlock.FontFamilyProperty);
                FontStyle fontStyle = (FontStyle)parent.GetValue(TextBlock.FontStyleProperty);
                FontWeight fontWeight = (FontWeight)parent.GetValue(TextBlock.FontWeightProperty);
                FontStretch fontStretch = (FontStretch)parent.GetValue(TextBlock.FontStretchProperty);
                _fontSize = (double)parent.GetValue(TextBlock.FontSizeProperty);
                _typeface = new Typeface(fontFamily, fontStyle, fontWeight, fontStretch);
                _parent = parent;
            }
            FormattedText ft = new FormattedText(label, CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight, _typeface, _fontSize, Brushes.Black);
            return new Size(ft.Width, ft.Height);
        }

    }
}