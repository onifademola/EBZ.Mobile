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
        private string _inputQuantity;
        private string _inputAmount;
        private string _inputSalesPin;
        private string _inputCustomerPin;
        private string _inputSalesPinVerify;
        private string _inputCustomerPinVerify;
        private string _inputSalesPinVerifyColor;
        private string _inputCustomerPinVerifyColor;
        private bool _isCustomerPinEnable;
        private CustomerPricing _customersPricing;
        #endregion

        #region Constructor
        DateModel _dateModel = new DateModel();
        DialogService _dialogService = new DialogService();
        AuthenticationService _authenticationService = new AuthenticationService();
        SettingsService _settingsService = new SettingsService();
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
                if(this._customersPricing == value)
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
                if(this._unitCost == value)
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
                if(this._accountBalance == value)
                {
                    return;
                }
                this._accountBalance = value;
                this.OnPropertyChanged();
            }
        }

        public string InputQuantity
        {
            get { return this._inputQuantity; }
            set
            {
                if(this._inputQuantity == value)
                {
                    return;
                }
                this._inputQuantity = value;
                this.OnPropertyChanged();
            }
        }

        public string InputAmount
        {
            get { return this._inputAmount; }
            set
            {
                if(this._inputAmount == value)
                {
                    return;
                }
                this._inputAmount = value;
                this.OnPropertyChanged();
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
                if(this._inputCustomerPin == value)
                {
                    return;
                }
                this._inputCustomerPin = value;
                this.OnPropertyChanged();
            }
        }

        public string InputSalesPinVerify
        {
            get { return this._inputSalesPinVerify; }
            set
            {
                if(this._inputSalesPinVerify == value)
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
                if(this._inputCustomerPinVerify == value)
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

        public bool IsCustomerPinEnable
        {
            get { return this._isCustomerPinEnable; }
            set
            {
                if(this._isCustomerPinEnable == value)
                {
                    return;
                }
                this._isCustomerPinEnable = value;
                this.OnPropertyChanged();
            }
        }
        #endregion

        #region Command
        public Command PayCommand { get; set; }
        #endregion

        #region Methods
        public void ManipulateProperties()
        {
            SalesPinValidation();
            CustomerPinValidation();
        }
        
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
                    //UserDialogs.Instance.HideLoading();
                    //entrySalesPin.IsEnabled = false;

                    //btnVerifySalesPin.Text = "Verified";
                    //btnVerifySalesPin.TextColor = Color.ForestGreen;
                    //btnVerifySalesPin.IsEnabled = false;

                    //entryCustomerPin.IsEnabled = true;
                    //entryCustomerPin.Focus();
                }
                else
                {
                    _dialogService.HideLoading();
                    _dialogService.ShowToast("Sorry! Sales PIN is NOT correct. Please try again.");
                    //entrySalesPin.Focus();
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
                    this.InputSalesPinVerifyColor = "Green";
                    this.InputSalesPinVerify = "Verified";
                    IsCustomerPinEnable = true;
                    //UserDialogs.Instance.HideLoading();
                    //entrySalesPin.IsEnabled = false;

                    //btnVerifySalesPin.Text = "Verified";
                    //btnVerifySalesPin.TextColor = Color.ForestGreen;
                    //btnVerifySalesPin.IsEnabled = false;

                    //entryCustomerPin.IsEnabled = true;
                    //entryCustomerPin.Focus();
                }
                else
                {
                    _dialogService.HideLoading();
                    _dialogService.ShowToast("Sorry! Customer PIN is NOT correct. Please try again.");
                    //entrySalesPin.Focus();
                }
            }
        }

        private void InitializeData()
        {
            this.InputSalesPinVerifyColor = "Red";
            this.InputSalesPinVerify = "Unverified";
            this.InputCustomerPinVerifyColor = "Red";           
            this.InputCustomerPinVerify = "Unverified";

            IsCustomerPinEnable = false;

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PayCommandClicked(object obj)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}