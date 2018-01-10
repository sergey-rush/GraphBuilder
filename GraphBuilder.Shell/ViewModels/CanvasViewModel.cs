using GraphBuilder.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GraphBuilder.Shell.Models;
using GraphBuilder.Shell.Views;

namespace GraphBuilder.Shell.ViewModels
{
    public class CanvasViewModel : ViewModelBase
    {
        //public CanvasViewModel(VirtualCanvas virtualCanvas, Document document)
        //{
        //    ContentControl = virtualCanvas;
        //    virtualCanvas.Document = document;
        //    Document = document;
        //    Init();CanvasView
        //}

        public CanvasViewModel(Document document)
        {
            ContentId = Guid.NewGuid();
            CanClose = true;
            IsClosed = false;
            CanvasView canvasView = new CanvasView();
            canvasView.DataContext = this;
            ContentControl = canvasView.Graph;
            ContentControl.Document = document;
            Document = document;
            Init();
            
            UserInterface = canvasView;
        }

        private MapZoom zoom;
        private Pan pan;
        private RectangleSelectionGesture rectZoom;
        private AutoScroll autoScroll;
        public Document Document { get; set; }
        public VirtualCanvas ContentControl { get; set; }

        private void Init()
        {
            if (Document.DocumentMode == DocumentMode.Auto)
            {
                List<Node> nodes = NodeFactory.CreateNodes(30);
                ContentControl.PopulateNodes(nodes);
                LinkFactory.Nodes = nodes;
                ContentControl.PopulateLinks(LinkFactory.CreateLinks(60));
            }

            Canvas target = ContentControl.ContentCanvas;
            zoom = new MapZoom(target);
            pan = new Pan(target, zoom);
            rectZoom = new RectangleSelectionGesture(target, zoom, ModifierKeys.Control);
            rectZoom.ZoomSelection = true;
            autoScroll = new AutoScroll(target, zoom);
            zoom.ZoomChanged += new EventHandler(OnZoomChanged);

            ContentControl.VisualsChanged += new EventHandler<VisualChangeEventArgs>(OnVisualsChanged);
            //ZoomSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(OnZoomSliderValueChanged);

            ContentControl.Scale.Changed += new EventHandler(OnScaleChanged);
            ContentControl.Translate.Changed += new EventHandler(OnScaleChanged);

            //ContentControl.Background = new SolidColorBrush(Color.FromRgb(0xd0, 0xd0, 0xd0));
            ContentControl.Background = new SolidColorBrush(Colors.White);
            ContentControl.ContentCanvas.Background = Brushes.White;
            Document.Nodes = ContentControl.Nodes;
            Document.Links = ContentControl.Links;
        }

        private void OnScaleChanged(object sender, EventArgs e)
        {
            // Make the grid lines get thinner as you zoom in
            //double t = _gridLines.StrokeThickness = 0.1 / grid.Scale.ScaleX;
            //grid.Backdrop.BorderThickness = new Thickness(t);
        }

        private void OnZoomChanged(object sender, EventArgs e)
        {
            //if (ZoomSlider.Value != zoom.Zoom)
            //{
            //    ZoomSlider.Value = zoom.Zoom;
            //}
        }

        private void OnVisualsChanged(object sender, VisualChangeEventArgs e)
        {
            //if (_animateStatus)
            //{
            //    StatusText.Text = string.Format(CultureInfo.InvariantCulture, "{0} live visuals of {1} total", grid.LiveVisualCount, _totalVisuals);

            //    int tick = Environment.TickCount;
            //    if (e.Added != 0 || e.Removed != 0)
            //    {
            //        addedPerSecond += e.Added;
            //        removedPerSecond += e.Removed;
            //        if (tick > lastTick + 100)
            //        {
            //            Created.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(
            //                Math.Min(addedPerSecond, 450),
            //                new Duration(TimeSpan.FromMilliseconds(100))));
            //            CreatedLabel.Text = addedPerSecond.ToString(CultureInfo.InvariantCulture) + " created";
            //            addedPerSecond = 0;

            //            Destroyed.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(
            //                Math.Min(removedPerSecond, 450),
            //                new Duration(TimeSpan.FromMilliseconds(100))));
            //            DestroyedLabel.Text = removedPerSecond.ToString(CultureInfo.InvariantCulture) + " disposed";
            //            removedPerSecond = 0;
            //        }
            //    }
            //    if (tick > lastTick + 1000)
            //    {
            //        lastTick = tick;
            //    }
            //}
        }

        

        private double _sliderValue = 1;

        public double SliderValue
        {
            get { return _sliderValue; }
            set
            {
                _sliderValue = value;
                zoom.Zoom = _sliderValue;
                OnPropertyChanged("SliderValue");
            }
        }

        private DelegateCommand _select;

        public ICommand Select
        {
            get
            {
                if (_select == null)
                {
                    _select = new DelegateCommand(OnSelect);
                }
                return _select;
            }
        }

        public void OnSelect()
        {
            ContentControl.SetCursor(MouseMode.Select);
        }

        private DelegateCommand _multiSelect;

        public ICommand MultiSelect
        {
            get
            {
                if (_multiSelect == null)
                {
                    _multiSelect = new DelegateCommand(OnMultiSelect);
                }
                return _multiSelect;
            }
        }

