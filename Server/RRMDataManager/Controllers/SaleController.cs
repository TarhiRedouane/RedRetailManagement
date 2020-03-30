using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using RRMDataManager.Library.DataAccess;
using RRMDataManager.Library.Models;

namespace RRMDataManager.Controllers
{
    [Authorize]
    public class SaleController : ApiController
    {
        /// <summary>
        /// Only for admin => not restful 
        /// </summary>
        /// <returns></returns>
        [Route("GetSalesReport")]
        public List<SaleReportModel> GetSalesReport()
        {
            var saleData = new SaleDataAccess();
            return saleData.GetSaleReport();
        } 

        // GET: api/Sale
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Sale/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sale
        public void Post([FromBody]SaleModel sale)
        {
            var saleData = new SaleDataAccess();
            var id = RequestContext.Principal.Identity.GetUserId();
            saleData.SaveSale(sale,id);
        }

        // PUT: api/Sale/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Sale/5
        public void Delete(int id)
        {
        }
    }
}
