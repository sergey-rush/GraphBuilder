using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace GraphBuilder.Core
{
    //public delegate void Changed(object sender, string myValue);
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region AvalonWindow

        public Guid ContentId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        
        private UserControl _userInterface;

        public UserControl UserInterface
        {
            get
            {
                return _userInterface;
            }
            set
            {
                _userInterface = value;
                OnPropertyChanged("UserInterface");
            }
        }



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
    }
}