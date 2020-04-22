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
    }
}
