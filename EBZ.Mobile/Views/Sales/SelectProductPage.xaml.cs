using EBZ.Mobile.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EBZ.Mobile.Views.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectProductPage : ContentPage
    {
        public SelectProductPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            var navServ = App.ViewNavigationService;
            navServ.GoBack();
            return true;
        }

        private void ListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {

        }
    }
}