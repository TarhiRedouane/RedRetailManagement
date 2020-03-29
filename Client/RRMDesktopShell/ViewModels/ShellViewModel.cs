using Caliburn.Micro;
using RRMDesktopShell.Events;
using RRMDesktopShell.Library.Api;
using RRMDesktopShell.Library.Models;

namespace RRMDesktopShell.ViewModels
{
    public class ShellViewModel : Conductor<object>,IHandle<LoggedInEvent>
    {

        private readonly SalesViewModel _salesViewModel;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggedInUserModel _loggedInUserModel;
        private readonly IApiHelper _apiHelper;

        public ShellViewModel(SalesViewModel salesViewModel,IEventAggregator eventAggregator,ILoggedInUserModel loggedInUserModel,IApiHelper apiHelper)
        {
            _salesViewModel = salesViewModel;
            _eventAggregator = eventAggregator;
            _loggedInUserModel = loggedInUserModel;
            _apiHelper = apiHelper;
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
            //initialize model
            _loggedInUserModel.ClearProfile();
            //clear Authorization token
            _apiHelper.LogOutUser();
            ActivateItem(IoC.Get<LoginViewModel>());
        }
    }
}