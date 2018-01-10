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
using GraphBuilder.Shell.Models;
using GraphBuilder.Shell.ViewModels;

namespace GraphBuilder.Shell.Views
{
    public partial class CanvasView : UserControl
    {
        public CanvasView()
        {
            InitializeComponent();
        }
        //private CanvasViewModel _canvasViewModel;
        //public CanvasView(Document document)
        //{
        //    InitializeComponent();
        //    _canvasViewModel = new CanvasViewModel(Graph, document);
        //    DataContext = _canvasViewModel;
        //}
    }
}
