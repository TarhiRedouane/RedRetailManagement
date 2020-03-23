using System.ComponentModel;
using Caliburn.Micro;

namespace RRMDesktopShell.ViewModels
{
    public class SalesViewModel : Screen
    {
        #region Properties

        private BindingList<string> _products;

        public BindingList<string> Products
        {
            get => _products;
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private string _itemQuantity;

        public string ItemQuantity
        {
            get => _itemQuantity;
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }

        private BindingList<string> _cart;

        public BindingList<string> Cart
        {
            get => _cart;
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        public string SubTotal => "$0.01";
        public string Tax => "$0.01";
        public string Total => "$0.01";

        #endregion

        #region ButtonMethods

        public void AddToCart()
        {

        }
        //make sure there is something selected 
        //make sure there is a quantity
        public bool CanAddToCart => true;


        public void RemoveFromCart()
        {

        }
        //make sure something is selected from cart
        public bool CanRemoveFromCart => true;


        public void Checkout()
        { }

        public bool CanCheckout => true;

        #endregion
    }
}
