using EBZ.Mobile.Models;
using EBZ.Mobile.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EBZ.Mobile.ViewModels.Sales
{
    [Preserve(AllMembers = true)]
    public class PaymentPageModel : INotifyPropertyChanged
    {
        #region Fields
        private string _customerEmail;
        private string _product;
        private string _unitCost;
        private string _accountBalance;
        private double _inputQuantity;
        private double _inputAmount;
        private string _inputSalesPin;
        private string _inputCustomerPin;
        private string _inputSalesPinVerify;
        private string _inputCustomerPinVerify;
        private string _inputSalesPinVerifyColor;
        private string _inputCustomerPinVerifyColor;
        private bool _isSalesPinEnabled;
        private bool _isCustomerPinEnabled;
        private bool _isPayBtnEnabled;
        private bool _isBuyInputEnabled;
        private CustomerPricing _customersPricing;
        #endregion

        #region Constructor
        //DateModel _dateModel = new DateModel();
        DialogService _dialogService = new DialogService();
        SalesDataService _salesDataService = new SalesDataService();

        public PaymentPageModel()
        {
            this.PayCommand = new Command(this.PayCommandClicked);
            InitializeData();
        }

        #endregion

        #region Property

        public CustomerPricing CustomersPricing
        {
            get { return this._customersPricing; }
            set
            {
                if (this._customersPricing == value)
                {
                    return;
                }
                this._customersPricing = value;
                this.OnPropertyChanged();
            }
        }

        public string Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged();
            }
        }

        public string CustomerEmail
        {
            get { return this._customerEmail; }
            set
            {
                if (this._customerEmail != value)
                {
                    return;
                }
                this._customerEmail = value;
                this.OnPropertyChanged();
            }
        }

        public string UnitCost
        {
            get { return this._unitCost; }
            set
            {
                if (this._unitCost == value)
                {
                    return;
                }
                this._unitCost = value;
                this.OnPropertyChanged();
            }
        }

        public string AccountBalance
        {
            get { return this._accountBalance; }
            set
            {
                if (this._accountBalance == value)
                {
                    return;
                }
                this._accountBalance = value;
                this.OnPropertyChanged();
            }
        }

        public double InputQuantity
        {
            get { return this._inputQuantity; }
            set
            {
                if (this._inputQuantity == value)
                {
                    return;
                }
                this._inputQuantity = value;
                this.OnPropertyChanged();
                InputQuantityValueChanged();
            }
        }

        public double InputAmount
        {
            get { return this._inputAmount; }
            set
            {
                if (this._inputAmount == value)
                {
                    return;
                }
                this._inputAmount = value;
                this.OnPropertyChanged();
                InputAmountValueChanged();
            }
        }

        public string InputSalesPin
        {
            get { return this._inputSalesPin; }
            set
            {
                if (this._inputSalesPin == value)
                {
                    return;
                }
                this._inputSalesPin = value;
                this.OnPropertyChanged();
                SalesPinValidation();
            }
        }
        public string InputCustomerPin
        {
            get { return this._inputCustomerPin; }
            set
            {
                if (this._inputCustomerPin == value)
                {
                    return;
                }
                this._inputCustomerPin = value;
                this.OnPropertyChanged();
                CustomerPinValidation();
            }
        }

        public string InputSalesPinVerify
        {
            get { return this._inputSalesPinVerify; }
            set
            {
                if (this._inputSalesPinVerify == value)
                {
                    return;
                }
                this._inputSalesPinVerify = value;
                this.OnPropertyChanged();
            }
        }

        public string InputCustomerPinVerify
        {
            get { return this._inputCustomerPinVerify; }
            set
            {
                if (this._inputCustomerPinVerify == value)
                {
                    return;
                }
                this._inputCustomerPinVerify = value;
                this.OnPropertyChanged();
            }
        }

        public string InputSalesPinVerifyColor
        {
            get { return this._inputSalesPinVerifyColor; }
            set
            {
                if (this._inputSalesPinVerifyColor == value)
                {
                    return;
                }
                this._inputSalesPinVerifyColor = value;
                this.OnPropertyChanged();
            }
        }

        public string InputCustomerPinVerifyColor
        {
            get { return this._inputCustomerPinVerifyColor; }
            set
            {
                if (this._inputCustomerPinVerifyColor == value)
                {
                    return;
                }
                this._inputCustomerPinVerifyColor = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsSalesPinEnabled
        {
            get { return this._isSalesPinEnabled; }
            set
            {
                if (this._isSalesPinEnabled == value)
                {
                    return;
                }
                this._isSalesPinEnabled = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsCustomerPinEnabled
        {
            get { return this._isCustomerPinEnabled; }
            set
            {
                if (this._isCustomerPinEnabled == value)
                {
                    return;
                }
                this._isCustomerPinEnabled = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsPayBtnEnabled
        {
            get { return this._isPayBtnEnabled; }
            set
            {
                if(this._isPayBtnEnabled == value)
                {
                    return;
                }
                this._isPayBtnEnabled = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsBuyInputEnabled
        {
            get { return this._isBuyInputEnabled; }
            set
            {
                if(this._isBuyInputEnabled == value)
                {
                    return;
                }
                this._isBuyInputEnabled = value;
                this.OnPropertyChanged();
            }
        }
        #endregion

        #region Command
        public Command PayCommand { get; set; }
        #endregion

        #region Methods
        
        public async void SalesPinValidation()
        {
            //check if pin length is upto 4 characters
            if (this.InputSalesPin != null && this.InputSalesPin.Length == 4)
            {
                //start to verify pin
                _dialogService.ShowLoading("Verifying...");
                var result = await _salesDataService.ValidateSalesPin(this.InputSalesPin);
                if (result != null)
                {
                    _dialogService.HideLoading();
                    this.InputSalesPinVerifyColor = "Green";
                    this.InputSalesPinVerify = "Verified";
                    IsSalesPinEnabled = false;
                    IsBuyInputEnabled = false;
                    IsCustomerPinEnabled = true;
                }
                else
                {
                    _dialogService.HideLoading();
                    _dialogService.ShowToast("Sorry! Sales PIN is NOT correct. Please try again.");
                }
            }
        }

        public async void CustomerPinValidation()
        {
            //check if pin length is upto 4 characters
            if (this.InputCustomerPin != null && this.InputCustomerPin.Length == 4)
            {
                //start to verify pin
                _dialogService.ShowLoading("Verifying...");
                var result = await _salesDataService.ValidateCustomersPin(CustomerEmail, this.InputCustomerPin);
                if (result != null)
                {
                    _dialogService.HideLoading();
                    this.InputCustomerPinVerifyColor = "Green";
                    this.InputCustomerPinVerify = "Verified";
                    IsCustomerPinEnabled = false;
                    IsPayBtnEnabled = true;
                }
                else
                {
                    _dialogService.HideLoading();
                    _dialogService.ShowToast("Sorry! Customer PIN is NOT correct. Please try again.");
                }
            }
        }

        private void InitializeData()
        {
            this.InputSalesPinVerifyColor = "Red";
            this.InputSalesPinVerify = "Unverified";
            this.InputCustomerPinVerifyColor = "Red";           
            this.InputCustomerPinVerify = "Unverified";

            IsCustomerPinEnabled = false;
            IsSalesPinEnabled = false;
            IsPayBtnEnabled = false;
            IsBuyInputEnabled = true;

            if (Application.Current.Properties.ContainsKey("transSelectedCustomerPricing"))
            {
                _customersPricing = (CustomerPricing)Application.Current.Properties["transSelectedCustomerPricing"];
                _customerEmail = (string)Application.Current.Properties["transCustomer"];

                Application.Current.Properties["transSelectedCustomerPricing"] = null;
                Application.Current.Properties["transCustomer"] = null;

                this.CustomerEmail = _customerEmail;
                this.Product = _customersPricing.ProductName;
                this.UnitCost = _customersPricing.CostView;
                this.AccountBalance = _customersPricing.CustomerBalanceView;
            }
        }

        private void InputAmountValueChanged()
        {
            if (InputAmount > CustomersPricing.CustomerBalance)
            {
                _dialogService.ShowToast("Customer balance is not enough for this transaction.");
                InputAmount = 0;
            }
            else
            {
                InputQuantity = InputAmount / CustomersPricing.Cost.Value;
                IsSalesPinEnabled = true;
            }
        }

        private void InputQuantityValueChanged()
        {
            if (InputQuantity == 0)
                IsSalesPinEnabled = false;
            InputAmount = InputQuantity * CustomersPricing.Cost.Value;
            IsCustomerBalanceEnough(InputAmount);
        }

        private void IsCustomerBalanceEnough(double newValue)
        {
            if (CustomersPricing.CustomerBalance < newValue)
            {
                _dialogService.ShowToast("Customer balance is not enough for this transaction.");
                InputQuantity = 0;
            }
            else
            {
                IsSalesPinEnabled = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void PayCommandClicked(object obj)
        {
            var navServ = App.ViewNavigationService;
            _dialogService.ShowLoading("Transacting...");
            var result = await _salesDataService.PayForProduct(InputSalesPin, CustomerEmail, CustomersPricing.PricingId, CustomersPricing.Cost.Value, CustomersPricing.ProductUom, InputQuantity);
            if (result != null)
            {
                _dialogService.HideLoading();
                await navServ.NavigateAsync("PaymentSuccessfulPage");
            }
            else
            {
                _dialogService.HideLoading();
                await navServ.NavigateAsync("PaymentFailedPage");
            }
        }
        #endregion
    }
}