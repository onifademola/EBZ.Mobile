using Syncfusion.XForms.TabView;
using Xamarin.Forms;

namespace EBZ.Mobile
{
    public partial class MainPage : TabbedPage
    {
        //private SfTabView tabView;
        public MainPage()
        {
            var marketersPage = new Views.Marketer.CustomersListPage();
            marketersPage.Title = "Customers";

            var shop = new Views.Sales.StartSalesPage();
            shop.Title = "Shop";

            //var transactionsPage = new UserView.Transactions();
            //transactionsPage.Title = "Txs";

            //var rechargesPage = new UserView.Recharges();
            //rechargesPage.Title = "Rxs";

            //var mySalesPage = new SalesPage.MySalesTodayPage();
            //mySalesPage.Title = "Sales";

            //Children.Add(new UserView.UserView());
            //if (_userService.LoggedInUserRole() != "Customer")
            //{
            //    Children.Add(marketersPage);
            //}
            Children.Add(marketersPage);
            Children.Add(shop);
            //Children.Add(rechargesPage);
            //Children.Add(mySalesPage);
            //Children.Add(salesPage);
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
    }
}