        public void OnMultiSelect()
        {
            ContentControl.SetCursor(MouseMode.MultiSelect);
        }

        private DelegateCommand _showTable;

        public ICommand ShowTable
        {
            get
            {
                if (_showTable == null)
                {
                    _showTable = new DelegateCommand(OnShowTable);
                }
                return _showTable;
            }
        }

        public void OnShowTable()
        {
            //MessageBox.Show("OnShowTable");
            ShowGridLines = true;
        }

        private double _tileWidth = 50;
        private double _tileHeight = 50;
        private double _tileMargin = 10;
        private Polyline _gridLines = new Polyline();
        private bool _showGridLines;
        public bool ShowGridLines
        {
            get { return _showGridLines; }
            set
            {
                _showGridLines = value;
                if (value)
                {
                    double width = _tileWidth + _tileMargin;
                    double height = _tileHeight + _tileMargin;

                    double numTileToAccumulate = 16;

                    Polyline gridCell = _gridLines;
                    gridCell.Margin = new Thickness(_tileMargin);
                    gridCell.Stroke = Brushes.Blue;
                    gridCell.StrokeThickness = 0.1;
                    gridCell.Points = new PointCollection(new Point[]
                    {
                        new Point(0, height - 0.1),
                        new Point(width - 0.1, height - 0.1), new Point(width - 0.1, 0)
                    });
                    VisualBrush gridLines = new VisualBrush(gridCell);
                    gridLines.TileMode = TileMode.Tile;
                    gridLines.Viewport = new Rect(0, 0, 1.0 / numTileToAccumulate, 1.0 / numTileToAccumulate);
                    gridLines.AlignmentX = AlignmentX.Center;
                    gridLines.AlignmentY = AlignmentY.Center;

                    VisualBrush outerVB = new VisualBrush();
                    Rectangle outerRect = new Rectangle();
                    outerRect.Width = 10.0; //can be any size
                    outerRect.Height = 10.0;
                    outerRect.Fill = gridLines;
                    outerVB.Visual = outerRect;
                    outerVB.Viewport = new Rect(0, 0,
                        width * numTileToAccumulate, height * numTileToAccumulate);
                    outerVB.ViewportUnits = BrushMappingMode.Absolute;
                    outerVB.TileMode = TileMode.Tile;

                    ContentControl.Backdrop.Background = outerVB;

                    Border border = ContentControl.Backdrop;
                    border.BorderBrush = Brushes.Blue;
                    border.BorderThickness = new Thickness(0.1);
                    ContentControl.ContentCanvas.InvalidateVisual();
                }
                else
                {
                    ContentControl.Backdrop.Background = null;
                }
            }
        }

        private DelegateCommand _drag;

        public ICommand Drag
        {
            get
            {
                if (_drag == null)
                {
                    _drag = new DelegateCommand(OnDrag);
                }
                return _drag;
            }
        }

        public void OnDrag()
        {
            ContentControl.SetCursor(MouseMode.Dragging);
        }

        private DelegateCommand _resize;

        public ICommand Resize
        {
            get
            {
                if (_resize == null)
                {
                    _resize = new DelegateCommand(OnResize);
                }
                return _resize;
            }
        }

        public void OnResize()
        {
            ContentControl.SetCursor(MouseMode.Resize);
        }

        private DelegateCommand _drawLink;

        public ICommand DrawLink
        {
            get
            {
                if (_drawLink == null)
                {
                    _drawLink = new DelegateCommand(OnDrawLink);
                }
                return _drawLink;
            }
        }

        public void OnDrawLink()
        {
            ContentControl.SetCursor(MouseMode.DrawLink);
            Bootstrap.Current.MainViewModel.SelectedNode = null;
        }

        private DelegateCommand _drawNode;

        public ICommand DrawNode
        {
            get
            {
                if (_drawNode == null)
                {
                    _drawNode = new DelegateCommand(OnDrawNode);
                }
                return _drawNode;
            }
        }

        public void OnDrawNode()
        {
            ContentControl.SetCursor(MouseMode.DrawNode);
            Bootstrap.Current.MainViewModel.SelectedNode = null;

            //DataList dataList = new DataList();
            //ContentControl.Populate(dataList.GetNodeList(5));
        }

        private DelegateCommand _fit;

        public ICommand Fit
        {
            get
            {
                if (_fit == null)
                {
                    _fit = new DelegateCommand(OnFit);
                }
                return _fit;
            }
        }

        public void OnFit()
        {
            double scaleX = ContentControl.ViewportWidth / ContentControl.Extent.Width;
            double scaleY = ContentControl.ViewportHeight / ContentControl.Extent.Height;
            zoom.Zoom = Math.Min(scaleX, scaleY);
            zoom.Offset = new Point(0, 0);
        }

        private DelegateCommand _openDocument;

        public ICommand OpenDocument
        {
            get
            {
                if (_openDocument == null)
                {
                    _openDocument = new DelegateCommand(OnOpenDocument);
                }
                return _openDocument;
            }
        }

        public void OnOpenDocument()
        {
            MessageBox.Show("OnOpenDocument");
        }
    }
}