using System;
using System.Windows.Input;
using GraphBuilder.Core;
using GraphBuilder.Shell.Views;

namespace GraphBuilder.Shell.ViewModels
{
    public class ContextViewModel : ViewModelBase
    {
        public ContextViewModel()
        {
            Title = "Контекст";
            Name = "ContextViewModel";
            ContentId = Guid.Parse("2d5213da-fdd3-43b9-9483-f3b699659c7a");
            CanClose = true;
            IsClosed = false;

            ContextView contextView = new ContextView();
            contextView.DataContext = this;
            UserInterface = contextView;
        }

        private int _nodesCount;
        public int NodesCount
        {
            get { return _nodesCount; }
            set
            {
                _nodesCount = value;
                OnPropertyChanged("NodesCount");
            }
        }

        private int _linksCount;
        public int LinksCount
        {
            get { return _nodesCount; }
            set
            {
                _linksCount = value;
                OnPropertyChanged("LinksCount");
            }
        }
    }
}