using EBZ.Mobile.Extensions;
using EBZ.Mobile.Models;
using EBZ.Mobile.Services;
using EBZ.Mobile.ServicesInterface;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Model = EBZ.Mobile.Models.Marketer.Article;

namespace EBZ.Mobile.ViewModels.Marketer
{
    /// <summary>
    /// ViewModel for Article bookmark page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class CustomersListPageModel : INotifyPropertyChanged
    {
        #region Fields
        CustomerDataService customerDataService = new CustomerDataService();
        UserService _userService = new UserService();
        SettingsService _settingsService = new SettingsService();
        DialogService _dialogService = new DialogService();
        NavigationService _navigationService = new NavigationService();

        public ObservableCollection<MarketerCustomer> _customers { get; set; }
        private ObservableCollection<Model> latestStories;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="BookmarksViewModel" /> class.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the property that has been bound with the list view, which displays the articles' latest stories items.
        /// </summary>

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

        public ObservableCollection<Model> LatestStories
        {
            get { return this.latestStories; }

            set
            {
                if (this.latestStories == value)
                {
                    return;
                }

                this.latestStories = value;
                this.NotifyPropertyChanged();
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
                _dialogService.ShowLoading("Loading...");
                var custs = await customerDataService.GetCustomersForMarketer(_userService.LoggedInUser());
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

        /// <summary>
        /// The PropertyChanged event occurs when changing the value of property.
        /// </summary>
        /// <param name="propertyName">Property name</param>
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Invoked when the bookmark button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void AddButtonClicked(object obj)
        {
            //ViewNavigationService.Configure("NewCustomerPage", typeof(Views.Marketer.NewCustomerPage));
            await _navigationService.NavigateModalAsync("NewCustomerPage", "");
        }

        private void BookmarkButtonClicked(object obj)
        {
            if (obj is Model article)
            {
                article.IsBookmarked = !article.IsBookmarked;
            }
        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        private void ItemSelected(object obj)
        {
            // Do something
        }

        //public static INavigationService ViewNavigationService { get; } = new NavigationService();
        #endregion
    }
}