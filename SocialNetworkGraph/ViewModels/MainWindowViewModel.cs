using QuickGraph;
using SocialNetworkGraph.Commands;
using SocialNetworkGraph.Models;
using SocialNetworkGraph.Utilities;
using System;
using System.Linq;
using System.Threading;

namespace SocialNetworkGraph.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private DbUtils dbUtils;

        private bool _loaded;
        public bool Loaded
        {
            get { return _loaded; }
            set
            {
                NotifyPropertyChanged("Loaded");
                _loaded = value;
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
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

        private CommandHandler _closeWindowCommand;
        public CommandHandler CloseWindowCommand
        {
            get
            {
                return _closeWindowCommand ?? (_closeWindowCommand = new CommandHandler(param => CloseAll(), CanExecute));
            }
        }

        private CommandHandler _stateWindowCommand;
        public CommandHandler StateWindowCommand
        {
            get
            {
                return _stateWindowCommand ?? (_stateWindowCommand = new CommandHandler(param => StateChangeAll(param), CanExecute));
            }
        }

        private void StateChangeAll(object param)
        {
            WindowUtils.WindowManager.Instance.ChangeStateAllWindows(param);
        }

        private void CloseAll()
        {
            WindowUtils.WindowManager.Instance.CloseAllWindows();
        }

        private bool _canExecute;
        public bool CanExecute
        {
            get { return _canExecute; }
            set
            {
                NotifyPropertyChanged("CanExecute");
                _canExecute = value;
            }
        }

        public void ShowInfo(object param)
        {
            try
            {
                ErrorMessage = string.Empty;
                WindowUtils.WindowManager.Instance.OpenWindow(
                    new PersonWindowViewModel(dbUtils.GetPersonById(((Vertex)param).Id)));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Instance.LogFile(ex.ToString());
                ErrorMessage = ex.Message;
            }
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
                ExceptionLogger.Instance.LogFile(ex.ToString());
                ErrorMessage = ex.Message;
                Timer exitTimer = new Timer(ExceptionExit, null, 10000, Timeout.Infinite);
            }
        }

        private void ExceptionExit(object state)
        {
            Environment.Exit(1);
        }


    }
}
