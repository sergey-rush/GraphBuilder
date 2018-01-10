using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GraphBuilder.Core;
using GraphBuilder.Shell.Models;
using GraphBuilder.Shell.Views;

namespace GraphBuilder.Shell.ViewModels
{
    public class DocumentViewModel : ViewModelBase
    {
        public DocumentViewModel(Document document)
        {
           //CanvasView canvasView = new CanvasView(document);
            //ContentControl = canvasView;

            //Title = "СвойстваTitle";
            //Name = "СвойстваName";
            ContentId = Guid.NewGuid();
            CanClose = true;
            IsClosed = false;
            //UserInterface = canvasView;
        }
        
        //private ContentControl _contentControl = new ContentControl();

        //public ContentControl ContentControl
        //{
        //    get { return _contentControl; }
        //    set
        //    {
        //        _contentControl = value;
        //        OnPropertyChanged("ContentControl");
        //    }
        //}
    }
}