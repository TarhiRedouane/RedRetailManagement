using Caliburn.Micro;
using RRMDesktopShell.Events;
using RRMDesktopShell.Library.Models;

namespace RRMDesktopShell.ViewModels
{
    public class ShellViewModel : Conductor<object>,IHandle<LoggedInEvent>
    {

        private readonly SalesViewModel _salesViewModel;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggedInUserModel _loggedInUserModel;

        public ShellViewModel(SalesViewModel salesViewModel,IEventAggregator eventAggregator,ILoggedInUserModel loggedInUserModel)
        {
            _salesViewModel = salesViewModel;
            _eventAggregator = eventAggregator;
            _loggedInUserModel = loggedInUserModel;
            _eventAggregator.Subscribe(this);
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(LoggedInEvent message)
        {
           ActivateItem(_salesViewModel); 
        }

        public void ExitApplication()
        {
            TryClose();
        }

        public void Logout()
        {
            _loggedInUserModel.ClearProfile();
            ActivateItem(IoC.Get<LoginViewModel>());
        }
    }
}