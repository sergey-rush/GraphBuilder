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
    public partial class SemanticTypesWindow : Window
    {
        SemanticTypesViewModel _dataTypeViewModel;
        public SemanticTypesWindow()
        {
            _dataTypeViewModel = new SemanticTypesViewModel();
            DataContext = _dataTypeViewModel;
            InitializeComponent();
        }
    }
}
