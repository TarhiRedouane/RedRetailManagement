using System.Collections.Generic;
using RRMDataManager.Library.Internal.DataAccess;
using RRMDataManager.Library.Models;

namespace RRMDataManager.Library.DataAccess
{
    public class InventoryDataAccess
    {
        /// <summary>
        /// return the inventory for the owner => admin
        /// </summary>
        /// <returns></returns>
        public List<InventoryModel> GetInventory()
        {
            var dataAccess = new SqlDataAccess();
            return dataAccess.LoadData<InventoryModel, dynamic>("[dbo].[SpGetInventories]", new { }, "RRMData");
        }
        /// <summary>
        /// add a record in an inventory => adding new product only by admin 
        /// </summary>
        /// <param name="inventoryModel"></param>
        public void SaveInventoryRecord(InventoryModel inventoryModel)
        {
            var dataAccess = new SqlDataAccess();
            dataAccess.SaveData<int,InventoryModel>("[dbo].[spInsertInventory]", inventoryModel, "RRMData");
        }
    }
}