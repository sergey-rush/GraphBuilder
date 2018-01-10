using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Xml.Serialization;
using GraphBuilder.Shell.ViewModels;
using GraphBuilder.Shell.Views;
using ListItem = GraphBuilder.Core.ListItem;

namespace GraphBuilder.Shell.Models
{
    [SerializableAttribute()]
    [XmlType(AnonymousType = true)]
    [DisplayName("Объект")][Description("Имя объекта")]
    public class Node : Element
    {
        #region Public Properties

        private string _label;

        [Category("Вид")]
        [DisplayName("Название"), Description("Задает название")]
        public string Label
        {
            get { return _label; }
            set
            {
                _label = value;
                PositionElements();
            }
        }

        [Browsable(false)]
        public string Header { get; set; }
        [Browsable(false)]
        public string Name { get; set; }
        [Browsable(false)]
        public string Info { get; set; }
        [Browsable(false)]
        public int Index { get; set; }
        [Browsable(false)]
        public List<Card> Cards { get; set; }
        [Browsable(false)]
        public List<ListItem> Attributes { get; set; }

        #endregion

        public void SetDimension(double width, double height)
        {
            _width = width;
            _height = height;
            PositionElements();
        }

        [Category("Система")]
        [DisplayName("№"), Description("Id внешней системы")]
        public string Id { get; set; }
        [Browsable(false)]
        public Rect Rect { get; set; }
        
        private double _width;
        [Category("Размер")]
        [DisplayName("Ширина фигуры"), Description("Задает ширину фигуры")]
        public double Width
        {
            get { return _width; }
            set
            {
                if (_width != value)
                {
                    _width = value;
                    PositionElements();
                }
            }
        }

        private double _height;
        [Category("Размер")]
        [DisplayName("Высота фигуры"), Description("Задает высоту фигуры")]
        public double Height
        {
            get { return _height; }
            set
            {
                if (_height != value)
                {
                    _height = value;
                    PositionElements();
                }
            }
        }

        private void PositionElements()
        {
            if (Canvas != null)
            {
                Size = new Size(Width, Height);
                Rect = new Rect(Location, Size);
                Canvas.Width = Width;
                Canvas.Height = Height;
                border.Width = Width;
                border.Height = Height;
                shape.Width = Width;
                shape.Height = Height;

                Size textSize = MeasureText(VirtualCanvas, Label);
                double textPositionX = (Width - textSize.Width) / 2;
                //double textPositionY = (Height - textSize.Height) / 2;
                double textPositionY = (Height + (textSize.Height/2));
                textBlock.Text = Label;
                Canvas.SetLeft(textBlock, textPositionX);
                Canvas.SetTop(textBlock, textPositionY);
            }
        }

        private ShapeType _shapeType;
        [Category("Вид")]
        [DisplayName("Форма фигуры"), Description("Задает форму фигуры")]
        public ShapeType ShapeType {
            get
            {
                return _shapeType;
            }
            set
            {
                if (_shapeType != value)
                {
                    _shapeType = value;
                    SetShapeType(_shapeType);
                }
            }
        }

        public void SetShapeType(ShapeType shapeType)
        {
            if (Canvas != null)
            {
                UIElementCollection elements = Canvas.Children;

                foreach (UIElement element in elements)
                {
                    if (element is Shape)
                    {
                        MessageBox.Show("Got it");
                    }
                }

                //ellipse.Stroke = new SolidColorBrush(color);
            }
        }

        private Color _shapeColor;

        [Category("Вид")]
        [DisplayName("Цвет фигуры"), Description("Задает цвет фигуры")]
        public Color ShapeColor
        {
            get { return _shapeColor; }
            set
            {
                if (_shapeColor != value)
                {
                    _shapeColor = value;
                    SetShapeColor(_shapeColor);
                }
            }
        }

        public void SetShapeColor(Color color)
        {
            if (shape != null)
            {
                shape.Fill = new SolidColorBrush(color);
            }
        }

        private Color _borderColor;
        [Category("Вид")]
        [DisplayName("Цвет границы"), Description("Задает цвет границы фигуры")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                if (_borderColor != value)
                {
                    _borderColor = value;
                    SetBorderColor(_borderColor);
                }
            }
        }

