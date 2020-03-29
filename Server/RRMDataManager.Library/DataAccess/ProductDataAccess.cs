using System.Collections.Generic;
using System.Linq;
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

        public ProductModel GetProductById(int id)
        {
            var dataAccess = new SqlDataAccess();
            var p = new { id };
            return  dataAccess.LoadData<ProductModel, dynamic>("[dbo].[spGetProductById]", p, "RRMData").FirstOrDefault();
        }
    }
}
