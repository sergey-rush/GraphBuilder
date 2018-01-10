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
using GraphBuilder.Shell.Views;

namespace GraphBuilder.Shell.ViewModels
{
    public class ImportViewModel : ViewModelBase
    {
        WizardViewModel _wizardViewModel;
        public ImportViewModel(WizardViewModel wizardViewModel, List<string> lines)
        {
            Lines = lines;

            wizardViewModel.ContentWindow = this;
            wizardViewModel.WizardTitle = "Импорт из файла";
            _wizardViewModel = wizardViewModel;

            ListItem semicolon = new ListItem("Точка с запятой ( ; )", ";");
            Delimiters.Add(semicolon);
            ListItem verticalBar = new ListItem("Вертикальная черта ( | )", "|");
            Delimiters.Add(verticalBar);
            ListItem tab = new ListItem("Табулятор ( t )", "\t");
            Delimiters.Add(tab);
            ListItem comma = new ListItem("Запятая ( , )", ",");
            Delimiters.Add(comma);
            ListItem colon = new ListItem("Двоеточие ( : )", ":");
            Delimiters.Add(colon);
            SelectedDelimiter = Delimiters[0];

            SetData();
        }
        
        private List<ListItem> _delimiters = new List<ListItem>();

        public List<ListItem> Delimiters
        {
            get { return _delimiters; }
            set
            {
                _delimiters = value;
                OnPropertyChanged("Delimiters");
            }
        }

        private ListItem _selectedDelimiter;

        public ListItem SelectedDelimiter
        {
            get { return _selectedDelimiter; }
            set
            {
                if (_selectedDelimiter != value)
                {
                    _selectedDelimiter = value;
                    OnPropertyChanged("SelectedDelimiter");
                    SetData();
                }
            }
        }

        private DataView _itemData;

        public DataView ItemData
        {
            get
            {
                return _itemData;
            }
            set
            {
                _itemData = value;
                OnPropertyChanged("ItemData");
            }
        }

        private bool _firstRow = true;

        public bool FirstRow
        {
            get { return _firstRow; }
            set
            {
                _firstRow = value;
                OnPropertyChanged("FirstRow");
                SetData();
            }
        }

        #region Commands
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
            GroupDataViewModel groupDataViewModel = new GroupDataViewModel(_wizardViewModel, Fields);
        }
        
        #endregion

        private Visibility _gridVisibility = Visibility.Collapsed;

        public Visibility GridVisibility
        {
            get
            {
                if (Fields!=null&& Fields.Rows.Count > 0)
                {
                    _gridVisibility = Visibility.Visible;
                }
                else
                {
                    _gridVisibility = Visibility.Collapsed;
                }

                return _gridVisibility;
            }
            set
            {
                _gridVisibility = value;
                OnPropertyChanged("GridVisibility");
            }
        }

        public List<string> Lines { get; set; }
        public List<ListItem> Columns { get; set; }
        public DataTable Fields { get; set; }
        
        private List<ListItem> _failed;
        public List<ListItem> Failed
        {
            get { return _failed; }
            set
            {
                _failed = value;
                OnPropertyChanged("Failed");
            }
        }
        
        private void SetData()
        {
            if (Lines == null || Lines.Count < 1) return;

            string[] segments = Lines[0].Split(new[] {SelectedDelimiter.Value}, StringSplitOptions.RemoveEmptyEntries);
            CreateColumns(segments);

            Failed = new List<ListItem>();

            int i = 0;
            if (FirstRow)
            {
                i = 1;
            }

            for (; i < Lines.Count; i++)
            {
                string line = Lines[i];
                string[] columns = line.Split(new[] {SelectedDelimiter.Value}, StringSplitOptions.None);
                
                DataRow dataRow = Fields.NewRow();
                for (int j = 0; j < Columns.Count; j++)
                {
                    dataRow[j] = columns[j];
                }

                Fields.Rows.Add(dataRow);

                //if (columns.Length == Columns.Count-1)
                //{
                //    dataRow.ItemArray = columns;
                //    Fields.Rows.Add(dataRow);
                //}
                //else
                //{
                //    Failed.Add(new ListItem(i.ToString(), line));
                //}
            }

            ItemData = Fields.DefaultView;
            
        }

        private void CreateColumns(string[] columns)
        {
            Columns = new List<ListItem>();
            for (int i = 0; i < columns.Length; i++)
            {
                if (FirstRow)
                {
                    string column = columns[i];
                    if (!string.IsNullOrEmpty(column))
                    {
                        Columns.Add(new ListItem(column, String.Format("Column{0}", i)));
                    }
                }
                else
                {
                    Columns.Add(new ListItem(String.Format("Колонка{0}", i), String.Format("Column{0}", i)));
                }
            }

            if (Columns == null || Columns.Count == 0) return;

            Fields = new DataTable();

            foreach (ListItem listItem in Columns)
            {
                DataColumn dc = new DataColumn();
                dc.Caption = listItem.Name;
                dc.ColumnName = listItem.Value;
                dc.DataType = typeof(string);
                Fields.Columns.Add(dc);
            }
        }

        private void Convert()
        {
            List<string> corrected = new List<string>();
            for (int i = 0; i < Lines.Count; i++)
            {
                string line = Lines[i];
                string[] columns = line.Split(new[] {SelectedDelimiter.Value}, StringSplitOptions.None);

                string output = string.Empty;

                for (int j = 0; j < 8; j++)
                {
                    output += columns[j] + ";";
                }
                corrected.Add(output);
            }

            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string path = Path.Combine(folder, "Employee.txt");
            AppFile.WriteTextFile(corrected, path, Encoding.UTF8);
        }
        
    }
}