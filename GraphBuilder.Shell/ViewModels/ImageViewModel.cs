using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using GraphBuilder.Core;
using GraphBuilder.Shell.Models;
using GraphBuilder.Shell.Views;

namespace GraphBuilder.Shell.ViewModels
{
    public class ImageViewModel : ViewModelBase
    {
        public ImageViewModel()
        {
            Title = "Иконки";
            Name = "ИконкиName";
            ContentId = Guid.Parse("afb1ba6a-61db-4b9f-8e14-5f1d2b2e04af");
            CanClose = true;
            IsClosed = false;

            Folders = AppFile.Default.GetFolders();
            if (Folders.Count > 0)
            {
                SelectedFolder = Folders[0].Path;
            }
            

            IconsView iconsView = new IconsView();
            iconsView.DataContext = this;
            UserInterface = iconsView;
        }

        private List<Thumb> _files;

        public List<Thumb> Files
        {
            get { return _files; }
            set
            {
                _files = value;
                OnPropertyChanged("Files");
            }
        }

        private Thumb _selectedFile;

        public Thumb SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                _selectedFile = value;
                OnPropertyChanged("SelectedFile");
            }
        }

        private List<Thumb> _folders;

        public List<Thumb> Folders
        {
            get { return _folders; }
            set
            {
                _folders = value;
                OnPropertyChanged("Folders");
            }
        }

        private string _selectedFolder;

        public string SelectedFolder
        {
            get { return _selectedFolder; }
            set
            {
                if (_selectedFolder != value)
                {
                    _selectedFolder = value;
                    OnPropertyChanged("SelectedFolder");
                    Files = AppFile.Default.GetFiles(_selectedFolder);
                }
            }
        }

        private DelegateCommand _refresh;

        public ICommand Refresh
        {
            get
            {
                if (_refresh == null)
                {
                    _refresh = new DelegateCommand(OnRefresh);
                }
                return _refresh;
            }
        }

        private void OnRefresh()
        {

        }

       
    }
}