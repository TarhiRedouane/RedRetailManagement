using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RRMDesktopShell.Library.Models;

namespace RRMDesktopShell.Library.Api
{
    public class ApiHelper : IApiHelper
    {
        private readonly ILoggedInUserModel _loggedInUserModel;

        private HttpClient _apiClient;

        public ApiHelper(ILoggedInUserModel loggedInUserModel)
        {
            _loggedInUserModel = loggedInUserModel;
            InitializeClient();
        }

        public HttpClient ApiClient => _apiClient;

        private void InitializeClient()
        {
            var api = ConfigurationManager.AppSettings["api"];

            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type","password"),
                new KeyValuePair<string, string>("username",username),
                new KeyValuePair<string, string>("password",password)
            });

            using (HttpResponseMessage response = await _apiClient.PostAsync("/Token", data))
            {
                if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
                var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                return result;

            }
        }

        public void LogOutUser()
        {
            _apiClient.DefaultRequestHeaders.Clear();
        }
        public async Task GetLoggedInUser(string token)
        {
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            using (HttpResponseMessage response = await _apiClient.GetAsync("api/User"))
            {
                if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
                var result = await response.Content.ReadAsAsync<LoggedInUserModel>();
                _loggedInUserModel.Id = result.Id;
                _loggedInUserModel.EmailAdress = result.EmailAdress;
                _loggedInUserModel.FirstName = result.FirstName;
                _loggedInUserModel.LastName = result.LastName;
                _loggedInUserModel.CreatedDate = result.CreatedDate;
                _loggedInUserModel.Token = token;
            }
        }

    }
}
