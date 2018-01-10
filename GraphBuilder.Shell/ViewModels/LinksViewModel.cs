using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GraphBuilder.Core;
using GraphBuilder.Shell.Models;
using GraphBuilder.Shell.Properties;
using GraphBuilder.Shell.Views;

namespace GraphBuilder.Shell.ViewModels
{
    public class LinksViewModel : ViewModelBase
    {
        public LinksViewModel()
        {
            Title = "Связи";
            ContentId = Guid.Parse("66fa7e53-3f4d-4f05-942e-cb7d3f5e1082");
            CanClose = true;
            IsClosed = false;
            Populate();

            LinksView linksView = new LinksView();
            linksView.DataContext = this;
            UserInterface = linksView;
        }

        private void Populate()
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(Settings.Default.IconsPath);
                IEnumerable<DirectoryInfo> folders = di.EnumerateDirectories();
                foreach (DirectoryInfo folder in folders)
                {
                    Thumb item = new Thumb();
                    item.Name = folder.Name;
                    Categories.Add(item);

                    FileInfo[] files = folder.GetFiles("*.png");
                    EnlistImages(files);
                }
            }
            catch
            {
                // ignored
            }
        }

        private void EnlistImages(FileInfo[] files)
        {
            foreach (FileInfo file in files)
            {
                Thumb item = new Thumb();
                item.Name = file.Name;
                BitmapImage image = new BitmapImage(new Uri(file.FullName));
                item.Image = image;
                Images.Add(item);
            }
        }
        
        private Thumb _selectedImage;
        public Thumb SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                _selectedImage = value;
                OnPropertyChanged("SelectedImage");
            }
        }

        private List<Thumb> _categories = new List<Thumb>();
        public List<Thumb> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged("Categories");
            }
        }

        private ObservableCollection<Thumb> _images = new ObservableCollection<Thumb>();
        public ObservableCollection<Thumb> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                OnPropertyChanged("Images");
            }
        }
    }
}