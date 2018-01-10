using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using GraphBuilder.Shell.Models;
using GraphBuilder.Shell.ViewModels;
using Microsoft.Windows.Controls.Ribbon;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace GraphBuilder.Shell
{
    public partial class MainWindow : RibbonWindow
    {
        private MainViewModel _mainViewModel;
        public MainWindow()
        {
            _mainViewModel = Bootstrap.Current.MainViewModel;
            DataContext = _mainViewModel;
            InitializeComponent();
            _mainViewModel.ShellWindow = this;
        }

        private void DockingManager_OnLoaded(object sender, RoutedEventArgs e)
        {
            //IconsViewTab.ToggleAutoHide();
            //// You might want to do this to get a reasonable height
            //var root1 = (LayoutAnchorablePane)IconsViewTab.Parent;
            ////root.DockHeight = new GridLength(300);
            //root1.DockWidth = new GridLength(300);

            //ContextViewTab.ToggleAutoHide();
            //var root2 = (LayoutAnchorablePane)ContextViewTab.Parent;
            //root2.DockWidth = new GridLength(200);

            //LinksViewTab.ToggleAutoHide();
            //var root3 = (LayoutAnchorablePane)LinksViewTab.Parent;
            //root3.DockWidth = new GridLength(200);
        }
        
        private void OnLayoutRootPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //var activeContent = ((LayoutRoot) sender).ActiveContent;
            //if (e.PropertyName == "UserInterface")
            //{
                //Debug.WriteLine(string.Format("ActiveContent-> {0}", activeContent));
            //}
        }

        private void OnLoadLayout(object sender, RoutedEventArgs e)
        {
            //var currentContentsList =
            //    dockManager.Layout.Descendents().OfType<LayoutContent>().Where(c => c.ContentId != null).ToArray();

            //string fileName = (sender as MenuItem).Header.ToString();
            //var serializer = new XmlLayoutSerializer(dockManager);
            ////serializer.LayoutSerializationCallback += (s, args) =>
            ////    {
            ////        var prevContent = currentContentsList.FirstOrDefault(c => c.ContentId == args.Model.ContentId);
            ////        if (prevContent != null)
            ////            args.Content = prevContent.Content;
            ////    };
            //using (var stream = new StreamReader(string.Format(@".\AvalonDock_{0}.config", fileName)))
            //    serializer.Deserialize(stream);
        }

        private void OnSaveLayout(object sender, RoutedEventArgs e)
        {
            //string fileName = (sender as MenuItem).Header.ToString();
            //var serializer = new XmlLayoutSerializer(dockManager);
            //using (var stream = new StreamWriter(string.Format(@".\AvalonDock_{0}.config", fileName)))
            //    serializer.Serialize(stream);
        }

        private void OnShowWinformsWindow(object sender, RoutedEventArgs e)
        {
            //var winFormsWindow =
            //    dockManager.Layout.Descendents().OfType<LayoutAnchorable>().Single(a => a.ContentId == "WinFormsWindow");
            //if (winFormsWindow.IsHidden)
            //    winFormsWindow.Show();
            //else if (winFormsWindow.IsVisible)
            //    winFormsWindow.IsActive = true;
            //else
            //    winFormsWindow.AddToLayout(dockManager, AnchorableShowStrategy.Bottom | AnchorableShowStrategy.Most);
        }

        private void AddTwoDocuments_click(object sender, RoutedEventArgs e)
        {
            //LayoutDocumentPane firstDocumentPane = dockManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            //ObservableCollection<LayoutContent> df = firstDocumentPane.Children;
            //if (firstDocumentPane != null)
            //{
            //    LayoutDocument doc = new LayoutDocument();
            //    doc.Title = "Test1";
            //    firstDocumentPane.Children.Add(doc);

            //    LayoutDocument doc2 = new LayoutDocument();
            //    doc2.Title = "Test2";
            //    firstDocumentPane.Children.Add(doc2);
            //}

            //var leftAnchorGroup = dockManager.Layout.LeftSide.Children.FirstOrDefault();
            //if (leftAnchorGroup == null)
            //{
            //    leftAnchorGroup = new LayoutAnchorGroup();
            //    dockManager.Layout.LeftSide.Children.Add(leftAnchorGroup);
            //}

            //leftAnchorGroup.Children.Add(new LayoutAnchorable() {Title = "New Anchorable"});

        }

        private void OnShowToolWindow1(object sender, RoutedEventArgs e)
        {
            //var toolWindow1 =
            //    dockManager.Layout.Descendents().OfType<LayoutAnchorable>().Single(a => a.ContentId == "toolWindow1");
            //if (toolWindow1.IsHidden)
            //    toolWindow1.Show();
            //else if (toolWindow1.IsVisible)
            //    toolWindow1.IsActive = true;
            //else
            //    toolWindow1.AddToLayout(dockManager, AnchorableShowStrategy.Bottom | AnchorableShowStrategy.Most);
        }

        private void dockManager_DocumentClosing(object sender, DocumentClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close the document?", "AvalonDock Sample",MessageBoxButton.YesNo) == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void OnDumpToConsole(object sender, RoutedEventArgs e)
        {
            // Uncomment when TRACE is activated on AvalonDock project
            // dockManager.Layout.ConsoleDump(0);
        }

        private void OnReloadManager(object sender, RoutedEventArgs e)
        {
        }

        //private void OnUnloadManager(object sender, RoutedEventArgs e)
        //{
        //    if (layoutRoot.Children.Contains(dockManager))
        //        layoutRoot.Children.Remove(dockManager);
        //}

        //private void OnLoadManager(object sender, RoutedEventArgs e)
        //{
        //    if (!layoutRoot.Children.Contains(dockManager))
        //        layoutRoot.Children.Add(dockManager);
        //}

        private void OnToolWindow1Hiding(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to hide this tool?", "AvalonDock",MessageBoxButton.YesNo) == MessageBoxResult.No)
                e.Cancel = true;
        }


        //private bool _shutdown;
        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var serializer = new XmlLayoutSerializer(DockingManager);
            serializer.LayoutSerializationCallback += (s, args) =>
            {
                args.Content = args.Content;
            };

            if (File.Exists(@".\GraphBuilder.config"))
                serializer.Deserialize(@".\GraphBuilder.config");
        }

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            XmlLayoutSerializer serializer = new XmlLayoutSerializer(DockingManager);
            // Remove document from layout
            serializer.Manager.DocumentsSource = null;
            serializer.Serialize(@".\GraphBuilder.config");
        }
    }
}
