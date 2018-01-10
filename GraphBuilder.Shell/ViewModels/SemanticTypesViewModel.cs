using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using GraphBuilder.Core;
using System.Linq;
using System.Windows.Input;


namespace GraphBuilder.Shell.ViewModels
{
    public class SemanticTypesViewModel:ViewModelBase
    {
        private List<SemanticType> _words;
        
        public SemanticTypesViewModel()
        {
            Bind();

            //OnChanged += SemanticTypesViewModel_OnChange;
        }

        private void SemanticTypesViewModel_OnChange(object sender, string myValue)
        {
            throw new NotImplementedException();
        }

        public void Bind()
        {
            Parents.Clear();
            _words = Data.Access.GetSemanticTypes();
            foreach (SemanticType type in _words)
            {
                SemanticTypeViewModel parent = new SemanticTypeViewModel(type);
                
                AddNode(parent);
                if (parent.ParentId == 0)
                {
                    parent.IsExpanded = true;
                    Parents.Add(parent);
                }
            }
        }

        private void AddNode(SemanticTypeViewModel st)
        {
            foreach (SemanticType w in _words)
            {
                if (st.Id == w.ParentId)
                {
                    SemanticTypeViewModel child = new SemanticTypeViewModel(w);
                    st.Words.Add(child);
                    AddNode(child);
                }
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

        private DelegateCommand _save;

        public ICommand Save
        {
            get
            {
                if (_save == null)
                {
                    _save = new DelegateCommand(OnSave, CanSave);
                }
                return _save;
            }
        }

        private void OnSave()
        {
            SemanticType st = new SemanticType
            {
                Name = Name,
                Alias = Alias,
                ParentId = SelectedType.Id
            };
            Data.Access.InsertSemanticType(st);
            Bind();
        }

        private bool CanSave()
        {
            bool result = false;
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Alias)&& SelectedType!=null)
            {
                result = true;
            }
            return result;
        }

        public SemanticTypeViewModel SelectedType
        {
            get { return GetSelectedType(); }
        }

        private SemanticTypeViewModel GetSelectedType()
        {
            SemanticTypeViewModel selected = null;
            foreach (SemanticTypeViewModel semanticTypeViewModel in Parents)
            {
                if (semanticTypeViewModel.IsSelected)
                {
                    return semanticTypeViewModel;
                }
                selected = Deepen(semanticTypeViewModel);
                if (selected!=null&&selected.IsSelected)
                {
                    break;
                }
            }
            return selected;
        }

        private SemanticTypeViewModel Deepen(SemanticTypeViewModel stvm)
        {
            SemanticTypeViewModel selected = null;
            foreach (SemanticTypeViewModel semanticTypeViewModel in stvm.Words)
            {
                if (semanticTypeViewModel.IsSelected)
                {
                    return semanticTypeViewModel;
                }
                selected = Deepen(semanticTypeViewModel);
                if (selected != null && selected.IsSelected)
                {
                    return selected;
                }
            }
            return selected;
        }

        private ObservableCollection<SemanticTypeViewModel> _parents = new ObservableCollection<SemanticTypeViewModel>();
        public ObservableCollection<SemanticTypeViewModel> Parents
        {
            get { return _parents; }
            set
            {
                _parents = value;
                OnPropertyChanged("Parents");
            }
        }
    }
}