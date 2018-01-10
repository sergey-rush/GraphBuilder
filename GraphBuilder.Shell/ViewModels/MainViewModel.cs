using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GraphBuilder.Core;
using GraphBuilder.Shell.Models;
using Xceed.Wpf.AvalonDock.Layout;

namespace GraphBuilder.Shell.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            
        }

        #region Props

        /// <summary>
        /// Shell window instance
        /// </summary>
        public MainWindow ShellWindow { get; set; }
        
        private bool _quitConfirmationEnabled;
        public bool QuitConfirmationEnabled
        {
            get { return _quitConfirmationEnabled; }
            set
            {
                if (value.Equals(_quitConfirmationEnabled)) return;
                _quitConfirmationEnabled = value;
                OnPropertyChanged("QuitConfirmationEnabled");
            }
        }

        private Visibility _magnifierVisibility = Visibility.Collapsed;

        public Visibility MagnifierVisibility
        {
            get { return _magnifierVisibility; }
            private set
            {
                _magnifierVisibility = value;
                OnPropertyChanged("MagnifierVisibility");
            }
        }

        private Visibility _progressVisibility = Visibility.Collapsed;

        public Visibility ProgressVisibility
        {
            get { return _progressVisibility; }
            private set
            {
                _progressVisibility = value;
                OnPropertyChanged("ProgressVisibility");
            }
        }

        private ObservableCollection<ViewModelBase> _anchorables = new ObservableCollection<ViewModelBase>();

        /// <summary>
        /// Gets a collection of all visible documents
        /// </summary>
        public ObservableCollection<ViewModelBase> Anchorables
        {
            get
            {
                return _anchorables;
            }
            set
            {
                _anchorables = value;
                OnPropertyChanged("Anchorables");
            }
        }

        private ObservableCollection<ViewModelBase> _documents = new ObservableCollection<ViewModelBase>();

        /// <summary>
        /// Gets a collection of all visible documents
        /// </summary>
        public ObservableCollection<ViewModelBase> Documents
        {
            get
            {
                return _documents;
            }
            set
            {
                _documents = value;
                OnPropertyChanged("Documents");
            }
        }

        private ViewModelBase _activeContent;

        public ViewModelBase ActiveContent
        {
            get
            {
                return _activeContent;
            }
            set
            {
                _activeContent = value;
                OnPropertyChanged("ActiveContent");
            }
        }
        
        private ObservableCollection<Node> _selectedNodes = new ObservableCollection<Node>();

        public ObservableCollection<Node> SelectedNodes
        {
            get
            {
                // Due to pop up collection from code-behind we notify TotalLinks property on get method
                OnPropertyChanged("TotalLinks");
                return _selectedNodes;
            }
            set
            {
                _selectedNodes = value;
                OnPropertyChanged("SelectedNodes");
                //OnPropertyChanged("TotalLinks");
            }
        }
        
        

        private Node _selectedNode;

        public Node SelectedNode
        {
            get { return _selectedNode; }
            set
            {
                _selectedNode = value;
                OnPropertyChanged("SelectedNode");
            }
        }

        private Link _selectedLink;

        public Link SelectedLink
        {
            get { return _selectedLink; }
            set
            {
                _selectedLink = value;
                OnPropertyChanged("SelectedLink");
            }
        }

        private TimeSpan _elapsed;

        public TimeSpan Elapsed
        {
            get { return _elapsed; }
            set
            {
                _elapsed = value;
                TotalElapsed += _elapsed;
                OnPropertyChanged("Elapsed");
            }
        }

        private TimeSpan _totalElapsed;

        public TimeSpan TotalElapsed
        {
            get { return _totalElapsed; }
            set
            {
                _totalElapsed = value;
                OnPropertyChanged("TotalElapsed");
            }
        }
        
        private List<Link> _statusList = new List<Link>();

        public List<Link> StatusList
        {
            get { return _statusList; }
            set
            {
                _statusList = value;
                OnPropertyChanged("StatusList");
            }
        }

        private ObservableCollection<Link> _eTickets = new ObservableCollection<Link>();

        public ObservableCollection<Link> ETickets
        {
            get { return _eTickets; }
            set
            {
                _eTickets = value;
                OnPropertyChanged("ETickets");
            }
        }
        
        private string _statusText = "Ready";

        public string StatusText
        {
            get { return _statusText; }
            set
            {
                _statusText = value;
                OnPropertyChanged("StatusText");
            }
        }

        

        #endregion

        #region Commands

        private DelegateCommand _addNodes;

        public ICommand AddNodes
        {
            get
            {
                if (_addNodes == null)
                {
                    _addNodes = new DelegateCommand(OnAddNodes);
                }
                return _addNodes;
            }
        }

        private void OnAddNodes()
        {

        }

        /// <summary>
        /// GetPointsOnCircle
        /// </summary>
        /// <param name="length">Number of points</param>
        /// <param name="radius">Radius of the circle</param>
        /// <param name="center">Center point</param>
        /// <returns></returns>
        private List<Point> GetPointsOnCircle(int length, double radius, Point center)
        {
            List<Point> points = new List<Point>();
            double slice = 2*Math.PI/length;
            for (int i = 0; i < length; i++)
            {
                double angle = slice*i;
                int newX = (int) (center.X + radius*Math.Cos(angle));
                int newY = (int) (center.Y + radius*Math.Sin(angle));
                Point p = new Point(newX, newY);
                //Debug.WriteLine(p);
                points.Add(p);
            }

            return points;
        }

        private DelegateCommand _addLinks;

        public ICommand AddLinks
        {
            get
            {
                if (_addLinks == null)
                {
                    _addLinks = new DelegateCommand(OnAddLinks);
                }
                return _addLinks;
            }
        }

        private void OnAddLinks()
        {



        }
        
        private DelegateCommand _toggleMagnifier;

        public ICommand ToggleMagnifier
        {
            get
            {
                if (_toggleMagnifier == null)
                {
                    _toggleMagnifier = new DelegateCommand(OnToggleMagnifier);
                }
                return _toggleMagnifier;
            }
        }

        private void OnToggleMagnifier()
        {
            if (MagnifierVisibility == Visibility.Visible)
            {
                MagnifierVisibility = Visibility.Collapsed;
            }
            else
            {
                MagnifierVisibility = Visibility.Visible;
            }
        }

        private DelegateCommand _showUserDialog;

        public ICommand ShowUserDialog
        {
            get
            {
                if (_showUserDialog == null)
                {
                    _showUserDialog = new DelegateCommand(OnShowUserDialog);
                }
                return _showUserDialog;
            }
        }

        private void OnShowUserDialog()
        {
            StatusText = "OnShowUserDialog";
        }

        private DelegateCommand<int> _showLog;

        public ICommand ShowLog
        {
            get
            {
                if (_showLog == null)
                {
                    _showLog = new DelegateCommand<int>(OnShowLog);
                }
                return _showLog;
            }
        }

        private void OnShowLog(int LinkId)
        {

        }

        private DelegateCommand _refreshTicket;

        public ICommand RefreshTicket
        {
            get
            {
                if (_refreshTicket == null)
                {
                    _refreshTicket = new DelegateCommand(OnRefreshTicket);
                }
                return _refreshTicket;
            }
        }

        private void OnRefreshTicket()
        {

        }

        private DelegateCommand _updateLinkStatus;

        public ICommand UpdateLinkStatus
        {
            get
            {
                if (_updateLinkStatus == null)
                {
                    _updateLinkStatus = new DelegateCommand(OnUpdateLinkStatus);
                }
                return _updateLinkStatus;
            }
        }

        private void OnUpdateLinkStatus()
        {

        }
        
        private DelegateCommand _showSales;

        public ICommand ShowSales
        {
            get
            {
                if (_showSales == null)
                {
                    _showSales = new DelegateCommand(OnShowSales);
                }
                return _showSales;
            }
        }

        private void OnShowSales()
        {
            
        }

        private DelegateCommand _deleteSalesDuplicates;

        public ICommand DeleteSalesDuplicates
        {
            get
            {
                if (_deleteSalesDuplicates == null)
                {
                    _deleteSalesDuplicates = new DelegateCommand(OnDeleteSalesDuplicates, CanDeleteSalesDuplicates);
                }
                return _deleteSalesDuplicates;
            }
        }

        private void OnDeleteSalesDuplicates()
        {

        }

        private bool CanDeleteSalesDuplicates()
        {
            return true;
        }


        private DelegateCommand _showETickets;

        public ICommand ShowETickets
        {
            get
            {
                if (_showETickets == null)
                {
                    _showETickets = new DelegateCommand(OnShowETickets);
                }
                return _showETickets;
            }
        }

        private void OnShowETickets()
        {

            StatusText = String.Format("Найдено билетов {0}", ETickets.Count);
        }

        private DelegateCommand _deleteETicketsDuplicates;

        public ICommand DeleteETicketsDuplicates
        {
            get
            {
                if (_deleteETicketsDuplicates == null)
                {
                    _deleteETicketsDuplicates = new DelegateCommand(OnDeleteETicketsDuplicates,
                        CanDeleteETicketsDuplicates);
                }
                return _deleteETicketsDuplicates;
            }
        }

        private void OnDeleteETicketsDuplicates()
        {

        }

        private bool CanDeleteETicketsDuplicates()
        {
            return ETickets.Count > 0;
        }
        
        private DelegateCommand _showTETickets;

        public ICommand ShowTETickets
        {
            get
            {
                if (_showTETickets == null)
                {
                    _showTETickets = new DelegateCommand(OnShowTETickets);
                }
                return _showTETickets;
            }
        }

        private void OnShowTETickets()
        {

        }
        
        #endregion
    }
}