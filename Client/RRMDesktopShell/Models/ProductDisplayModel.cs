using RRMDesktopShell.Library.Base;

namespace RRMDesktopShell.Models
{
    public class ProductDisplayModel : PropertyChangedRedBase
    {
        /// <summary>
        /// unique Identifier for the product
        /// </summary>
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal RetailPrice { get; set; }
        /// <summary>
        /// quantity in stock inventory for the product
        /// </summary>
        private int _quantityInStock;

        public int QuantityInStock
        {
            get => _quantityInStock;
            set
            {
                _quantityInStock = value;
                OnPropertyChanged();
            }
        }
        public bool IsTaxable { get; set; }
    }
}