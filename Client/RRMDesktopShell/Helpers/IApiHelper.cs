using System.Threading.Tasks;
using RRMDesktopShell.Models;

namespace RRMDesktopShell.Helpers
{
    public interface IApiHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}