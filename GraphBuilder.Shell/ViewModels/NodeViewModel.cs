using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using GraphBuilder.Core;
using GraphBuilder.Shell.Models;

namespace GraphBuilder.Shell.ViewModels
{
    public class NodeViewModel:ViewModelBase
    {
        
        public NodeViewModel(Node node)
        {
            Node = node;
            Cards = _node.Cards;
        }

        private Node _node;
        public Node Node
        {
            get { return _node; }
            set
            {
                _node = value;
            }
        }

        private List<Card> _cards;

        public List<Card> Cards
        {
            get { return _cards; }
            set
            {
                _cards = value;
            }
        }

        private Card _selectedCard;

        public Card SelectedCard
        {
            get { return _selectedCard; }
            set
            {
                _selectedCard = value;
            }
        }
    }
}