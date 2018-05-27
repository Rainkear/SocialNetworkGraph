using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using QuickGraph;
using SocialNetworkGraph.Commands;
using SocialNetworkGraph.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SocialNetworkGraph.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private DbUtils dbUtils;

        public event EventHandler<PersonWindowViewModel> DisplayPersonWindow;

        private BidirectionalGraph<object, IEdge<object>> _graph;
        public BidirectionalGraph<object, IEdge<object>> Graph
        {
            get { return _graph; }
            set
            {
                _graph = value;
                NotifyPropertyChanged("Graph");
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
                _canExecute = value;
            }
        }

        public void ShowInfo(object param)
        {
            DisplayPersonWindow(this, new PersonWindowViewModel(dbUtils.GetPersonById((int)param)));
        }

        public MainWindowViewModel()
        {
            ConstructGraph();
        }

        public void ConstructGraph()
        {
            dbUtils = new DbUtils();

            var personIdDict = dbUtils.GetAllPersonsIds(); //key - person id, value - list of friends
            Graph = new BidirectionalGraph<object, IEdge<object>>(false);
            Graph.AddVerticesAndEdgeRange(personIdDict.Select(x =>
                x.Value.Where(y => x.Key < y).Select(y => new Edge<object>(x.Key, y)).ToList()
            ).SelectMany(l => l).ToList());
            CanExecute = true;
        }
    }
}
