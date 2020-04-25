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
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("api/User/GetAllRoles")]
        public Dictionary<string, string> GetAllRoles()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Roles.ToDictionary(role => role.Id, role => role.Name);
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("api/User/AddRole")]
        public void AddRole(UserRolePairModel userRole)
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                userManager.AddToRole(userRole.UserId, userRole.RoleName);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("api/User/DeleteRole")]
        public void DeleteRole(UserRolePairModel userRole)
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                userManager.RemoveFromRole(userRole.UserId, userRole.RoleName);
            }
        }
    }

}
