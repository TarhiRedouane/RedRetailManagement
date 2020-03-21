using System.Collections.Generic;
using System.Linq;
using RRMDataManager.Library.Internal.DataAccess;

namespace RRMDataManager.Library.DataAccess
{
    public class UserDataAccess
    {
        public UserModel GetUserById(string id)
        {
            var dataAccess = new SqlDataAccess();
            var p = new {id };
            var list = dataAccess.LoadData<UserModel, dynamic>("[dbo].[spUserLookup]", p, "RRMData");
            return list.FirstOrDefault( );
        }
    }
}
