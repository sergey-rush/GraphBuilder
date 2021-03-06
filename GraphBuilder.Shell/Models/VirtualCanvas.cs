﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using GraphBuilder.Core;
using GraphBuilder.Shell.ViewModels;

namespace GraphBuilder.Shell.Models
{
    public partial class VirtualCanvas : VirtualizingPanel
    {
        Border _backdrop;
        public Border Backdrop
        {
            get { return _backdrop; }
        }
        public Document Document { get; set; }
        public static DependencyProperty VirtualChildProperty = DependencyProperty.Register("VirtualChild", typeof(Element), typeof(VirtualCanvas));
        public event EventHandler<VisualChangeEventArgs> VisualsChanged;
        private Canvas _canvas;

        public Canvas ContentCanvas
        {
            get { return _canvas; }
        }
        
        private TranslateTransform _translate;
        private ScaleTransform _scale;
        /// <summary>
        /// The current zoom transform.
        /// </summary>
        public ScaleTransform Scale
        {
            get { return _scale; }
        }

        /// <summary>
        /// The current translate transform.
        /// </summary>
        public TranslateTransform Translate
        {
            get { return _translate; }
        }
        public Line Line { get; set; }

        public VirtualCanvas()
        {
            _canvas = new Canvas();
            _backdrop = new Border();
            _canvas.Children.Add(_backdrop);
            _canvas.AllowDrop = true;
            AllowDrop = true;

            codePopup = new Popup();
            _canvas.Children.Add(codePopup);
            _canvas.Children.Add(CreateCanvasLine());

            TransformGroup g = new TransformGroup();
            _scale = new ScaleTransform();
            _translate = new TranslateTransform();
            g.Children.Add(_scale);
            g.Children.Add(_translate);
            _canvas.RenderTransform = g;

            _translate.Changed += new EventHandler(OnTranslateChanged);
            _scale.Changed += new EventHandler(OnScaleChanged);
            
            Children.Add(_canvas);

            CanvasContextMenu canvasContextMenu = new CanvasContextMenu(this);
            _canvas.ContextMenu = canvasContextMenu.CreateContextMenu();

            Nodes = new List<Node>();
            Links = new List<Link>();
            _popupTimer = new DispatcherTimer(DispatcherPriority.Normal);
        }

        private Line CreateCanvasLine()
        {
            Line = new Line();
            Line.X1 = 0;
            Line.Y1 = 0;
            Line.X2 = 0;
            Line.Y2 = 0;
            
            Line.StrokeThickness = 1;
            DoubleCollection dashes = new DoubleCollection();
            dashes.Add(2);
            dashes.Add(2);
            Line.StrokeDashArray = dashes;
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Transparent;
            Line.Stroke = redBrush;
            return Line;
        }
        
        private void Deselect(Node except)
        {
            foreach (Node node in Nodes)
            {
                if (node.Selected)
                {
                    if (except != null)
                    {
                        if (except.UId != node.UId)
                        {
                            node.Canvas.Cursor = Cursors.Arrow;
                            node.Selected = false;
                        }
                    }
                    else
                    {
                        node.Canvas.Cursor = Cursors.Arrow;
                        node.Selected = false;
                    }
                }
            }
        }

        #region PopupEvents

        private Popup codePopup;

        private void CreatePopup(Node node)
        {
            Border border = new Border();

            border.BorderBrush = Brushes.Blue;
            border.BorderThickness = new Thickness(1);

            StackPanel parentPanel = new StackPanel();
            parentPanel.Background = Brushes.White;

            TextBlock header = new TextBlock();
            header.TextAlignment = TextAlignment.Center;
            header.Text = node.Header;
            parentPanel.Children.Add(header);

            StackPanel stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(20);

            foreach (ListItem item in node.Attributes)
            {
                TextBlock text = new TextBlock();
                text.Text = String.Format("{0} : {1}", item.Name, item.Value);
                stackPanel.Children.Add(text);
            }
            parentPanel.Children.Add(stackPanel);
            border.Child = parentPanel;

            codePopup.Child = border;

            
            _popupTimer.Interval = TimeSpan.FromSeconds(2);
            _popupTimer.Tick += (obj, events) =>
            {
                codePopup.IsOpen = true;
            };
            _popupTimer.Start();
        }

