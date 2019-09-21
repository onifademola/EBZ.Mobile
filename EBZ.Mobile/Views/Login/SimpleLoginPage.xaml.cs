using EBZ.Mobile.ViewModels.Login;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EBZ.Mobile.Views.Login
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleLoginPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleLoginPage" /> class.
        /// </summary>
        public SimpleLoginPage()
        {
            this.InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            var navServ = App.ViewNavigationService;
            navServ.ClearBackStack4Login();
            return false;
        }
    }
}