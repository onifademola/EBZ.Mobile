using EBZ.Mobile.Models;
using EBZ.Mobile.Services;
using EBZ.Mobile.ViewModels.Login;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EBZ.Mobile.ViewModels.Marketer
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class NewCustomerPageModel : LoginViewModel
    {
        #region Fields

        private string name;
        private string password;
        private string phone;
        private string selectedDay;
        private string selectedMonth;
        private Category selectedCategory;
        //public IList<Category> CategoryList;
        
        #endregion
        #region Constructor
        DateModel _dateModel = new DateModel();
        DialogService _dialogService = new DialogService();
        AuthenticationService _authenticationService = new AuthenticationService();
        SettingsService _settingsService = new SettingsService();

        
        public NewCustomerPageModel()
        {            
            this.CreateCommand = new Command(this.CreateClicked);
            //CategoryList = Task.Run(async () => await _customerDataService.GetCustomerCategories()).Result;
        }

        #endregion

        #region Property

        public string SelectedDay
        {
            get { return this.selectedDay; }
            set
            {
                if (this.selectedDay == value)
                {
                    return;
                }
                this.selectedDay = value;
                this.OnPropertyChanged();
            }
        }

        public string SelectedMonth
        {
            get { return this.selectedMonth; }
            set
            {
                if (this.selectedMonth == value)
                {
                    return;
                }
                this.selectedMonth = value;
                this.OnPropertyChanged();
            }
        }

        public Category SelectedCategory
        {
            get
            {
                return this.selectedCategory;
            }
            set
            {
                if (this.selectedCategory == value)
                {
                    return;
                }
                this.selectedCategory = value;
                this.OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name == value)
                {
                    return;
                }

                this.name = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password confirmation from users in the Sign Up page.
        /// </summary>
        public string Phone
        {
            get
            {
                return this.phone;
            }

            set
            {
                if (this.phone == value)
                {
                    return;
                }

                this.phone = value;
                this.OnPropertyChanged();
            }
        }

        #endregion

        #region Command
        public Command CreateCommand { get; set; }
        #endregion

        #region Methods

        private async void CreateClicked(object obj)
        {
            if (Email.Equals(string.Empty) || Password.Equals(string.Empty) || Phone.Equals(string.Empty))
            {
                await _dialogService.ShowDialog(
                        "This username/password/phone combination must be entered",
                        "Signup Error",
                        "OK");
            }
            else
            {
                try
                {
                    _dialogService.ShowLoading("Registering...");
                    string birthDay = SelectedDay;
                    string birthMonth = SelectedMonth;
                    int categoryId = SelectedCategory.Id;

                    var authenticationResponse = await _authenticationService.Register(Email, Password, Phone, birthDay, birthMonth, categoryId);

                    if (authenticationResponse.IsAuthenticated)
                    {
                        // we store the Id to know if the user is already logged in to the application
                        _settingsService.UserNameSetting = authenticationResponse.Username;
                        _settingsService.TokenSetting = authenticationResponse.Token;
                        _settingsService.ValidToSetting = authenticationResponse.ValidTo.ToShortDateString();
                        _settingsService.RolesSetting = authenticationResponse.Role;

                        _dialogService.HideLoading();
                        var viewNAvServ = App.ViewNavigationService;
                        var mainPage = ((NavigationService)viewNAvServ).SetRootPage("MainPage");
                        App.Current.MainPage = mainPage;
                    }
                    else
                    {
                        _dialogService.HideLoading();
                        await _dialogService.ShowDialog(
                        "This username/password combination is not valid",
                        "Error logging you in",
                        "OK");
                    }
                }
                catch (System.Exception)
                {
                    _dialogService.HideLoading();
                    await _dialogService.ShowDialog(
                    "This username/password combination is not valid",
                    "Error logging you in",
                    "OK");
                }
            }
        }

        #endregion
    }
}