using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GraphBuilder.Core;
using GraphBuilder.Shell.Models;
using GraphBuilder.Shell.Views;
using Xceed.Wpf.AvalonDock.Layout;

namespace GraphBuilder.Shell.ViewModels
{
    /// <summary>
    /// Upper level class. You cannot call anything from this class
    /// </summary>
    public class Bootstrap
    {
        private static Bootstrap _current;

        public static Bootstrap Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new Bootstrap();
                }
                return _current;
            }
        }

        public Bootstrap()
        {
            MouseMode = MouseMode.Select;
            SettingsViewModel = new SettingsViewModel();
            MainViewModel = new MainViewModel();
            PropertyViewModel = new PropertyViewModel();
            ContextViewModel = new ContextViewModel();
            LinksViewModel = new LinksViewModel();
            ImageViewModel = new ImageViewModel();

            //ContextView contextView = new ContextView();
            //contextView.DataContext = ContextViewModel;
            //MainViewModel.Anchorables.Add(contextView);

            MainViewModel.Anchorables.Add(ImageViewModel);
            MainViewModel.Anchorables.Add(LinksViewModel);
            MainViewModel.Anchorables.Add(ContextViewModel);
            MainViewModel.Anchorables.Add(PropertyViewModel);

            //MainViewModel.Anchorables.Add(ContextViewModel);
            //MainViewModel.Anchorables.Add(PropertyViewModel);
            //MainViewModel.Anchorables.Add(ImageViewModel);
            Init();
        }

        private void Init()
        {
            Document document = new Document();
            CreateDocument(document);
        }

        public MouseMode MouseMode { get; set; }
        
        public PropertyViewModel PropertyViewModel { get; private set; }
        public MainViewModel MainViewModel { get; private set; }
        public SettingsViewModel SettingsViewModel { get; private set; }
        public ContextViewModel ContextViewModel { get; private set; }
        public LinksViewModel LinksViewModel { get; private set; }
        public ImageViewModel ImageViewModel { get; private set; }

        //public void CreateDocument(Document document)
        //{
        //    DocumentViewModel documentViewModel = new DocumentViewModel(document);
        //    int count = MainViewModel.Documents.Count;
        //    documentViewModel.Title = string.Format("Документ{0}", count + 1);
        //    MainViewModel.Documents.Add(documentViewModel);
        //    MainViewModel.ActiveContent = documentViewModel;
        //}

        public void CreateDocument(Document document)
        {
            document.DocumentMode = DocumentMode.Auto;
            CanvasViewModel canvasViewModel = new CanvasViewModel(document);
            int count = MainViewModel.Documents.Count;
            canvasViewModel.Title = string.Format("Документ{0}", count + 1);
            MainViewModel.Documents.Add(canvasViewModel);
            MainViewModel.ActiveContent = canvasViewModel;
        }

        private Thumb _selectedImage;
        public Thumb SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                _selectedImage = value;
            }
        }
    }
}