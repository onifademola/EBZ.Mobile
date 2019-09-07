using EBZ.Mobile.Models;
using EBZ.Mobile.ServicesInterface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EBZ.Mobile.Services
{
    public class CustomerDataService : ICustomerDataService
    {
        private readonly GenericService _genericRepository;
        public CustomerDataService(GenericService genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<List<MarketerCustomer>> GetCustomersForMarketer(string marketerEmail)
        {
            string uri = Constants.BaseApiUrl + Constants.CustomersForMarketer + marketerEmail;
            var customers = await _genericRepository.GetAsync<List<MarketerCustomer>>(uri);
            return customers;
        }

        public async Task<double> GetCustomerBalance(string email)
        {
            string uri = Constants.BaseApiUrl + Constants.CustomerBalance + email;
            var balance = await _genericRepository.GetAsync<double>(uri);
            return balance;
        }

        public async Task<List<CustomerPricing>> GetCustomersPricing(string email)
        {
            string uri = Constants.BaseApiUrl + Constants.CustomerPricing + email;
            var customersPricing = await _genericRepository.GetAsync<List<CustomerPricing>>(uri);
            return customersPricing;
        }
        
        public async Task<Customer> RegisterNewCustomerByMarketer(string customerEmail, string customerPhone, string birthDay, string birthMonth, int categoryId, string marketerEmail)
        {
            string uri = Constants.BaseApiUrl + Constants.CreateCustomerByMarketer + customerEmail + "/" + customerPhone + "/" + birthDay + "/" + birthMonth + "/" + categoryId + "/" + marketerEmail;
            var result = await _genericRepository.GetAsync<Customer>(uri);
            return result;
        }

        public async Task<List<Category>> GetCustomerCategories()
        {
            string uri = Constants.BaseApiUrl + Constants.GetCustomerCategories;
            var result = await _genericRepository.GetAsync<List<Category>>(uri);
            return result;
        }

        public async Task<List<Sale>> MyRecentTransactions(string email)
        {
            string uri = Constants.BaseApiUrl + Constants.CustomerRecentTransaction + email;
            var sales = await _genericRepository.GetAsync<List<Sale>>(uri.ToString());
            return sales;
        }

        public async Task<List<Recharge>> MyRecentRecharges(string email)
        {
            string uri = Constants.BaseApiUrl + Constants.CustomerRecentRecharges + email;
            var recharges = await _genericRepository.GetAsync<List<Recharge>>(uri.ToString());
            return recharges;
        }

        public async Task<CustomerDashboard> MyDashboard(string email)
        {
            string uri = Constants.BaseApiUrl + Constants.CustomersDashboard + email;
            var cusDash = await _genericRepository.GetAsync<CustomerDashboard>(uri.ToString());
            return cusDash;
        }
    }
}
