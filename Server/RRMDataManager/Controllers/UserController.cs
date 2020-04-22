using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RRMDataManager.Library.DataAccess;
using RRMDataManager.Library.Models;
using RRMDataManager.Models;

namespace RRMDataManager.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("api/User/GetAllUsers")]
        public List<ApplicationUserModel> GetAllUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var users = userManager.Users.ToList();
                var roles = context.Roles.ToList();


                var userRoles = users.Select(user => new ApplicationUserModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Roles = user.Roles
                        .Zip(roles,(role, identityRole) =>  new {Key = role.RoleId,Value = roles.First(role1 =>role1.Id== role.RoleId).Name})
                        .ToDictionary(arg => arg.Key,arg => arg.Value)
                }).ToList();

                return userRoles;
            }
        }
    }

}