        public void SetBorderColor(Color color)
        {
            if (shape != null)
            {
                shape.Stroke = new SolidColorBrush(color);
            }
        }

        private double _strokeThickness;
        [Category("Вид")]
        [DisplayName("Толщина границы"), Description("Задает толщину границы фигуры")]
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
            if (shape != null)
            {
                shape.StrokeThickness = thickness;
            }
        }

        private string _imagePath;
        [Category("Вид")]
        [DisplayName("Изображение"), Description("Изображение на фигуре")]
        [Editor(typeof(BrowseFileEditor), typeof(BrowseFileEditor))]
        public string ImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                if (value != _imagePath)
                {
                    _imagePath = value;
                    SetImageBrush(_imagePath);
                }
            }
        }

        public void SetImageBrush(string imagePath)
        {
            if (shape != null)
            {
                Uri uri = new Uri(imagePath);
                Image = new BitmapImage(uri);
                shape.Fill = new ImageBrush(Image);
            }
        }

        private string _videoPath;
        [Category("Вид")]
        [DisplayName("Видео"), Description("Видео на фигуре")]
        [Editor(typeof(BrowseFileEditor), typeof(BrowseFileEditor))]
        public string VideoPath
        {
            get
            {
                return _videoPath;
            }
            set
            {
                if (value != _videoPath)
                {
                    _videoPath = value;
                    SetVideoBrush(_videoPath);
                }
            }
        }

        public void SetVideoBrush(string videoPath)
        {
            if (shape != null)
            {
                Uri uri = new Uri(videoPath);
                MediaPlayer mp = new MediaPlayer();
                mp.Open(uri);
                VideoDrawing vd = new VideoDrawing();
                vd.Player = mp;
                vd.Rect = Rect;
                DrawingBrush db = new DrawingBrush(vd);
                shape.Fill = db;
                mp.Play();
            }
        }

        [Browsable(false)]
        public byte[] ImageBytes { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public BitmapImage Image { get; set; }
        
        [Browsable(false)]
        public Point ActualPoint
        {
            get { return new Point((Rect.Left + Rect.Right)/2, (Rect.Top + Rect.Bottom)/2); }
        }

        /// <summary>
        /// Contains actual node links collection 
        /// </summary>
        [Browsable(false)]
        public List<Link> Links { get; set; }

        [Browsable(false)]
        public Point Location { get; set; }
        [Browsable(false)]
        public Size Size { get; set; }

        public Node()
        {
            Attributes = new List<ListItem>();
            Links = new List<Link>();
            Cards = new List<Card>();
        }

        public void Init()
        {
            UId = Guid.NewGuid();
            Id = UId.ToString("D");
            
            //Image = new BitmapImage(new Uri("pack://application:,,,/Shell;component/Resources/Userpic.png"));
            //Image = AppFile.Default.UserImage;
            //ImageBytes = AppFile.Default.ImageBytes;
        }

        private bool _selected;

        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    
                    Bootstrap.Current.MainViewModel.SelectedNode = this;
                    ToggleBorderColor(_selected);
                }
            }
        }

        public void ToggleBorderColor(bool show)
        {
            if (show)
            {
                border.BorderBrush = new SolidColorBrush(BorderColor);
            }
            else
            {
                border.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
        }

        private UIElement _element;
        [XmlIgnore]
        [Browsable(false)]
        public UIElement Element
        {
            get { return _element; }
            set { _element = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public VirtualCanvas VirtualCanvas { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public Canvas Canvas { get; set; }

        private Shape shape;
        private Border border;
        private TextBlock textBlock;

        public UIElement CreateElement()
        {
            if (_element == null)
            {
                Canvas = new Canvas();
                Canvas.Background = new SolidColorBrush(Colors.Transparent);
                border = new Border();
                border.BorderBrush = new SolidColorBrush(Colors.Transparent);
                border.BorderThickness = new Thickness(1);
                Canvas.Children.Add(border);
                
                
                
                

                switch (ShapeType)
                {
                    case ShapeType.Ellipse:
                    {
                        shape = new Ellipse();
                        shape.StrokeThickness = StrokeThickness;
                        shape.Stroke = new SolidColorBrush(BorderColor);
                        shape.Fill = new SolidColorBrush(ShapeColor);
                        break;
                    }
                    case ShapeType.Rectangle:
                    {
                        shape = new Rectangle();
                        shape.StrokeThickness = StrokeThickness;
                        shape.Stroke = new SolidColorBrush(BorderColor);
                        shape.Fill = new SolidColorBrush(ShapeColor);
                        break;
                    }
                    case ShapeType.Image:
                    {
                        shape = new Rectangle();
                        ImagePath = OpenDialog();
                        break;
                    }
                    case ShapeType.Video:
                    {
                        shape = new Rectangle();
                        break;
                    }
                    case ShapeType.Document:
                    {
                        shape = new Rectangle();
                        break;
                    }
                    case ShapeType.Text:
                    {
                        shape = new Rectangle();
                        
                        break;
                    }
                }

                Canvas.Children.Add(shape);
                textBlock = new TextBlock();
                //textBlock.Foreground = new SolidColorBrush(Colors.Black);
                //Size textSize = MeasureText(VirtualCanvas, Label);
                //double textPositionX = (Width - textSize.Width) / 2;
                //double textPositionY = (Height - textSize.Height) / 2;
                //textBlock.Text = Label;
                Canvas.Children.Add(textBlock);
                //Canvas.SetLeft(textBlock, textPositionX);
                //Canvas.SetTop(textBlock, textPositionY);

                PositionElements();

                Canvas.MouseDown += VirtualCanvas.Shape_OnMouseDown;
                Canvas.MouseMove += VirtualCanvas.Shape_MouseMove;
                Canvas.MouseUp += VirtualCanvas.Shape_MouseUp;
                Canvas.MouseEnter += VirtualCanvas.Shape_MouseEnter;
                Canvas.MouseLeave += VirtualCanvas.Shape_MouseLeave;
                Canvas.ContextMenu = CreateContextMenu();

                if (ShapeType == ShapeType.Video)
                {
                    VideoPath = OpenDialog();
                }

                _element = Canvas;
                _element.Uid = UId.ToString("D");
            }
            return _element;
        }

        public string OpenDialog()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();
            return dlg.FileName;
        }

        public void MouseIn()
        {
            if (!Selected)
            {
                ToggleBorderColor(true);
            }
        }

        public void MouseOut()
        {
            if (!Selected)
            {
                ToggleBorderColor(false);
            }
        }

        private ContextMenu CreateContextMenu()
        {
            ContextMenu contextMenu = new ContextMenu();

            MenuItem cardMenuItem = new MenuItem();
            cardMenuItem.Header = "Карты";
            cardMenuItem.Click += CardMenuItem_Click;
            contextMenu.Items.Add(cardMenuItem);

            Separator separator = new Separator();
            contextMenu.Items.Add(separator);

            MenuItem propMenuItem = new MenuItem();
            propMenuItem.Header = "Свойства";
            propMenuItem.Click += PropMenuItem_Click;
            contextMenu.Items.Add(propMenuItem);

            return contextMenu;
        }

        private void CardMenuItem_Click(object sender, RoutedEventArgs e)
        {
            NodeWindow nodeWindow = new NodeWindow(this);
            nodeWindow.Show();
        }

        private void PropMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Bootstrap.Current.PropertyViewModel.SelectedObject = this;
        }

        public void DisposeElement()
        {
            _element = null;
        }
        
        public Size MeasureText(VirtualCanvas parent, string label)
        {
            FontFamily fontFamily = (FontFamily) parent.GetValue(TextBlock.FontFamilyProperty);
            FontStyle fontStyle = (FontStyle) parent.GetValue(TextBlock.FontStyleProperty);
            FontWeight fontWeight = (FontWeight) parent.GetValue(TextBlock.FontWeightProperty);
            FontStretch fontStretch = (FontStretch) parent.GetValue(TextBlock.FontStretchProperty);
            double fontSize = (double)parent.GetValue(TextBlock.FontSizeProperty);
            Typeface typeface = new Typeface(fontFamily, fontStyle, fontWeight, fontStretch);
            FormattedText ft = new FormattedText(label, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, typeface, fontSize, Brushes.Black);
            return new Size(ft.Width, ft.Height);
        }
    }
}