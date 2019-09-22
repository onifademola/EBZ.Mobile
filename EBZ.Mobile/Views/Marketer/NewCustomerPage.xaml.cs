using EBZ.Mobile.Models;
using EBZ.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EBZ.Mobile.Views.Marketer
{

    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCustomerPage
    {
        CustomerDataService _customerDataService = new CustomerDataService();

        public NewCustomerPage()
        {
            this.InitializeComponent();
            LoadControls();
        }

        private async void LoadControls()
        {
            List<Category> categoryList = await _customerDataService.GetCustomerCategories();
            catPicker.ItemsSource = categoryList.ToList();
        }

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            return false;
        }
    }
}