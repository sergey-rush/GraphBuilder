using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GraphBuilder.Core;
using GraphBuilder.Shell.Models;
using GraphBuilder.Shell.Views;

namespace GraphBuilder.Shell.ViewModels
{
    public class DataTableViewModel : ViewModelBase
    {
        private ContentControl _contentControl = new ContentControl();

        public ContentControl ContentControl
        {
            get
            {
                return _contentControl;
            }
            set
            {
                _contentControl = value;
                OnPropertyChanged("ContentControl");
            }
        }

        private DocumentViewModel _document;

        public DataTableViewModel(DocumentViewModel document)
        {
            _document = document;

            //NodeList = _document.VirtualCanvas.Nodes;
            //NodeList.Add(SelectedNode);
            
        }

        private List<Node> _nodeList;

        public List<Node> NodeList
        {
            get { return _nodeList; }
            set
            {
                _nodeList = value;
                OnPropertyChanged("NodeList");
            }
        }

        private Node _selectedNode;

        public Node SelectedNode
        {
            get
            {
                return _selectedNode;
            }
            set
            {
                _selectedNode = value;
                OnPropertyChanged("SelectedNode");
            }
        }
    }
}