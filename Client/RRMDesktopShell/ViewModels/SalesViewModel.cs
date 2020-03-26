using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using RRMDesktopShell.Helpers;
using RRMDesktopShell.Library.Api;
using RRMDesktopShell.Library.Models;

namespace RRMDesktopShell.ViewModels
{
    public class SalesViewModel : Screen
    {
        private readonly IProductApi _productApi;
        private readonly IConfigHelper _configHelper;

        #region Constructor

        public SalesViewModel(IProductApi productApi, IConfigHelper configHelper)
        {
            _productApi = productApi;
            _configHelper = configHelper;
        }

        protected override async void OnInitialize()
        {
            Cart = new BindingList<CartItemModel>();
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
        //default quantity 1
        private int _itemQuantity = 1;

        public int ItemQuantity
        {
            get => _itemQuantity;
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private BindingList<CartItemModel> _cart;

        public BindingList<CartItemModel> Cart
        {
            get => _cart;
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private ProductModel _selectedProduct;

        public ProductModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private CartItemModel _selectedCart;

        public CartItemModel SelectedCart
        {
            get => _selectedCart;
            set
            {
                _selectedCart = value;
                NotifyOfPropertyChange(() => SelectedCart);
            }
        }

        public string SubTotal => CalculateSubTotal().ToString("C", CultureInfo.CurrentCulture);

        private decimal CalculateSubTotal()
        {
            var calc = Cart?.ToList().Sum(item => (item.Product.RetailPrice * item.QuantityInCart)) ?? 0;
            return calc;
        }

        public string Tax => CalculateTax().ToString("C", CultureInfo.CurrentCulture);

        private decimal CalculateTax()
        {
            var taxRate = _configHelper.GetTaxRate();
            var calc = Cart?
                .ToList()
                .Where(item => item.Product.IsTaxable)
                .Sum(item => item.Product.RetailPrice * item.QuantityInCart * (taxRate / 100)) ?? 0;
            return calc;
        }

        public string Total => (CalculateSubTotal()+CalculateTax()).ToString("C",CultureInfo.CurrentCulture);

        #endregion

        #region ButtonMethods

        public void AddToCart()
        {
            if (ItemExistInCart())
                UpdateQuantity();
            else
                AddNewItemToCart();

            ReinitializeQuantities();
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
        }

        private void UpdateQuantity()
        {
            var itemInCart = Cart.FirstOrDefault(item => item.Product.Id == SelectedProduct.Id);
            if (itemInCart != null) itemInCart.QuantityInCart += ItemQuantity;
            Cart.ResetItem(Cart.IndexOf(itemInCart));
        }

        private bool ItemExistInCart()
        {
            return Cart.Any(item => item.Product.Id == SelectedProduct.Id);
        }

        private void AddNewItemToCart()
        {
            var cartItem = new CartItemModel
            {
                Product = SelectedProduct,
                QuantityInCart = ItemQuantity
            };
            Cart.Add(cartItem);
        }

        private void ReinitializeQuantities()
        {
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
        }

        public bool CanAddToCart => IsQuantityValid();

        /// <summary>
        /// check if there is at least  one quantity for the current product ,
        /// check if there is a selected product,
        /// check if the quantity is less than the quantity in stock,
        /// </summary>
        /// <returns></returns>
        private bool IsQuantityValid()
        {
            return ItemQuantity <= SelectedProduct?.QuantityInStock && ItemQuantity >= 1;
        }


        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
        }
        //make sure something is selected from cart
        public bool CanRemoveFromCart => true;


        public void Checkout()
        { }

        public bool CanCheckout => true;

        #endregion
    }
}
