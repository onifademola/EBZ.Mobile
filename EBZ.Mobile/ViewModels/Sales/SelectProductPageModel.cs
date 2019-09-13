using EBZ.Mobile.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using EBZ.Mobile.Services;
using EBZ.Mobile.Extensions;

namespace EBZ.Mobile.ViewModels.Sales
{
    public class SelectProductPageModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<CustomerPricing> _customerPricings { get; set; }
        string _customerEmail;
        private Command itemTappedCommand;

        public Command ItemTappedCommand
        {
            get { return itemTappedCommand; }
            protected set { itemTappedCommand = value; }
        }


        public ObservableCollection<CustomerPricing> CustomerPricings
        {
            get
            {
                return _customerPricings;
            }
            set
            {
                if (value != _customerPricings)
                {
                    _customerPricings = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public Command<Object> NavigateToSelectModelsCommand { get; private set; }

        public SelectProductPageModel()
        {
            itemTappedCommand = new Command(OnItemTapped);
            InitializeData();
        }

        private async void InitializeData()
        {
            DialogService _dialogService = new DialogService();
            CustomerDataService _customerDataService = new CustomerDataService();

            if (Application.Current.Properties.ContainsKey("transCustomer"))
            {
                _customerEmail = (string)Application.Current.Properties["transCustomer"];

                _dialogService.ShowLoading("Loading...");
                var pricings = await _customerDataService.GetCustomersPricing(_customerEmail);
                if (pricings != null)
                {
                    CustomerPricings = pricings.ToObservableCollection();
                    _dialogService.HideLoading();
                }
                else
                {
                    _dialogService.HideLoading();
                    await _dialogService.ShowDialog("Not found", "Customer information does not exist.", "Ok");
                    var navServ = App.ViewNavigationService;
                    await navServ.GoBack();
                }
            }

            else
            {
                await _dialogService.ShowDialog("Not found", "Customer information does not exist.", "Ok");
                var navServ = App.ViewNavigationService;
                await navServ.GoBack();
            }
        }
        
        public async void OnItemTapped(object obj)
        {
            var customerPricing = obj as CustomerPricing;
            Application.Current.Properties["transSelectedCustomerPricing"] = customerPricing;
            var navServ = App.ViewNavigationService;
            await navServ.NavigateModalAsync("PaymentPage");
        }
    }
}
