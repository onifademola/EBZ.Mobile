using EBZ.Mobile.Services;
using Syncfusion.XForms.TabView;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EBZ.Mobile
{
    public partial class MainPage : TabbedPage
    {
        string[] roles = null;

        
        public MainPage()
        {
            //Customize layout
            this.BackgroundColor = Color.FromHex("#20FFFFFF");
            this.BarTextColor = Color.FromHex("#012E8B");
            this.BarBackgroundColor = Color.FromHex("#20FFFFDD");
            this.SelectedTabColor = Color.Yellow;
            this.UnselectedTabColor = Color.Turquoise;
            this.Title = "EBZ";

            StorageService _storageService = new StorageService();
            Task.Run(async () => { roles = await _storageService.GetFromCache<string[]>("userRoles"); });

            var marketersPage = new Views.Marketer.CustomersListPage
            {
                Title = "Clients",
                IconImageSource = "",
                
            };

            var shop = new Views.Sales.StartSalesPage();
            shop.Title = "Shop";

            var userPage = new Views.User.ProfilePage();
            userPage.Title = "Profile";

            var userRxPage = new Views.User.Recharges();
            userRxPage.Title = "RX";

            var userTxPage = new Views.User.Transactions();
            userTxPage.Title = "TX";

            //add tabs
            if(UserIsInRole("Sales") == true)
            {
                Children.Add(shop);
            }
            Children.Add(userPage);
            if (UserIsInRole("Customer") == true)
            {
                Children.Add(userTxPage);
                Children.Add(userRxPage);
            }
            if (UserIsInRole("Marketer") == true)
            {
                Children.Add(marketersPage);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        private bool UserIsInRole(string roleName)
        {
            if (roles != null)
            {
                List<string> lst = roles.OfType<string>().ToList();
                var check = lst.Contains(roleName);
                if (check == true)
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}