        private DispatcherTimer _popupTimer;
        private void StopPopup()
        {
            if (_popupTimer.IsEnabled)
            {
                _popupTimer.Stop();
                codePopup.IsOpen = false;
            }
        }

        #endregion

        public void SetCursor(MouseMode mouseMode)
        {
            switch (mouseMode)
            {
                case MouseMode.Select:
                    Cursor = Cursors.Arrow;
                    Bootstrap.Current.MouseMode = MouseMode.Select;
                    break;
                case MouseMode.MultiSelect:
                    Cursor = Cursors.Arrow;
                    Bootstrap.Current.MouseMode = MouseMode.MultiSelect;
                    break;
                case MouseMode.Dragging:
                    Cursor = Cursors.Hand;
                    Bootstrap.Current.MouseMode = MouseMode.Dragging;
                    break;
                case MouseMode.DrawLink:
                    Bootstrap.Current.MouseMode = MouseMode.DrawLink;
                    Cursor = Cursors.Cross;
                    break;
                case MouseMode.DrawNode:
                    Bootstrap.Current.MouseMode = MouseMode.DrawNode;
                    Cursor = Cursors.Cross;
                    break;
            }
        }

        /// <summary>
        /// Last point the mouse was clicked
        /// </summary>
        public Point LastPoint;

        #region NodeEvents


        public List<Node> Nodes { get; set; }
        public List<Link> Links { get; set; }

        public void AddNode(Node node)
        {
            node.VirtualCanvas = this;
            UIElement e = node.CreateElement();
            e.SetValue(VirtualChildProperty, node);
            Rect bounds = node.Rect;
            Canvas.SetLeft(e, bounds.Left);
            Canvas.SetTop(e, bounds.Top);
            SetZIndex(e, 0);
            _canvas.Children.Add(e);
            Nodes.Add(node);

            if (VisualsChanged != null)
            {
                VisualsChanged(this, new VisualChangeEventArgs(0, 0));
            }

            if (_owner != null)
            {
                _owner.InvalidateScrollInfo();
            }
        }

        public void PopulateNodes(List<Node> nodes)
        {
            foreach (Node node in nodes)
            {
                AddNode(node);
            }
        }

        private Node FindNode(UIElement element)
        {
            Node selectedNode = null;
            foreach (Node node in Nodes)
            {
                if (node.UId == new Guid(element.Uid))
                {
                    selectedNode = node;
                    break;
                }
            }
            return selectedNode;
        }

        #endregion

        #region LinkEvents

        public Link TempLink { get; set; }

        public void PopulateLinks(List<Link> links)
        {
            foreach (Link link in links)
            {
                AddLink(link);
            }
        }

        private void UpdateLinks(Node node, Point center)
        {
            List<Link> links = node.Links;
            foreach (Link link in links)
            {
                if (link.Element != null)
                {
                    if (link.NodeFrom == node.UId)
                    {
                        link.StartPoint = center;
                        link.UpdateElement();
                    }

                    if (link.NodeTo == node.UId)
                    {
                        link.EndPoint = center;
                        link.UpdateElement();
                    }
                }
                else
                {
                    Debug.WriteLine("UpdateLinks: link is NULL");
                    throw new Exception("UpdateLinks: link is NULL");
                }
            }
        }
        
        #endregion

        #region Canvas Events

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (Bootstrap.Current.MouseMode == MouseMode.DrawNode)
            {
                Point mousePoint = e.GetPosition(ContentCanvas);
                Node node = NodeFactory.CreateNode(mousePoint, ShapeType.Ellipse);
                List<Node> nodes = new List<Node>();
                nodes.Add(node);
                PopulateNodes(nodes);
                e.Handled = true;
                CalculateExtent();
            }

            if (Bootstrap.Current.MouseMode == MouseMode.Select)
            {
                Deselect(null);
            }

