using Akavache;
using EBZ.Mobile.AppStart;
using EBZ.Mobile.Services;
using EBZ.Mobile.ServicesInterface;
using Xamarin.Forms;

namespace EBZ.Mobile
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = App.BaseImageUrl + "https://raw.githubusercontent.com/sumathij/essential-ui-kit-for-xamarin.forms/master/Images/";
        UtilityService _utilityService = new UtilityService();

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTM5MTQ4QDMxMzcyZTMyMmUzMGdMOURWSEZhZnRRdEkzaVBFZ2tteVBRNG1hZHFYbHo5eGhZdDhhV29JZlE9;MTM5MTQ5QDMxMzcyZTMyMmUzMGN2SmZFOUpaSG5vZzJTcGh1U0Z2ZG43OGdVTUJRQmRrdmxERHZTODQyNmc9;MTM5MTUwQDMxMzcyZTMyMmUzMExwTit3ZW5ncjBqS09kVGRFMEpwaHpVVW5Bb055WkpiTVVQZXZ5aFUzWGc9;MTM5MTUxQDMxMzcyZTMyMmUzMGpPbEd3RFlEc09nTWRDSFJuQ3hKWGl3alhhOXVkTCt4dUFUSFhGbEtwZGs9;MTM5MTUyQDMxMzcyZTMyMmUzMFM3RDd6V29VNmo3akNpN2tOUFVIMytXTFY4cXpJREhIaWprVmw3bDljdW89;MTM5MTUzQDMxMzcyZTMyMmUzMFhrMkVodk53RW5JZzlGT21TRHZHM21OZmRPV3k3eUJOR3pLcm00cFlVTG89;MTM5MTU0QDMxMzcyZTMyMmUzMGtQOXV2aXlIcGdnMUF2M255eS9kT3R0eDJ2d0hhc0ZLZzF6Z09iTlVNVjg9;MTM5MTU1QDMxMzcyZTMyMmUzMGUyWVBYVFc3ZzlpY25wbUg0bm1JT1VJM1pheVdRd3FuNStpcHNMcWZzQ009;MTM5MTU2QDMxMzcyZTMyMmUzMFBpT0hpTG5sM3R6RzVlTVpmSWNvL1FWNnRYK2tEUkcxRS84eGwrOHNXWEk9;MTM5MTU3QDMxMzcyZTMyMmUzMFM3RDd6V29VNmo3akNpN2tOUFVIMytXTFY4cXpJREhIaWprVmw3bDljdW89");
            BlobCache.ApplicationName = "EBZmobile";
            AppContainer.RegisterDependencies();
            RegisterViews();
            InitializeComponent();
            StartupControl();
            //var page = ((NavigationService)ViewNavigationService).SetRootPage("StartSalesPage");
            //MainPage = page;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void StartupControl()
        {
            if (_utilityService.IsTokenExpired())
            {
                var loginPage = ((NavigationService)ViewNavigationService).SetRootPage("SimpleLoginPage");
                MainPage = loginPage;
            }
            else if (_utilityService.IsAuthenticated())
            {
                var mainPage = ((NavigationService)ViewNavigationService).SetRootPage("MainPage");
                MainPage = mainPage;
            }
            else
            {
                var loginPage = ((NavigationService)ViewNavigationService).SetRootPage("SimpleLoginPage");
                MainPage = loginPage;
            }
        }

        public void SetAppRootPage()
        {
            var mainPage = ((NavigationService)ViewNavigationService).SetRootPage("MainPage");
            MainPage = mainPage;
        }

        public void RegisterViews()
        {
            ViewNavigationService.Configure("MainPage", typeof(MainPage));
            ViewNavigationService.Configure("ProfilePage", typeof(Views.User.ProfilePage));
            ViewNavigationService.Configure("SimpleLoginPage", typeof(Views.Login.SimpleLoginPage));
            ViewNavigationService.Configure("SimpleSignUpPage", typeof(Views.Login.SimpleSignUpPage));
            ViewNavigationService.Configure("SimpleResetPasswordPage", typeof(Views.Login.SimpleResetPasswordPage));
            ViewNavigationService.Configure("SimpleForgotPasswordPage", typeof(Views.Login.SimpleForgotPasswordPage));            
            ViewNavigationService.Configure("CustomersListPage", typeof(Views.Marketer.CustomersListPage));
            ViewNavigationService.Configure("NewCustomerPage", typeof(Views.Marketer.NewCustomerPage));
            ViewNavigationService.Configure("StartSalesPage", typeof(Views.Sales.StartSalesPage));
            ViewNavigationService.Configure("SelectProductPage", typeof(Views.Sales.SelectProductPage));
            ViewNavigationService.Configure("PaymentPage", typeof(Views.Sales.PaymentPage));
            ViewNavigationService.Configure("PaymentFailedPage", typeof(Views.Sales.PaymentFailedPage));
            ViewNavigationService.Configure("PaymentSuccessfulPage", typeof(Views.Sales.PaymentSuccessfulPage));
            
        }

        public static INavigationService ViewNavigationService { get; } = new NavigationService();
    }
}
