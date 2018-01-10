using System.Windows;
using System.Windows.Controls;
using GraphBuilder.Shell.ViewModels;
using GraphBuilder.Shell.Views;

namespace GraphBuilder.Shell.Models
{
    public class PanesStyleSelector : StyleSelector
    {
        public Style CanvasStyle { get; set; }

        public Style ContStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is CanvasViewModel)
            {
                return CanvasStyle;
            }
            else
            {
                return ContStyle;
            }
            return base.SelectStyle(item, container);
        }
    }
}