using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using RRMDesktopShell.Helpers;

namespace RRMDesktopShell.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly IApiHelper _apiHelper;

        public LoginViewModel(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
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
        #endregion

        public bool CanLogIn => UserName?.Length > 0 && Password?.Length > 0;

        public async Task LogIn(string userName)
        {
            try
            {
                var user = await _apiHelper.Authenticate(userName, Password);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }        }
    }
}
