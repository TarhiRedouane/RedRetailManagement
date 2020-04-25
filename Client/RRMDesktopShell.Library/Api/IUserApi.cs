using System.Collections.Generic;
using System.Threading.Tasks;
using RRMDesktopShell.Library.Models;

namespace RRMDesktopShell.Library.Api
{
    public interface IUserApi
    {
        Task<List<UserModel>> GetAll();
        Task<Dictionary<string,string>> GetAllRoles();
        Task AddRole(string userId ,string roleName);
        Task DeleteRole(string userId, string roleName);
    }
}