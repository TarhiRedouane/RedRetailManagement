using System.Collections.Generic;
using System.Threading.Tasks;
using RRMDesktopShell.Library.Models;

namespace RRMDesktopShell.Library.Api
{
    public interface IProductApi
    {
        Task<List<ProductModel>> GetAll();
    }
}