            if (Bootstrap.Current.MouseMode == MouseMode.MultiSelect)
            {
                Point mousePoint = e.GetPosition(ContentCanvas);
                BeginMultiSelect(mousePoint);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            LastPoint = e.GetPosition(ContentCanvas);
            base.OnMouseMove(e);
            StopPopup();
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (Bootstrap.Current.MouseMode == MouseMode.DrawLink)
                {
                    Link link = TempLink;

                    if (link.ElementState == ElementState.NodeFrom)
                    {
                        Line.X1 = link.StartPoint.X;
                        Line.Y1 = link.StartPoint.Y;
                        Line.X2 = LastPoint.X;
                        Line.Y2 = LastPoint.Y;
                        SolidColorBrush redBrush = new SolidColorBrush();
                        redBrush.Color = Colors.Red;
                        Line.Stroke = redBrush;
                    }
                }

                if (Bootstrap.Current.MouseMode == MouseMode.MultiSelect)
                {
                    UpdateMultiSelect(LastPoint);
                }

                if (Bootstrap.Current.MouseMode == MouseMode.Resize)
                {
                    ResizeNode(SelectedCanvas, SelectedNode, LastPoint);
                }
            }
        }

        public Canvas SelectedCanvas { get; set; }
        public Node SelectedNode { get; set; }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (Bootstrap.Current.MouseMode == MouseMode.Resize)
            {
                Bootstrap.Current.MouseMode = MouseMode.Select;
            }

            if (Bootstrap.Current.MouseMode == MouseMode.DrawLink)
            {
                Link link = TempLink;
                if (link.ElementState == ElementState.NodeTo)
                {
                    link.ElementState = ElementState.Added;
                    AddLink(link);
                }
                else
                {
                    DisposeLink(link);
                }
                CollapseTempLink(link);
            }

