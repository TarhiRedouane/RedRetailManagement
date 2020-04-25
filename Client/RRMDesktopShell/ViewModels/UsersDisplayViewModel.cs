using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        private UserModel _selectedUser;

        public UserModel SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                DisplaySelectedUserDetails(value);
                NotifyOfPropertyChange(() => SelectedUser);
            }
        }

        private string _selectedUserName;

        public string SelectedUserName
        {
            get => _selectedUserName;
            set
            {
                _selectedUserName = value;
                NotifyOfPropertyChange(() => SelectedUserName);
            }
        }

        private BindingList<string> _selectedUserRoles = new BindingList<string>();

        public BindingList<string> SelectedUserRoles
        {
            get => _selectedUserRoles;
            set
            {
                _selectedUserRoles = value;
                NotifyOfPropertyChange(() => SelectedUserRoles);
            }
        }

        private BindingList<string> _availableRoles = new BindingList<string>();

        public BindingList<string> AvailableRoles
        {
            get => _availableRoles;
            set
            {
                _availableRoles = value;
                NotifyOfPropertyChange(() => AvailableRoles);
            }
        }

        private string _selectedRoleToRemove;

        public string SelectedRoleToRemove
        {
            get => _selectedRoleToRemove;
            set
            {
                _selectedRoleToRemove = value;
                NotifyOfPropertyChange(() => SelectedRoleToRemove);
            }
        }

        private string _selectedRoleToAdd;

        public string SelectedRoleToAdd
        {
            get => _selectedRoleToAdd;
            set
            {
                _selectedRoleToAdd = value;
                NotifyOfPropertyChange(() => SelectedRoleToAdd);
            }
        }

        public Dictionary<string,string> AllRoles { get; set; }

        #endregion

        public UsersDisplayViewModel(IDialogService dialogService,
                                    IUserApi userApi)
        {
            _dialogService = dialogService;
            _userApi = userApi;
        }

        #region Methods
        private async void DisplaySelectedUserDetails(UserModel value)
        {
            SelectedUserName = value.Email;
            SelectedUserRoles.Clear();
            value.Roles.Select(arg => arg.Value)
                .ToList()
                .ForEach(role => SelectedUserRoles.Add(role));

            await LoadSelectedUserAvailableRoles();

        }

        private async Task LoadSelectedUserAvailableRoles()
        {
             AllRoles = await _userApi.GetAllRoles();
            var roles = AllRoles
                .Select(dic => dic.Value)
                .ToList();

            AvailableRoles.Clear();
            roles.Except(SelectedUserRoles)
                .ToList()
                .ForEach(availableRole => AvailableRoles.Add(availableRole));

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

        #endregion

        #region Buttons

        public async void RemoveSelectedRole()
        {
            if(SelectedRoleToRemove ==null) return;
            await _userApi.DeleteRole(SelectedUser.Id, SelectedRoleToRemove);

            var role = SelectedRoleToRemove;
            AvailableRoles.Add(role);
            SelectedUserRoles.Remove(role);

            SelectedUser.Roles.Remove(SelectedUser.Roles.First(x => x.Value == role).Key);

            Users.ResetBindings();

        }

        public async void AddSelectedRole()
        {
            if (SelectedRoleToAdd == null) return;
            await _userApi.AddRole(SelectedUser.Id, SelectedRoleToAdd);

            var role = SelectedRoleToAdd;
            AvailableRoles.Remove(role);
            SelectedUserRoles.Add(role);

            SelectedUser.Roles.Add(AllRoles.First(arg => arg.Value == role).Key,role);

            Users.ResetBindings();
        }

        #endregion
    }
}
