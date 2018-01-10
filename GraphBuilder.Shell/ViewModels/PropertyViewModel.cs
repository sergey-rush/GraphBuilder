using System;
using System.Windows.Input;
using GraphBuilder.Core;
using GraphBuilder.Shell.Views;

namespace GraphBuilder.Shell.ViewModels
{
    public class PropertyViewModel : ViewModelBase
    {
        public PropertyViewModel()
        {
            Title = "Свойства";
            Name = "СвойстваName";
            ContentId = Guid.Parse("e2b0173c-84d4-4ef4-ad37-087aed6cf359");
            CanClose = true;
            IsClosed = false;

            PropertyView propertyView = new PropertyView();
            propertyView.DataContext = this;
            UserInterface = propertyView;
        }

        private object _selectedObject;

        public object SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                _selectedObject = value;
                OnPropertyChanged("SelectedObject");
            }
        }
    }
}