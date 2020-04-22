using System.ComponentModel;
using System.Threading.Tasks;
using Caliburn.Micro;
using RRMCustomControls.Services;
using RRMDesktopShell.Library.Api;
using RRMDesktopShell.Library.Models;

namespace RRMDesktopShell.ViewModels
{
    public class UsersDisplayViewModel : Screen
    {
        #region Private Fields
        private readonly IDialogService _dialogService;
        private readonly IUserApi _userApi;
        #endregion

        #region Properties

        private BindingList<UserModel> _users;

        public BindingList<UserModel> Users
        {
            get => _users;
            set
            {
                _users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }

        #endregion

        public UsersDisplayViewModel(IDialogService dialogService,
                                    IUserApi userApi)
        {
            _dialogService = dialogService;
            _userApi = userApi;
        }

        protected override async void OnInitialize()
        {
            try
            {
                await LoadUsersAsync();
            }
            catch
            {
                _dialogService.Message("System Error", "Unauthorized", "you do not have permission to interact with this area");
                TryClose();
            }
        }

        private async Task LoadUsersAsync()
        {
            var users = await _userApi.GetAll();
            Users = new BindingList<UserModel>(users);
        }
    }
}
