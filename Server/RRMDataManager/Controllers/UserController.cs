using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using RRMDataManager.Library.DataAccess;

namespace RRMDataManager.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
       

        // GET: User/5
        
        public UserModel GetById()
        {
            var id = RequestContext.Principal.Identity.GetUserId();
            var data = new UserDataAccess();
            var user = data.GetUserById(id);
            return user;
        }
    }
}
