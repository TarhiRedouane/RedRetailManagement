using System.Windows;
using Caliburn.Micro;
using RRMDesktopShell.ViewModels;

namespace RRMDesktopShell.Startup
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}