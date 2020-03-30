using System.Collections.Generic;
using System.Web.Http;
using RRMDataManager.Library.DataAccess;
using RRMDataManager.Library.Models;

namespace RRMDataManager.Controllers
{
    [Authorize]
    public class InventoryController : ApiController
    {
        // GET: api/Inventory
        public List<InventoryModel> Get()
        {
            var inventoryData = new InventoryDataAccess();
            return inventoryData.GetInventory();
        }
        // POST: api/Inventory
        public void Post([FromBody] InventoryModel item)
        {
            var inventoryData = new InventoryDataAccess();
            inventoryData.SaveInventoryRecord(item);
        }
    }
}
