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
    /// <summary>
    /// Page to sign in with user details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCustomerPage
    {
        DateModel _dateModel = new DateModel();
        CustomerDataService _customerDataService = new CustomerDataService();
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSignUpPage" /> class.
        /// </summary>
        public NewCustomerPage()
        {
            this.InitializeComponent();
            LoadControls();
        }

        private async void LoadControls()
        {
            dobDay.ItemsSource = _dateModel.DayPicker();
            dobMonth.ItemsSource = _dateModel.MonthPicker();

            IList<Category> categoryList = await _customerDataService.GetCustomerCategories();
            catPicker.ItemsSource = categoryList.ToList();
        }

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            var navServ = App.ViewNavigationService;
            navServ.GoBack();
            return true;
        }
    }
}