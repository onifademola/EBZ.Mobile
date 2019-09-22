using EBZ.Mobile.Extensions;
using EBZ.Mobile.Models;
using EBZ.Mobile.Services;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EBZ.Mobile.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Transactions : ContentPage
    {
        public Transactions()
        {
            InitializeComponent();
            listViewCtrl.IsPullToRefreshEnabled = true;
            listViewCtrl.RefreshControlColor = Color.Transparent;
            listViewCtrl.RefreshCommand = new Command(() => {
                InitializePage();
                listViewCtrl.IsRefreshing = false;
            });
        }

        CustomerDataService customerDataService = new CustomerDataService();
        SettingsService _settingsService = new SettingsService();
        DialogService _dialogService = new DialogService();

        private ObservableCollection<Sale> _sales { get; set; }

        public ObservableCollection<Sale> Sales
        {
            get
            {
                return _sales;
            }
            set
            {
                if (value != _sales)
                {
                    _sales = value;
                    OnPropertyChanged();
                }
            }
        }

        protected override void OnAppearing()
        {
            if (isSalesEmpty())
            {
                InitializePage();
            }
            base.OnAppearing();
        }

        public bool isSalesEmpty()
        {
            if (Sales == null)
                return true;
            var cnt = Sales.Count;
            if (cnt > 0)
                return false;
            return true;
        }

        private async void InitializePage()
        {
            _dialogService.ShowLoading("Loading...");
            var pricings = await customerDataService.MyRecentTransactions(_settingsService.UserNameSetting);
            if (pricings != null)
            {
                Sales = pricings.ToObservableCollection();
                listViewCtrl.ItemsSource = Sales;
                _dialogService.HideLoading();
            }
            else
            {
                _dialogService.HideLoading();
                await _dialogService.ShowDialog("Not found", "Customer information does not exist.", "Ok");
            }
        }
    }
}