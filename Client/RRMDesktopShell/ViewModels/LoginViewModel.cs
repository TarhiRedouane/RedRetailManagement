using Caliburn.Micro;

namespace RRMDesktopShell.ViewModels
{
    public class LoginViewModel : Screen
    {
        public LoginViewModel()
        {

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

        public void LogIn(string userName, string password)
        {

        }


    }
}
