using Syncfusion.XForms.TabView;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EBZ.Mobile
{
    public partial class MainPage : TabbedPage
    {
        //private SfTabView tabView;
        public MainPage()
        {
            //Customize layout
            this.BackgroundColor = Color.FromHex("#20FFFFFF");
            this.BarTextColor = Color.FromHex("#012E8B");
            this.BarBackgroundColor = Color.FromHex("#20FFFFDD");
            this.SelectedTabColor = Color.Yellow;
            this.UnselectedTabColor = Color.Turquoise;
            this.Title = "EBZ";

            //get user roles
            if (Application.Current.Properties.ContainsKey("userRoles"))
            {
                List<string> roles = (List<string>)Application.Current.Properties["userRoles"];
                var rols = roles;
            }
                

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
