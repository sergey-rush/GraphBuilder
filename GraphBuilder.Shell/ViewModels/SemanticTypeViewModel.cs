using System.Collections.ObjectModel;
using System.Windows.Input;
using GraphBuilder.Core;


namespace GraphBuilder.Shell.ViewModels
{
    public class SemanticTypeViewModel:ViewModelBase
    {
        public SemanticTypeViewModel(SemanticType word)
        {
            Id = word.Id;
            ParentId = word.ParentId;
            Name = word.Name;
            Alias = word.Alias;
        }

        private int _id;
        public int Id
        {
            
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        private int _parentId;
        public int ParentId
        {

            get { return _parentId; }
            set
            {
                _parentId = value;
                OnPropertyChanged("ParentId");
            }
        }

        private string _name;
        public string Name
        {

            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _alias;
        public string Alias
        {

            get { return _alias; }
            set
            {
                _alias = value;
                OnPropertyChanged("Alias");
            }
        }

        private ObservableCollection<SemanticTypeViewModel> _words = new ObservableCollection<SemanticTypeViewModel>();
        public ObservableCollection<SemanticTypeViewModel> Words
        {
            get { return _words; }
            set
            {
                _words = value;
                OnPropertyChanged("Words");
            }
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    OnPropertyChanged("IsExpanded");
                }
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        private DelegateCommand _select;

        public ICommand Select
        {
            get
            {
                if (_select == null)
                {
                    _select = new DelegateCommand(OnSelect);
                }
                return _select;
            }
        }

        private void OnSelect()
        {

        }

        private DelegateCommand _remove;

        public ICommand Remove
        {
            get
            {
                if (_remove == null)
                {
                    _remove = new DelegateCommand(OnRemove);
                }
                return _remove;
            }
        }

        private void OnRemove()
        {
            if (IsSelected)
            {
                bool result = Data.Access.DeleteSemanticType(Id);
                //OnChanged(this, "myvalue");
            }
        }
    }
}