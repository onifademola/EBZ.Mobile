using EBZ.Mobile.Extensions;
using EBZ.Mobile.Models;
using EBZ.Mobile.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EBZ.Mobile.ViewModels.Marketer
{

    [Preserve(AllMembers = true)]
    public class CustomersListPageModel : INotifyPropertyChanged
    {
        #region Fields
        CustomerDataService customerDataService = new CustomerDataService();
        UserService _userService = new UserService();
        SettingsService _settingsService = new SettingsService();
        DialogService _dialogService = new DialogService();
        StorageService _storageService = new StorageService();
        private string username;
        public ObservableCollection<MarketerCustomer> _customers { get; set; }

        #endregion

        #region Constructor

        public CustomersListPageModel()
        {
            
            if (isCustomersEmpty())
            {
                LoadData();
            }
            this.AddCommand = new Command(this.AddButtonClicked);
            this.BookmarkCommand = new Command(this.BookmarkButtonClicked);
            this.ItemSelectedCommand = new Command(this.ItemSelected);
        }

        #endregion

        #region Event

        /// <summary>
        /// The declaration of the property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties        
                
        public ObservableCollection<MarketerCustomer> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                if (value != _customers)
                {
                    _customers = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when the bookmark button is clicked.
        /// </summary>
        public Command AddCommand { get; set; }
        public Command BookmarkCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when an item is selected.
        /// </summary>
        public Command ItemSelectedCommand { get; set; }

        #endregion

        #region Methods

        public bool isCustomersEmpty()
        {
            if (Customers == null)
                return true;
            var cnt = Customers.Count;
            if (cnt > 0)
                return false;
            return true;
        }

        public async void LoadData()
        {
            if (_userService.IsAuthenticated())
            {
                 username = await _storageService.GetFromCache<string>("username");

                _dialogService.ShowLoading("Loading...");
                var custs = await customerDataService.GetCustomersForMarketer(username);
                if (custs != null)
                {
                    Customers = custs.ToObservableCollection();
                    _dialogService.HideLoading();
                }
                else
                {
                    _dialogService.HideLoading();
                    _dialogService.ShowToast("Hey! You have not yet added any Customer.");
                }
            }
        }

        
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void AddButtonClicked(object obj)
        {
            var navServ = App.ViewNavigationService;
            await navServ.NavigateModalAsync("NewCustomerPage");
        }

        private void BookmarkButtonClicked(object obj)
        {
            
        }


        private void ItemSelected(object obj)
        {
            // Do something
        }

        #endregion
    }
}