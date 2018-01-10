using System.Windows;
using System.Windows.Controls;
using GraphBuilder.Shell.ViewModels;
using Xceed.Wpf.AvalonDock.Layout;

namespace GraphBuilder.Shell.Models
{
    public class PanesTemplateSelector : DataTemplateSelector
    {
        public PanesTemplateSelector()
        {

        }

        public DataTemplate CanvasViewTemplate { get; set; }

        public DataTemplate ContViewTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            LayoutContent itemAsLayoutContent = item as LayoutContent;

            if (item is CanvasViewModel)
            {
                return CanvasViewTemplate;
            }
            if (item is PropertyViewModel)
            {
                return ContViewTemplate;
            }
            if (item is ContextViewModel)
            {
                return ContViewTemplate;
            }
            if (item is LinksViewModel)
            {
                return ContViewTemplate;
            }
            if (item is ImageViewModel)
            {
                return ContViewTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}