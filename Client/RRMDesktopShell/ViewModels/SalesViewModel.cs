using System.ComponentModel;
using System.Threading.Tasks;
using Caliburn.Micro;
using RRMDesktopShell.Library.Api;
using RRMDesktopShell.Library.Models;

namespace RRMDesktopShell.ViewModels
{
    public class SalesViewModel : Screen
    {
        private readonly IProductApi _productApi;

        #region Constructor

        public SalesViewModel(IProductApi productApi)
        {
            _productApi = productApi;
            
        }

        protected override async void OnInitialize()
        {
            await LoadProductsAsync();
        }

        public async Task LoadProductsAsync()
        {
            var products = await _productApi.GetAll();
            Products = new BindingList<ProductModel>(products);
        }

        #endregion
        #region Properties

        private BindingList<ProductModel> _products;

        public BindingList<ProductModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private int _itemQuantity;

        public int ItemQuantity
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
