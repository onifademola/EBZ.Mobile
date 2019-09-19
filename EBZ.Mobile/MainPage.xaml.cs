using Syncfusion.XForms.TabView;
using Xamarin.Forms;

namespace EBZ.Mobile
{
    public partial class MainPage : TabbedPage
    {
        //private SfTabView tabView;
        public MainPage()
        {
            //Customize layout
            this.BarTextColor = Color.DarkRed;
            this.BarBackgroundColor = Color.SlateGray;
            this.SelectedTabColor = Color.Yellow;
            this.UnselectedTabColor = Color.Turquoise;
            this.Title = "Tab Page";

            var marketersPage = new Views.Marketer.CustomersListPage
            {
                Title = "Customers",
                IconImageSource = "",
                
            };

            var shop = new Views.Sales.StartSalesPage();
            shop.Title = "Shop";

            var userPage = new Views.User.ProfilePage();
            userPage.Title = "Profile";

            Children.Add(userPage);
            Children.Add(marketersPage);
            Children.Add(shop);
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
    }
}
