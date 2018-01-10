using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using GraphBuilder.Core;
using GraphBuilder.Shell.Models;
using GraphBuilder.Shell.Views;

namespace GraphBuilder.Shell.ViewModels
{
    public class StartPageViewModel : ViewModelBase
    {
        public StartPageViewModel()
        {
            StartPageView startPageView = new StartPageView();
            Title = "Начать";
            ContentControl = startPageView;
            Populate();
        }

        #region AvalonWindow

        public Guid Id { get; set; }
        public string Title { get; set; }

        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new DelegateCommand(OnClose);
                return _closeCommand;
            }
        }

        public void OnClose()
        {
            IsClosed = true;
        }

        private bool _isClosed;

        public bool IsClosed
        {
            get { return _isClosed; }
            set
            {
                if (_isClosed != value)
                {
                    _isClosed = value;
                    OnPropertyChanged("IsClosed");
                }
            }
        }

        private bool _canClose;

        public bool CanClose
        {
            get { return _canClose; }
            set
            {
                if (_canClose != value)
                {
                    _canClose = value;
                    OnPropertyChanged("CanClose");
                }
            }
        }

        #endregion

        private ContentControl _contentControl = new ContentControl();

        public ContentControl ContentControl
        {
            get
            {
                return _contentControl;
            }
            set
            {
                _contentControl = value;
                OnPropertyChanged("ContentControl");
            }
        }
        
        public void Populate()
        {
            for (int i = 1; i < 15; i++)
            {
                Thumb item = new Thumb();
                item.Name = String.Format("Документ{0}", i);
                Documents.Add(item);
            }
        }

        private ObservableCollection<Thumb> _documents = new ObservableCollection<Thumb>();

        public ObservableCollection<Thumb> Documents
        {
            get { return _documents; }
            set
            {
                _documents = value;
                OnPropertyChanged("Documents");
            }
        }
    }
}