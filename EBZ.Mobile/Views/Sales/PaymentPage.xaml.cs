using EBZ.Mobile.Models;
using EBZ.Mobile.Services;
using EBZ.Mobile.ViewModels.Sales;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EBZ.Mobile.Views.Sales
{
    //[Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentPage
    {
        public PaymentPage()
        {
            this.InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            var navServ = App.ViewNavigationService;
            navServ.GoToRoot();
            return true;
        }
    }
}