            if (Bootstrap.Current.MouseMode == MouseMode.MultiSelect)
            {
                EndRectangleSelect();
            }
        }

        #endregion

        #region Link Events

        private void CollapseTempLink(Link link)
        {
            Line.X1 = 0;
            Line.Y1 = 0;
            Line.X2 = 0;
            Line.Y2 = 0;
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Colors.Transparent;
            Line.Stroke = brush;
        }

        private void AddLink(Link link)
        {
            link.VirtualCanvas = this;
            UIElement e = link.CreateLink();
            SetZIndex(e, 0);
            e.SetValue(VirtualChildProperty, link);
            _canvas.Children.Add(e);
            Links.Add(link);

            if (VisualsChanged != null)
            {
                VisualsChanged(this, new VisualChangeEventArgs(0, 0));
            }
        }

        private void DisposeLink(Link link)
        {
            foreach (Node node in Nodes)
            {
                if (node.UId == link.NodeFrom)
                {
                    Link selectedLink = null;
                    foreach (Link l in node.Links)
                    {
                        if (l.UId == link.UId)
                        {
                            selectedLink = l;
                            break;
                        }
                    }

                    node.Links.Remove(selectedLink);
                    break;
                }
            }
        }
        
        public void Link_MouseEnter(object sender, MouseEventArgs e)
        {
        }

        public void Link_MouseLeave(object sender, MouseEventArgs e)
        {
        }

        public void Link_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        public void Link_MouseMove(object sender, MouseEventArgs e)
        {
        }

        public void Link_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        #endregion

        #region Shape Events

        public void Shape_MouseEnter(object sender, MouseEventArgs e)
        {
            Canvas element = (Canvas)sender;
            Point mousePoint = e.GetPosition(ContentCanvas);

            codePopup.PlacementRectangle = new Rect(mousePoint.X - 100, mousePoint.Y + 20, 0, 0);

            Node node = FindNode(element);

            if (node != null)
            {
                node.MouseIn();

                if (Bootstrap.Current.MouseMode == MouseMode.Select)
                {
                    element.Cursor = Cursors.Arrow;
                    CreatePopup(node);
                }
            }
        }
        
        public void Shape_MouseLeave(object sender, MouseEventArgs e)
        {
            
            Canvas element = (Canvas)sender;
            Node node = FindNode(element);

            if (node != null)
            {
                node.MouseOut();
            }
            else
            {
                Debug.WriteLine("MouseLeave node NULL !");
            }
        }

        private HitType _hitType = HitType.None;

        public void Shape_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                StopPopup();
                LastPoint = e.GetPosition(ContentCanvas);
                Canvas element = (Canvas) sender;

                Node node = FindNode(element);
                if (node != null)
                {
                    if (!node.Selected)
                    {
                        node.Selected = true;
                        Deselect(node);
                    }

                    if (Bootstrap.Current.MouseMode == MouseMode.DrawLink)
                    {
                        Point innerPoint = e.GetPosition(element);
                        double width = element.ActualWidth;
                        double height = element.ActualHeight;
                        double centerX = LastPoint.X + ((width / 2) - innerPoint.X);
                        double centerY = LastPoint.Y + ((height / 2) - innerPoint.Y);
                        Point center = new Point(centerX, centerY);

                        TempLink = new Link
                        {
                            StartPoint = center,
                            ElementState = ElementState.NodeFrom,
                            NodeFrom = node.UId
                        };
                        node.Links.Add(TempLink);

                        
                    }

                    if (Bootstrap.Current.MouseMode == MouseMode.Select)
                    {
                        _hitType = SetHitType(element, LastPoint);
                        element.Cursor = SetMouseCursor(_hitType);

                        if (_hitType == HitType.None)
                        {
                            Debug.WriteLine("HitType returned NULL");
                            return;
                        }
                        if (_hitType == HitType.Body)
                        {
                            Bootstrap.Current.MouseMode = MouseMode.Dragging;
                            element.CaptureMouse();
                        }
                        else
                        {
                            SelectedCanvas = element;
                            SelectedNode = node;
                            Bootstrap.Current.MouseMode = MouseMode.Resize;
                        }
                    }
                }

                e.Handled = true;
            }
        }

        public void Shape_MouseMove(object sender, MouseEventArgs e)
        {
            Canvas element = (Canvas) sender;
            Point mousePoint = e.GetPosition(ContentCanvas);

            Node node = FindNode(element);
            if (node != null)
            {
                if (Bootstrap.Current.MouseMode == MouseMode.DrawLink)
                {
                    element.Cursor = Cursors.Cross;
                }

                if (node.Selected)
                {
                    if (Bootstrap.Current.MouseMode == MouseMode.Select)
                    {
                        _hitType = SetHitType(element, mousePoint);
                        element.Cursor = SetMouseCursor(_hitType);
                    }

                    if (Mouse.LeftButton == MouseButtonState.Pressed)
                    {
                        if (Bootstrap.Current.MouseMode == MouseMode.Resize)
                        {
                            ResizeNode(SelectedCanvas, SelectedNode, mousePoint);
                        }

                        if (Bootstrap.Current.MouseMode == MouseMode.Dragging)
                        {
                            element.Cursor = Cursors.SizeAll;
                            Canvas.SetLeft(element, mousePoint.X - (element.ActualWidth / 2));
                            Canvas.SetTop(element, mousePoint.Y - (element.ActualHeight / 2));

                            UpdateLinks(node, mousePoint);
                        }
                    }
                }

                

                e.Handled = true;
            }
        }

        public void Shape_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                LastPoint = e.GetPosition(ContentCanvas);
                Canvas element = (Canvas) sender;
                
                if (Bootstrap.Current.MouseMode == MouseMode.Resize)
                {
                    Bootstrap.Current.MouseMode = MouseMode.Select;
                }
                
                if (Bootstrap.Current.MouseMode == MouseMode.Dragging)
                {
                    Bootstrap.Current.MouseMode = MouseMode.Select;
                    element.ReleaseMouseCapture();
                    Node node = FindNode(element);
                    Rect rect = new Rect(LastPoint, element.DesiredSize);
                    node.Rect = rect;
                    CalculateExtent();
                }

                if (Bootstrap.Current.MouseMode == MouseMode.DrawLink)
                {
                    Node node = FindNode(element);
                    if (node != null)
                    {
                        Point innerPoint = e.GetPosition(element);
                        double width = element.ActualWidth;
                        double height = element.ActualHeight;
                        double centerX = LastPoint.X + ((width/2) - innerPoint.X);
                        double centerY = LastPoint.Y + ((height/2) - innerPoint.Y);
                        Point center = new Point(centerX, centerY);

                        TempLink.EndPoint = center;
                        TempLink.ElementState = ElementState.NodeTo;
                        TempLink.NodeTo = node.UId;
                        node.Links.Add(TempLink);
                        
                    }
                }
            }
        }

        #endregion

        #region  Resize Events

        private void ResizeNode(Canvas canvas, Node node, Point point)
        {
            // See how much the mouse has moved.

            double offsetX = point.X - LastPoint.X;
            double offsetY = point.Y - LastPoint.Y;

            // Get the rectangle's current position.
            double newX = Canvas.GetLeft(canvas);
            double newY = Canvas.GetTop(canvas);
            //Debug.WriteLine("Current size - width: {0} height");

            double newWidth = canvas.Width;
            double newHeight = canvas.Height;
            
            // Update the rectangle.
            switch (_hitType)
            {
                case HitType.UpperLeft:
                    newX += offsetX;
                    newY += offsetY;
                    newWidth -= offsetX;
                    newHeight -= offsetY;
                    break;
                case HitType.UpperRight:
                    newY += offsetY;
                    newWidth += offsetX;
                    newHeight -= offsetY;
                    break;
                case HitType.LowerRight:
                    newWidth += offsetX;
                    newHeight += offsetY;
                    break;
                case HitType.LowerLeft:
                    newX += offsetX;
                    newWidth -= offsetX;
                    newHeight += offsetY;
                    break;
                case HitType.Left:
                    newX += offsetX;
                    newWidth -= offsetX;
                    break;
                case HitType.Right:
                    newWidth += offsetX;
                    break;
                case HitType.Bottom:
                    newHeight += offsetY;
                    break;
                case HitType.Top:
                    newY += offsetY;
                    newHeight -= offsetY;
                    break;
            }

            if (newHeight > newWidth)
            {
                newWidth = newHeight;
            }
            else
            {
                newHeight = newWidth;
            }

            if ((newWidth > 20) && (newHeight > 20))
            {
                node.SetDimension(newWidth, newHeight);
                double centerX = Canvas.GetLeft(canvas) + canvas.ActualWidth/2;
                double centerY = Canvas.GetTop(canvas) + canvas.ActualHeight/2;
                UpdateLinks(node, new Point(centerX, centerY));
                LastPoint = point;
            }
        }

        private HitType SetHitType(Canvas canvas, Point point)
        {
            double left = Canvas.GetLeft(canvas);
            double top = Canvas.GetTop(canvas);
            double right = left + canvas.Width;
            double bottom = top + canvas.Height;

            if (point.X < left) return HitType.None;
            if (point.X > right) return HitType.None;
            if (point.Y < top) return HitType.None;
            if (point.Y > bottom) return HitType.None;

            const double gap = 10;
            if (point.X - left < gap)
            {
                // Left edge.
                if (point.Y - top < gap) return HitType.UpperLeft;
                if (bottom - point.Y < gap) return HitType.LowerLeft;
                return HitType.Left;
            }
            if (right - point.X < gap)
            {
                // Right edge.
                if (point.Y - top < gap) return HitType.UpperRight;
                if (bottom - point.Y < gap) return HitType.LowerRight;
                return HitType.Right;
            }
            if (point.Y - top < gap) return HitType.Top;
            if (bottom - point.Y < gap) return HitType.Bottom;
            return HitType.Body;
        }

        private Cursor SetMouseCursor(HitType mouseType)
        {
            Cursor cursor = Cursors.Arrow;
            switch (mouseType)
            {
                case HitType.None:
                    cursor = Cursors.Arrow;
                    break;
                case HitType.Body:
                    cursor = Cursors.Arrow;
                    break;
                case HitType.UpperLeft:
                case HitType.LowerRight:
                    cursor = Cursors.SizeNWSE;
                    break;
                case HitType.LowerLeft:
                case HitType.UpperRight:
                    cursor = Cursors.SizeNESW;
                    break;
                case HitType.Top:
                case HitType.Bottom:
                    cursor = Cursors.SizeNS;
                    break;
                case HitType.Left:
                case HitType.Right:
                    cursor = Cursors.SizeWE;
                    break;
            }
            return cursor;
        }

        #endregion

        #region Selection

        private Rectangle rectSelect;
        private void BeginMultiSelect(Point point)
        {
            rectSelect = new Rectangle();
            rectSelect.Stroke = Brushes.Black;
            rectSelect.StrokeThickness = 1.0;
            DoubleCollection dashes = new DoubleCollection();
            dashes.Add(2);
            dashes.Add(2);
            rectSelect.StrokeDashArray = dashes;
            rectSelect.Fill = null;
            rectSelect.SetValue(Canvas.LeftProperty, point.X);
            rectSelect.SetValue(Canvas.TopProperty, point.Y);
            _canvas.Children.Add(rectSelect);
            LastPoint = point;
            CaptureMouse();
        }

        private void UpdateMultiSelect(Point point)
        {
            rectSelect.Height = Math.Abs(LastPoint.Y - point.Y);
            rectSelect.Width = Math.Abs(LastPoint.X - point.X);
            if (LastPoint.Y > point.Y)
            {
                rectSelect.SetValue(Canvas.TopProperty, point.Y);
            }
            else
            {
                rectSelect.SetValue(Canvas.TopProperty, LastPoint.Y);
            }
            if (LastPoint.X > point.X)
            {
                rectSelect.SetValue(Canvas.LeftProperty, point.X);
            }
            else
            {
                rectSelect.SetValue(Canvas.LeftProperty, LastPoint.X);
            }
        }

        private void EndRectangleSelect()
        {
            Rect rect = new Rect(LastPoint, rectSelect.DesiredSize);

            foreach (Node node in Nodes)
            {
                if (rect.IntersectsWith(node.Rect))
                {
                    node.Selected = true;
                }
            }

            ReleaseMouseCapture();
            _canvas.Children.Remove(rectSelect);
        }

        #endregion

        #region DragEvents

        protected override void OnDragEnter(DragEventArgs e)
        {
            //Debug.WriteLine("OnDragEnter");
            base.OnDragEnter(e);
            var fg = e.Data;
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            //Debug.WriteLine("OnDragOver");
            base.OnDragOver(e);
            Thumb fg = (Thumb)e.Data.GetData(typeof(Thumb));
        }

        protected override void OnDrop(DragEventArgs e)
        {
            Debug.WriteLine("OnDrop");
            base.OnDrop(e);

            var fg = e.Data;
        }

        #endregion

        #region SystemEvents

        /// <summary>
        /// WPF ArrangeOverride for laying out the control
        /// </summary>
        /// <param name="finalSize">The size allocated by parents</param>
        /// <returns>finalSize</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            base.ArrangeOverride(finalSize);

            CalculateExtent();

            if (finalSize != _viewPortSize)
            {
                SetViewportSize(finalSize);
            }

            _canvas.Arrange(new Rect(0, 0, _canvas.Width, _canvas.Height));

            //if (_index == null)
            //{
            //    StartLazyUpdate();
            //}

            return finalSize;
        }

        /// <summary>
        /// WPF Measure override for measuring the control
        /// </summary>
        /// <param name="availableSize">Available size will be the viewport size in the scroll viewer</param>
        /// <returns>availableSize</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            base.MeasureOverride(availableSize);

            // We will be given the visible size in the scroll viewer here.
            CalculateExtent();

            if (availableSize != _viewPortSize)
            {
                SetViewportSize(availableSize);
            }

            //foreach (UIElement child in this.InternalChildren)
            //{
            //    IVirtualChild n = child.GetValue(VirtualChildProperty) as IVirtualChild;
            //    if (n != null)
            //    {
            //        Rect bounds = n.Bounds;
            //        child.Measure(bounds.Size);
            //    }
            //}
            if (double.IsInfinity(availableSize.Width))
            {
                return _extent;
            }
            else
            {
                return availableSize;
            }
        }

        /// <summary>
        /// Set the viewport size and raize a scroll changed event.
        /// </summary>
        /// <param name="s">The new size</param>
        private void SetViewportSize(Size s)
        {
            if (s != _viewPortSize)
            {
                _viewPortSize = s;
                OnScrollChanged();
            }
        }

        /// <summary>
        /// Calculate the size needed to display all the virtual children.
        /// </summary>
        private void CalculateExtent()
        {
            Rect extent = new Rect();

            foreach (Node node in Nodes)
            {
                extent = Rect.Union(extent, node.Rect);
            }
            
            _extent = extent.Size;
            
            double w = Math.Max(_canvas.MinWidth, _extent.Width);
            double h = Math.Max(_canvas.MinHeight, _extent.Height);
            _canvas.Width = w;
            _canvas.Height = h;

            // Make sure the backdrop covers the ViewPort bounds.
            double zoom = _scale.ScaleX;
            if (!double.IsInfinity(ViewportHeight) && !double.IsInfinity(ViewportHeight))
            {
                w = Math.Max(w, ViewportWidth / zoom);
                h = Math.Max(h, ViewportHeight / zoom);
                _backdrop.Width = w;
                _backdrop.Height = h;
            }

            if (_owner != null)
            {
                _owner.InvalidateScrollInfo();
            }

            //if (rebuild)
            //{
            //    AddVisibleRegion();
            //}
        }

        #endregion

        #region ScrollEvents


        /// <summary>
        /// Callback whenever the current TranslateTransform is changed.
        /// </summary>
        /// <param name="sender">TranslateTransform</param>
        /// <param name="e">noop</param>
        void OnTranslateChanged(object sender, EventArgs e)
        {
            OnScrollChanged();
        }

        /// <summary>
        /// Callback whenever the current ScaleTransform is changed.
        /// </summary>
        /// <param name="sender">ScaleTransform</param>
        /// <param name="e">noop</param>
        void OnScaleChanged(object sender, EventArgs e)
        {
            OnScrollChanged();
        }

        /// <summary>
        /// The visible region has changed, so we need to queue up work for dirty regions and new visible regions
        /// then start asynchronously building new visuals via StartLazyUpdate.
        /// </summary>
        void OnScrollChanged()
        {
            //Rect dirty = _visible;
            //AddVisibleRegion();
            //_nodeCollectCycle = 0;
            //_done = false;

            //Rect intersection = Rect.Intersect(dirty, _visible);
            //if (intersection == Rect.Empty)
            //{
            //    _dirtyRegions.Add(dirty); // the whole thing is dirty
            //}
            //else
            //{
            //    // Add left stripe
            //    if (dirty.Left < intersection.Left)
            //    {
            //        _dirtyRegions.Add(new Rect(dirty.Left, dirty.Top, intersection.Left - dirty.Left, dirty.Height));
            //    }
            //    // Add right stripe
            //    if (dirty.Right > intersection.Right)
            //    {
            //        _dirtyRegions.Add(new Rect(intersection.Right, dirty.Top, dirty.Right - intersection.Right, dirty.Height));
            //    }
            //    // Add top stripe
            //    if (dirty.Top < intersection.Top)
            //    {
            //        _dirtyRegions.Add(new Rect(dirty.Left, dirty.Top, dirty.Width, intersection.Top - dirty.Top));
            //    }
            //    // Add right stripe
            //    if (dirty.Bottom > intersection.Bottom)
            //    {
            //        _dirtyRegions.Add(new Rect(dirty.Left, intersection.Bottom, dirty.Width, dirty.Bottom - intersection.Bottom));
            //    }
            //}

            //StartLazyUpdate();
            InvalidateScrollInfo();
        }

        /// <summary>
        /// Tell the ScrollViewer to update the scrollbars because, extent, zoom or translate has changed.
        /// </summary>
        public void InvalidateScrollInfo()
        {
            if (_owner != null)
            {
                _owner.InvalidateScrollInfo();
            }
        }
        

        #endregion
    }
}