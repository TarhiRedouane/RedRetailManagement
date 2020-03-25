namespace RRMDesktopShell.Library.Models
{
    public class ProductModel
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
        public int QuantityInStock { get; set; }
    }
}
