using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GraphBuilder.Shell.ViewModels;


namespace GraphBuilder.Shell.Views
{
    public partial class ImageWindow : Window
    {
        ImageViewModel _imageViewModel;
        public ImageWindow()
        {
            _imageViewModel = new ImageViewModel();
            DataContext = _imageViewModel;
            InitializeComponent();
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
