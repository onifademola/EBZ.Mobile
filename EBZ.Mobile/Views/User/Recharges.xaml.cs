using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EBZ.Mobile.Services;
using EBZ.Mobile.Models;
using EBZ.Mobile.Extensions;

namespace EBZ.Mobile.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Recharges : ContentPage
    {
        public Recharges()
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

        private ObservableCollection<Recharge> _recharges { get; set; }

        public ObservableCollection<Recharge> Recharge
        {
            get
            {
                return _recharges;
            }
            set
            {
                if (value != _recharges)
                {
                    _recharges = value;
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
            if (Recharge == null)
                return true;
            var cnt = Recharge.Count;
            if (cnt > 0)
                return false;
            return true;
        }

        private async void InitializePage()
        {
            _dialogService.ShowLoading("Loading...");
            var rchrgs = await customerDataService.MyRecentRecharges(_settingsService.UserNameSetting);
            if (rchrgs != null)
            {
                Recharge = rchrgs.ToObservableCollection();
                listViewCtrl.ItemsSource = Recharge;
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