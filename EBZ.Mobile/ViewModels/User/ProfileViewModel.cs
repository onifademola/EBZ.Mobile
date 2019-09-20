using EBZ.Mobile.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EBZ.Mobile.ViewModels.User
{
    /// <summary>
    /// ViewModel for Article profile page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class ProfileViewModel : INotifyPropertyChanged
    {
        #region Fields

        private string profileImage;
        private string profileName;
        private string email;

        #endregion

        #region Constructor
        StorageService _storageService = new StorageService();


        public ProfileViewModel()
        {
            this.profileImage = App.BaseImageUrl + "ProfileImage1.png";
            this.profileName = "John Deo";
            this.email = _storageService.GetFromCache<string>("username").Result;

            this.CustomerCommand = new Command(this.CustomerButtonClicked);
            this.TransactionCommand = new Command(this.TransactionOptionClicked);
            this.SalesCommand = new Command(this.SalesOptionClicked);
            this.SettingsCommand = new Command(this.SettingsOptionClicked);
        }

        #endregion

        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Public properties
                
        public string ProfileImage
        {
            get
            {
                return this.profileImage;
            }

            set
            {
                if (this.profileImage != value)
                {
                    this.profileImage = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
               
        public string ProfileName
        {
            get
            {
                return this.profileName;
            }

            set
            {
                if (this.profileName != value)
                {
                    this.profileName = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        
        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                if (this.email != value)
                {
                    this.email = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Command
        public Command CustomerCommand { get; set; }
        public Command TransactionCommand { get; set; }
        public Command SettingsCommand { get; set; }
        public Command SalesCommand { get; set; }
        #endregion

        #region Methods

        private async void CustomerButtonClicked(object obj)
        {
            var navServ = App.ViewNavigationService;
            await navServ.NavigateAsync("CustomersListPage");
        }

        private void TransactionOptionClicked(object obj)
        {
            // Do something
        }

        private async void SalesOptionClicked(object obj)
        {
            var grid = obj as Grid;
            Application.Current.Resources.TryGetValue("Gray-F4", out var retVal);
            grid.BackgroundColor = (Color)retVal;
            //To make the selected item color changes for 100 milliseconds.
            await Task.Delay(100);
            Application.Current.Resources.TryGetValue("Gray-White", out var retValue);
            grid.BackgroundColor = (Color)retValue;
        }
                
        private async void SettingsOptionClicked(object obj)
        {
            var grid = obj as Grid;
            Application.Current.Resources.TryGetValue("Gray-F4", out var retVal);
            grid.BackgroundColor = (Color)retVal;
            //To make the selected item color changes for 100 milliseconds.
            await Task.Delay(100);
            Application.Current.Resources.TryGetValue("Gray-White", out var retValue);
            grid.BackgroundColor = (Color)retValue;
        }
                
        private void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
