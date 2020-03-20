using Caliburn.Micro;

namespace RRMDesktopShell.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private LoginViewModel _loginViewModel;
        public ShellViewModel(LoginViewModel loginViewModel)
        {
            _loginViewModel = loginViewModel;
            ActivateItem(loginViewModel);
        }
    }
}