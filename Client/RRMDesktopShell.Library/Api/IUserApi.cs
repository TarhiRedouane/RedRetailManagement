using System.Collections.Generic;
using System.Threading.Tasks;
using RRMDesktopShell.Library.Models;

namespace RRMDesktopShell.Library.Api
{
    public interface IUserApi
    {
        Task<List<UserModel>> GetAll();
    }
}