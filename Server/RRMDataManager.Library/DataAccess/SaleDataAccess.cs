using System;
using System.Collections.Generic;
using System.Linq;
using RRMDataManager.Library.Helpers;
using RRMDataManager.Library.Internal.DataAccess;
using RRMDataManager.Library.Models;

namespace RRMDataManager.Library.DataAccess
{
    public class SaleDataAccess
    {
        public void SaveSale(SaleModel saleInfo,string cashierId)
        {
            //todo : make this respect solid principles // Dry
            //start filling in the sale detail models we will save to the database 
            var details = new List<SaleDetailDbModel>();
            var productDataAccess = new ProductDataAccess();
            var taxRate = ConfigHelper.GetTaxRate();
            saleInfo.SaleDetails.ForEach(saleDetail =>
            {
                var detail = new SaleDetailDbModel
                {
                    ProductId = saleDetail.ProductId,
                    Quantity = saleDetail.Quantity
                };

                //Get information about the current procduct
                var product = productDataAccess.GetProductById(detail.ProductId);
                if (product == null) throw new Exception($"the product id of {detail.ProductId} could not be found");

                detail.PurchasePrice = product.RetailPrice * detail.Quantity;
                detail.Tax = product.IsTaxable ? detail.PurchasePrice * (taxRate/100) : 0;

                details.Add(detail);
            });
            //create sale model
            var sale = new SaleDbModel
            {
                SubTotal = details.Sum(detail => detail.PurchasePrice),
                Tax = details.Sum(detail => detail.Tax),
                CashierId = cashierId
            };
            sale.Total = sale.SubTotal + sale.Tax;
            using (SqlDataAccess sql = new SqlDataAccess())
            {
                try
                {
                    sql.StartTransaction("RRMData");
                    //save the sale model 
                    //get the id from the sale model
                    var saleId = sql.SaveDataInTransaction<int, SaleDbModel>("[dbo].[spInsertSale]", sale);
                    // finish filling sale details models with id 
                    foreach (var detail in details)
                    {
                        detail.SaleId = saleId;
                        //save the sale detail models
                        sql.SaveDataInTransaction<int, SaleDetailDbModel>("[dbo].[spInsertSaleDetail]", detail);
                    }
                }
                catch
                {
                    sql.RollbackTransaction();
                    throw;
                }
            }
        }
        /// <summary>
        /// returns sale reports to be used by the  owner
        /// </summary>
        /// <returns></returns>
        public List<SaleReportModel> GetSaleReport()
        {
            var dataAccess = new SqlDataAccess();
            return dataAccess.LoadData<SaleReportModel, dynamic>("[dbo].[spSaleReport]", new { }, "RRMData");
        }
    }
}