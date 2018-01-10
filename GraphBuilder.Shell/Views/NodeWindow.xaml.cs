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
using System.Windows.Shapes;
using GraphBuilder.Shell.Models;
using GraphBuilder.Shell.ViewModels;


namespace GraphBuilder.Shell.Views
{
    public partial class NodeWindow : Window
    {
        public NodeWindow(Node node)
        {
            InitializeComponent();
            DataContext = new NodeViewModel(node);
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
