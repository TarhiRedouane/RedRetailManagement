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
        public List<UserModel> GetById()
        {
            var id = RequestContext.Principal.Identity.GetUserId();
            var data = new UserDataAccess();
            var users = data.GetUserById(id);
            return users;
        }
    }
}
