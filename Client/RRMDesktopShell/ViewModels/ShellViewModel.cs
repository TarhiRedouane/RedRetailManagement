using Caliburn.Micro;
using RRMDesktopShell.Events;

namespace RRMDesktopShell.ViewModels
{
    public class ShellViewModel : Conductor<object>,IHandle<LoggedInEvent>
    {
        private LoginViewModel _loginViewModel;
        private readonly SalesViewModel _salesViewModel;
        private readonly IEventAggregator _eventAggregator;

        public ShellViewModel(LoginViewModel loginViewModel,SalesViewModel salesViewModel,IEventAggregator eventAggregator)
        {
            _loginViewModel = loginViewModel;
            _salesViewModel = salesViewModel;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            ActivateItem(loginViewModel);
        }

        public void Handle(LoggedInEvent message)
        {
           ActivateItem(_salesViewModel); 
        }
    }
}