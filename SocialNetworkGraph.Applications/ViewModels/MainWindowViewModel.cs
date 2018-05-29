using QuickGraph;
using SocialNetworkGraph.Commands;
using SocialNetworkGraph.Models;
using SocialNetworkGraph.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkGraph.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private DbUtils dbUtils;

        public event EventHandler<PersonWindowViewModel> DisplayPersonWindow;

        private bool _loaded;
        public bool Loaded
        {
            get
            {
                return _loaded;
            }

            set
            {
                NotifyPropertyChanged("Loaded");
                _loaded = value;
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }

            set
            {
                NotifyPropertyChanged("ErrorMessage");
                _errorMessage = value;
            }
        }

        private BidirectionalGraph<object, IEdge<object>> _graph;
        public BidirectionalGraph<object, IEdge<object>> Graph
        {
            get { return _graph; }
            set
            {
                NotifyPropertyChanged("Graph");
                _graph = value;
            }
        }

        private CommandHandler _vertexClickComand;
        public CommandHandler VertexClickCommand
        {
            get
            {
                return _vertexClickComand ?? (_vertexClickComand = new CommandHandler(param => ShowInfo(param), CanExecute));
            }
        }

        private bool _canExecute;
        public bool CanExecute
        {
            get
            {
                return _canExecute;
            }
            set
            {
                NotifyPropertyChanged("CanExecute");
                _canExecute = value;
            }
        }

        public void ShowInfo(object param)
        {
            DisplayPersonWindow(this, new PersonWindowViewModel(dbUtils.GetPersonById(((Vertex)param).Id)));
        }

        public MainWindowViewModel()
        {
            ConstructGraph();
        }

        public void ConstructGraph()
        {
            try
            {
                dbUtils = new DbUtils();
                var personIdDict = dbUtils.GetAllPersonsIds(); //key - person id, value - list of friends
                Graph = new BidirectionalGraph<object, IEdge<object>>(false);
                Graph.AddVerticesAndEdgeRange(personIdDict.Select(x =>
                    x.Value.Where(y => x.Key.Id < y.Id).Select(y => new Edge<object>(x.Key, y)).ToList()
                ).SelectMany(l => l).ToList());
                CanExecute = true;
                Loaded = true;

            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
