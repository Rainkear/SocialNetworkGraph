using System;
using System.Diagnostics;
using System.Windows;
using SocialNetworkGraph.ViewModels;
using System.ComponentModel;
using SocialNetworkGraph.Utilities;

namespace SocialNetworkGraph
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _view;
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                _view = new MainWindow();
                _view.Show();
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                ExceptionLogger.Instance.LogFile(ex.Message);
                Debug.WriteLine(ex);
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = new MainWindowViewModel();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            (e.Result as MainWindowViewModel).DisplayPersonWindow += Vm_DisplayPersonWindow;
            _view.DataContext = (e.Result as MainWindowViewModel);
        }

        private void Vm_DisplayPersonWindow(object sender, PersonWindowViewModel e)
        {
            var personView = new PersonWindow();
            personView.DataContext = e;
            personView.Show();
        }
    }
}
