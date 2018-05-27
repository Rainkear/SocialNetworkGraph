using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SocialNetworkGraph.ViewModels;

namespace SocialNetworkGraph
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                var view = new MainWindow();
                view.Show();
                var vm = new MainWindowViewModel();
                vm.DisplayPersonWindow += Vm_DisplayPersonWindow;
                view.DataContext = vm;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void Vm_DisplayPersonWindow(object sender, PersonWindowViewModel e)
        {
            var personView = new PersonWindow();
            personView.DataContext = e;
            personView.Show();

        }
    }
}
