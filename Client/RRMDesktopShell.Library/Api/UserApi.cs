using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RRMDesktopShell.Library.Models;

namespace RRMDesktopShell.Library.Api
{
    public class UserApi : IUserApi
    {
        private readonly IApiHelper _apiHelper;

        public UserApi(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }


        public async Task<List<UserModel>> GetAll()
        {
            using (var response = await  _apiHelper.ApiClient.GetAsync("api/User/GetAllUsers"))
            {
                if(!response.IsSuccessStatusCode) throw  new Exception(response.ReasonPhrase);
                var result = await response.Content.ReadAsAsync<List<UserModel>>();
                return result;
            }
        }

        public async Task<Dictionary<string,string>> GetAllRoles()
        {
            using (var response = await _apiHelper.ApiClient.GetAsync("api/User/GetAllRoles"))
            {
                if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
                var result = await response.Content.ReadAsAsync<Dictionary<string,string>>();
                return result;
            }
        }

        public async Task AddRole(string userId ,string roleName)
        {
            var data = new {userId, roleName};
            using (var response = await _apiHelper.ApiClient.PostAsJsonAsync("api/User/AddRole",data))
            {
                if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task DeleteRole(string userId, string roleName)
        {
            var data = new { userId, roleName };
            using (var response = await _apiHelper.ApiClient.PostAsJsonAsync("api/User/DeleteRole", data))
            {
                if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
