using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Caliburn.Micro;
using RRMCustomControls.Services;
using RRMDesktopShell.Helpers;
using RRMDesktopShell.Library.Api;
using RRMDesktopShell.Library.Models;
using RRMDesktopShell.Models;

namespace RRMDesktopShell.ViewModels
{
    public class SalesViewModel : Screen
    {
        private readonly IProductApi _productApi;
        private readonly IConfigHelper _configHelper;
        private readonly ISaleApi _saleApi;
        private readonly IMapper _mapper;
        private readonly IDialogService _dialogService;


        #region Constructor

        public SalesViewModel(IProductApi productApi, 
            IConfigHelper configHelper, 
            ISaleApi saleApi, 
            IMapper mapper,
            IDialogService dialogService)
        {
            _productApi = productApi;
            _configHelper = configHelper;
            _saleApi = saleApi;
            _mapper = mapper;
            _dialogService = dialogService;
        }

        protected override async void OnInitialize()
        {
            Cart = new BindingList<CartItemDisplayModel>();
            try
            {
                await LoadProductsAsync();
            }
            catch
            {
                _dialogService.Message("System Error","Unauthorized","you do not have permission to interact with the sales form");
                TryClose();
            }
        }

        public async Task LoadProductsAsync()
        {
            var productsList = await _productApi.GetAll();
            var products = _mapper.Map<List<ProductDisplayModel>>(productsList);
            Products = new BindingList<ProductDisplayModel>(products);
        }

        #endregion

        #region Properties

        private BindingList<ProductDisplayModel> _products;

        public BindingList<ProductDisplayModel> Products
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

        private BindingList<CartItemDisplayModel> _cart;

        public BindingList<CartItemDisplayModel> Cart
        {
            get => _cart;
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private ProductDisplayModel _selectedProduct;

        public ProductDisplayModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private CartItemDisplayModel _selectedCart;

        public CartItemDisplayModel SelectedCart
        {
            get => _selectedCart;
            set
            {
                _selectedCart = value;
                NotifyOfPropertyChange(() => SelectedCart);
                NotifyOfPropertyChange(() => CanRemoveFromCart);
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
                .Where(item => item.Product.IsTaxable)
                .Sum(item => item.Product.RetailPrice * item.QuantityInCart * (taxRate / 100)) ?? 0;
            return calc;
        }

        public string Total => (CalculateSubTotal() + CalculateTax()).ToString("C", CultureInfo.CurrentCulture);

        #endregion

        #region ButtonMethods

        public void AddToCart()
        {
            if (ItemExistInCart())
                UpdateQuantity();
            else
                AddNewItemToCart();
            NotifyAmount();
            ReinitializeQuantities();
            NotifyOfPropertyChange(() => CanCheckout);
        }

        private void UpdateQuantity()
        {
            var itemInCart = Cart.FirstOrDefault(item => item.Product.Id == SelectedProduct.Id);
            if (itemInCart != null) itemInCart.QuantityInCart += ItemQuantity;
            //Cart.ResetItem(Cart.IndexOf(itemInCart));
        }

        private bool ItemExistInCart()
        {
            return Cart.Any(item => item.Product.Id == SelectedProduct.Id);
        }

        private void AddNewItemToCart()
        {
            var cartItem = new CartItemDisplayModel
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
            SelectedCart.Product.QuantityInStock += SelectedCart.QuantityInCart;
            Cart?.Remove(SelectedCart);
            NotifyAmount();
            NotifyOfPropertyChange(() => CanCheckout);
        }
        //make sure something is selected from cart
        public bool CanRemoveFromCart => IsItemSelectedFromCart();

        private bool IsItemSelectedFromCart()
        {
            return SelectedCart != null;
        }


        public async Task Checkout()
        {
            //create sale Model and post it to the api 
            var sale = new SaleModel();
            Cart.ToList().ForEach(model =>
            {
                sale.SaleDetails.Add(new SaleDetailModel
                {
                    ProductId = model.Product.Id,
                    Quantity = model.QuantityInCart
                });
            });
            await _saleApi.PostSale(sale);
            await ResetSalesViewModel();
        }

        public bool CanCheckout => IsCartValid();
        private bool IsCartValid()
        {
            return Cart.Count > 0;
        }

        #endregion

        #region Methods

        private void NotifyAmount()
        {
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
        }

        #endregion

        #region Reset

        private async Task ResetSalesViewModel()
        { 
            OnInitialize();
            NotifyAmount();
        }

        #endregion
    }
}
