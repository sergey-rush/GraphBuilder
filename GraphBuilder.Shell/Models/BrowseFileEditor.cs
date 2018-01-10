﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace GraphBuilder.Shell.Models
{
    public class BrowseFileEditor : ITypeEditor
    {
        public FrameworkElement ResolveEditor(PropertyItem propertyItem)
        {
            Grid panel = new Grid();
            panel.ColumnDefinitions.Add(new ColumnDefinition());
            panel.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = GridLength.Auto
            });

            TextBox textBox = new TextBox();
            textBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            Binding binding = new Binding("Value"); //bind to the Value property of the PropertyItem
            binding.Source = propertyItem;
            binding.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(textBox, TextBox.TextProperty, binding);

            Button button = new Button();
            button.Content = "...";
            button.Tag = propertyItem;
            button.Click += button_Click;
            Grid.SetColumn(button, 1);

            panel.Children.Add(textBox);
            panel.Children.Add(button);

            return panel;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            PropertyItem item = ((Button) sender).Tag as PropertyItem;
            if (null == item)
            {
                return;
            }

            OpenDialog(item);
        }

        public void OpenDialog(PropertyItem item)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();
            item.Value = dlg.FileName;
        }
    }
}
    