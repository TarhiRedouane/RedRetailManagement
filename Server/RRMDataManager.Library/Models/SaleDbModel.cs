using System;

namespace RRMDataManager.Library.Models
{
    public class SaleDbModel
    {
        public string CashierId { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

    }
}