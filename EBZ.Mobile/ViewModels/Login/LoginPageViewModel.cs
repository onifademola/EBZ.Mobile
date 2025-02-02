﻿using EBZ.Mobile.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EBZ.Mobile.ViewModels.Login
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginPageViewModel : LoginViewModel
    {
        #region Fields
        private string password;
        private AuthenticationService _authenticationService;
        private SettingsService _settingsService;
        private DialogService _dialogService;
        private ConnectionService _connectionService;
        private StorageService _storageService;
        #endregion

        #region Constructor        
        public LoginPageViewModel()
        {
            _authenticationService = new AuthenticationService();
            _settingsService = new SettingsService();
            _dialogService = new DialogService();
            _connectionService = new ConnectionService();
            _storageService = new StorageService();

            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);
        }

        #endregion

        #region property

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
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


        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the social media login button is clicked.
        /// </summary>
        public Command SocialMediaLoginCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void LoginClicked(object obj)
        {
            _dialogService.ShowLoading("Authenticating...");

            if (_connectionService.IsConnected)
            {
                try
                {
                    var authenticationResponse = await _authenticationService.Authenticate(Email, Password);

                    if (authenticationResponse.IsAuthenticated)
                    {
                        // we store the Id to know if the user is already logged in to the application
                        _settingsService.UserNameSetting = authenticationResponse.Username;
                        _settingsService.TokenSetting = authenticationResponse.Token;
                        _settingsService.ValidToSetting = authenticationResponse.ValidTo.ToShortDateString();
                        //_settingsService.RolesSetting = authenticationResponse.Role;
                        _storageService.InsertIntoCache("userRoles", authenticationResponse.Roles.ToArray());
                        Application.Current.Properties["userRoles"] = authenticationResponse.Roles;
                        _storageService.InsertIntoCache("username", authenticationResponse.Username);


                        var viewNAvServ = App.ViewNavigationService;
                        var mainPage = ((NavigationService)viewNAvServ).SetRootPage("MainPage");

                        _dialogService.HideLoading();
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
            else
            {
                await _dialogService.ShowDialog(
                    "This username/password combination isn't known",
                    "Error logging you in",
                    "OK");
            }

        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            var viewNAvServ = App.ViewNavigationService;
            var mainPage = ((NavigationService)viewNAvServ).NavigateModalAsync("SimpleSignUpPage");
        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void ForgotPasswordClicked(object obj)
        {
            var label = obj as Label;
            label.BackgroundColor = Color.FromHex("#70FFFFFF");
            await Task.Delay(100);
            label.BackgroundColor = Color.Transparent;
            var viewNAvServ = App.ViewNavigationService;
            var mainPage = ((NavigationService)viewNAvServ).NavigateModalAsync("SimpleForgotPasswordPage");
        }

        /// <summary>
        /// Invoked when social media login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SocialLoggedIn(object obj)
        {
            // Do something
        }

        #endregion
    }
}