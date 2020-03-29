using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RRMDesktopShell.Library.Models;

namespace RRMDesktopShell.Library.Api
{
    public class ProductApi : IProductApi
    {
        private readonly IApiHelper _apiHelper;

        public ProductApi(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<ProductModel>> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/Product"))
            {
                if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
                var result = await response.Content.ReadAsAsync<List<ProductModel>>();
                return result;
            }
        }
    }
}
