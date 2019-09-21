using EBZ.Mobile.ViewModels.Marketer;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EBZ.Mobile.Views.Marketer
{
    /// <summary>
    /// Page to show article bookmark items
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomersListPage
    {
        CustomersListPageModel cbsm = new CustomersListPageModel();

        public CustomersListPage()
        {
            this.InitializeComponent();
        }

        private void PullToRefresh_Refreshing(object sender, System.EventArgs e)
        {            
            cbsm.LoadData();
        }

        private void ContentPage_Appearing(object sender, System.EventArgs e)
        {
            cbsm.StartModel();
        }
    }
}