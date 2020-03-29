using RRMDesktopShell.Library.Base;

namespace RRMDesktopShell.Models
{
    public class CartItemDisplayModel : PropertyChangedRedBase
    {
        public ProductDisplayModel Product { get; set; }

		private int _quantityInCart;
        public int QuantityInCart
		{
			get => _quantityInCart;
            set
            {
                _quantityInCart = value; 
                OnPropertyChanged();
            }
		}

	}
}