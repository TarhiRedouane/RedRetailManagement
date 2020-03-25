using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using RRMDesktopShell.Events;
using RRMDesktopShell.Library.Api;

namespace RRMDesktopShell.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly IApiHelper _apiHelper;
        private readonly IEventAggregator _eventAggregator;

        public LoginViewModel(IApiHelper apiHelper,IEventAggregator eventAggregator)
        {
            _apiHelper = apiHelper;
            _eventAggregator = eventAggregator;
        }

        #region Properties
        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool HasErrorLogin => !string.IsNullOrEmpty(ErrorMessage);

        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
                NotifyOfPropertyChange(() => HasErrorLogin);
            }
        }
        #endregion

        public bool CanLogIn => UserName?.Length > 0 && Password?.Length > 0;

        public async Task LogIn(string userName)
        {
            try
            {
                ErrorMessage = $"";
                var result = await _apiHelper.Authenticate(userName, Password);

                //get the user information 
               await _apiHelper.GetLoggedInUser(result.Access_Token);
               _eventAggregator.PublishOnUIThread(new LoggedInEvent());
            }
            catch (Exception ex){ ErrorMessage =ex.Message; }
        }
    }
}
