using System;
using GraphBuilder.Core;
using GraphBuilder.Shell.Views;

namespace GraphBuilder.Shell.ViewModels
{
    public class WizardViewModel:ViewModelBase
    {
        public WizardWindow WizardWindow;
        public WizardViewModel()
        {
            WizardWindow = new WizardWindow();
            WizardWindow.DataContext = this;
        }

        private string _wizardTitle = String.Empty;

        public string WizardTitle
        {
            get { return _wizardTitle; }
            set
            {
                if (_wizardTitle != value)
                {
                    _wizardTitle = value;
                    OnPropertyChanged("WizardTitle");
                }
            }
        }

        private ViewModelBase _contentWindow;

        public ViewModelBase ContentWindow
        {
            get { return _contentWindow; }
            set
            {
                _contentWindow = value;
                OnPropertyChanged("ContentWindow");
            }
        }
    }
}