using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace EBZ.Mobile.Views.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartSalesPage : ZXingScannerPage
    {
        public ZXingScannerPage scanPage;

        public StartSalesPage()
        {
            InitializeComponent();
            var navServ = App.ViewNavigationService;

            buttonScanDefaultOverlay.Clicked += async delegate
            {
                scanPage = new ZXingScannerPage();
                scanPage.OnScanResult += (result) =>
                {
                    scanPage.IsScanning = false;

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        Application.Current.Properties["transCustomer"] = result.Text;
                        await Navigation.PopAsync();
                        await navServ.NavigateAsync("SelectProductPage");
                    });
                };
                //await navServ.GoBack();
                await Navigation.PushAsync(scanPage);
            };

            //btnBegin.Clicked += async delegate
            //{                
            //    Application.Current.Properties["transCustomer"] = "onifademola@gmail.com";
            //    await navServ.NavigateAsync("SelectProductPage");
            //};
        }
    }
}