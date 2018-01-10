using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using GraphBuilder.Core;
using GraphBuilder.Shell.Models;

namespace GraphBuilder.Shell.ViewModels
{
    public class GroupDataViewModel : ViewModelBase
    {
        private DataTable _data;
        private WizardViewModel _wizardViewModel;

        public GroupDataViewModel(WizardViewModel wizardViewModel, DataTable data)
        {
            _data = data;
            Columns = data.Columns;
            SelectedColumn = data.Columns[0];
            ItemData = _data.DefaultView;
            _wizardViewModel = wizardViewModel;
            _wizardViewModel.ContentWindow = this;
            _wizardViewModel.WizardTitle = "Выбор модели графа";
        }

        private DataView _itemData;

        public DataView ItemData
        {
            get { return _itemData; }
            set
            {
                _itemData = value;
                OnPropertyChanged("ItemData");
            }
        }

        private DataColumnCollection _columns;

        public DataColumnCollection Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                OnPropertyChanged("Columns");
            }
        }

        private DataColumn _selectedColumn;

        public DataColumn SelectedColumn
        {
            get { return _selectedColumn; }
            set
            {
                _selectedColumn = value;
                OnPropertyChanged("SelectedColumn");
            }
        }

        private DelegateCommand _submit;

        public ICommand Submit
        {
            get
            {
                if (_submit == null)
                {
                    _submit = new DelegateCommand(OnSubmit);
                }
                return _submit;
            }
        }

        private void OnSubmit()
        {
            Document document = new Document();
            DocumentViewModel documentViewModel = new DocumentViewModel(document);
            documentViewModel.Title = String.Format("Документ{0}", Bootstrap.Current.MainViewModel.Documents.Count + 1);
            Bootstrap.Current.MainViewModel.Documents.Add(documentViewModel);
            Bootstrap.Current.MainViewModel.ActiveContent = documentViewModel;
            _wizardViewModel.WizardWindow.Close();
        }

        private DelegateCommand _back;

        public ICommand Back
        {
            get
            {
                if (_back == null)
                {
                    _back = new DelegateCommand(OnBack);
                }
                return _back;
            }
        }

        private void OnBack()
        {
        }
    }
}