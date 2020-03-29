using System.Collections.Generic;

namespace RRMDesktopShell.Library.Models
{
    public class SaleModel
    {
        public List<SaleDetailModel> SaleDetails { get; set; } = new List<SaleDetailModel>();
    }
}
