using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RRMDesktopShell.Library.Models;

namespace RRMDesktopShell.Library.Api
{
    public class ProductApi : ApiHelper, IProductApi
    {
        private readonly ILoggedInUserModel _loggedInUserModel;

        public ProductApi(ILoggedInUserModel loggedInUserModel) : base(loggedInUserModel)
        {
            _loggedInUserModel = loggedInUserModel;
        }

        public async Task<List<ProductModel>> GetAll()
        {
            ApiClient.DefaultRequestHeaders.Clear();
            ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_loggedInUserModel.Token}");
            using (HttpResponseMessage response = await ApiClient.GetAsync("api/Product"))
            {
                if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
                var result = await response.Content.ReadAsAsync<List<ProductModel>>();
                return result;
            }
        }
    }
}
