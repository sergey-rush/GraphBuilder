using System.Windows;
using System.Windows.Controls;
using GraphBuilder.Shell.ViewModels;

namespace GraphBuilder.Shell.Models
{
    public class CanvasContextMenu
    {
        private VirtualCanvas _canvas;
        public CanvasContextMenu(VirtualCanvas canvas)
        {
            _canvas = canvas;
        }


        public ContextMenu CreateContextMenu()
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem addMenuItem = new MenuItem();
            addMenuItem.Header = "Добавить";

            MenuItem rectangleMenuItem = new MenuItem();
            rectangleMenuItem.Header = "Квадрат";
            rectangleMenuItem.Tag = ShapeType.Rectangle;
            rectangleMenuItem.Click += AddMenuItem_Click;
            addMenuItem.Items.Add(rectangleMenuItem);

            MenuItem ellipseMenuItem = new MenuItem();
            ellipseMenuItem.Header = "Круг";
            ellipseMenuItem.Tag = ShapeType.Ellipse;
            ellipseMenuItem.Click += AddMenuItem_Click;
            addMenuItem.Items.Add(ellipseMenuItem);

            MenuItem textMenuItem = new MenuItem();
            textMenuItem.Header = "Текст";
            textMenuItem.Tag = ShapeType.Text;
            textMenuItem.Click += AddMenuItem_Click;
            addMenuItem.Items.Add(textMenuItem);

            MenuItem imageMenuItem = new MenuItem();
            imageMenuItem.Header = "Изображение";
            imageMenuItem.Tag = ShapeType.Image;
            imageMenuItem.Click += AddMenuItem_Click;
            addMenuItem.Items.Add(imageMenuItem);

            MenuItem videoMenuItem = new MenuItem();
            videoMenuItem.Header = "Видео";
            videoMenuItem.Tag = ShapeType.Video;
            videoMenuItem.Click += AddMenuItem_Click;
            addMenuItem.Items.Add(videoMenuItem);

            MenuItem documentMenuItem = new MenuItem();
            documentMenuItem.Header = "Документ";
            documentMenuItem.Tag = ShapeType.Document;
            documentMenuItem.Click += AddMenuItem_Click;
            addMenuItem.Items.Add(documentMenuItem);

            contextMenu.Items.Add(addMenuItem);
            Separator separator = new Separator();
            contextMenu.Items.Add(separator);

            MenuItem propMenuItem = new MenuItem();
            propMenuItem.Header = "Свойства";
            propMenuItem.Click += PropMenuItem_Click;
            contextMenu.Items.Add(propMenuItem);
            return contextMenu;
        }



        private void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            ShapeType shapeType = (ShapeType)menuItem.Tag;
            Point point = _canvas.LastPoint;
            Node node = NodeFactory.CreateNode(point, shapeType);
            _canvas.AddNode(node);
        }

        private void PropMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Bootstrap.Current.PropertyViewModel.SelectedObject = _canvas.Document;
        }
        
    }
}