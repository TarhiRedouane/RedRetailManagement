using System;
using System.Net.Http;
using System.Threading.Tasks;
using RRMDesktopShell.Library.Models;

namespace RRMDesktopShell.Library.Api
{
    public interface ISaleApi
    {
        Task PostSale(SaleModel sale);
    }

    public class SaleApi : ISaleApi
    {
        private readonly IApiHelper _apiHelper;

        public SaleApi(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }
        public async Task PostSale(SaleModel sale)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Sale",sale))
            {
                if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
                //todo : successful post sale
            }
        }
    }
}