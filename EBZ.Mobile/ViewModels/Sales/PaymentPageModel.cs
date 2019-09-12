﻿using EBZ.Mobile.Models;
using EBZ.Mobile.Services;
using EBZ.Mobile.ViewModels.Login;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EBZ.Mobile.ViewModels.Sales
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
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
        private CustomerPricing _customersPricing;
        #endregion

        #region Constructor
        DateModel _dateModel = new DateModel();
        DialogService _dialogService = new DialogService();
        AuthenticationService _authenticationService = new AuthenticationService();
        SettingsService _settingsService = new SettingsService();

        
        public PaymentPageModel()
        {
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
                this.NotifyPropertyChanged();
            }
        }

        public string Product
        {
            get { return this._product; }
            set
            {
                if (this._product == value)
                {
                    return;
                }
                this._product = value;
                this.NotifyPropertyChanged();
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
                this.NotifyPropertyChanged();
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
                this.NotifyPropertyChanged();
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
                this.NotifyPropertyChanged();
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
                this.NotifyPropertyChanged();
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
                this.NotifyPropertyChanged();
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
                this.NotifyPropertyChanged();
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
                this.NotifyPropertyChanged();
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
                this.NotifyPropertyChanged();
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
                this.NotifyPropertyChanged();
            }
        }


        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command CreateCommand { get; set; }


        #endregion

        #region Methods

        private void InitializeData()
        {
            if (Application.Current.Properties.ContainsKey("transSelectedCustomerPricing"))
            {
                _customersPricing = (CustomerPricing)Application.Current.Properties["transSelectedCustomerPricing"];

                Application.Current.Properties["transSelectedCustomerPricing"] = null;

                this._product = _customersPricing.ProductName;
                this._unitCost = _customersPricing.ProductUom;
                this._accountBalance = _customersPricing.CustomerBalanceView;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}