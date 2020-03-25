using System.Collections.Generic;
using RRMDataManager.Library.Internal.DataAccess;
using RRMDataManager.Library.Models;

namespace RRMDataManager.Library.DataAccess
{
    public class ProductDataAccess
    {
        public List<ProductModel> GetProducts()
        {
            var dataAccess = new SqlDataAccess();
            return dataAccess.LoadData<ProductModel, dynamic>("[dbo].[spGetProducts]", new{}, "RRMData");   
        }
    }
}
