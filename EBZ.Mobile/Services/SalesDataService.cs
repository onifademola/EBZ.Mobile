using EBZ.Mobile.Models;
using EBZ.Mobile.ServicesInterface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EBZ.Mobile.Services
{
    public class SalesDataService : ISalesDataService
    {
        GenericService _genericRepository = new GenericService();
        public SalesDataService()
        {
        }

        public async Task<String> ValidateSalesPin(string pin)
        {
            string uri = Constants.BaseApiUrl + Constants.ValidateSalesPersonPIN + pin;
            var result = await _genericRepository.GetAsync<String>(uri.ToString());
            return result;
        }

        public async Task<string> ValidateCustomersPin(string email, string pin)
        {
            string uri = Constants.BaseApiUrl + Constants.ValidateCustomerPIN + email + "/" + pin;
            var result = await _genericRepository.GetAsync<String>(uri.ToString());
            return result;
        }

        public async Task<List<CustomerPricing>> GetCustomersPricing(string email)
        {
            string uri = Constants.BaseApiUrl + Constants.CustomerPricing  + email;
            var customersPricing = await _genericRepository.GetAsync<List<CustomerPricing>>(uri.ToString());
            return customersPricing;
        }

        public async Task<string> PayForProduct(string salesUserPin, string user, int pricingId, double pricingCost, string pricingUnit, double pricingVolume)
        {
            string uri = Constants.BaseApiUrl + Constants.Payment + salesUserPin + "/" + user + "/" + pricingId
                + "/" + pricingCost + "/" + pricingUnit + "/" + pricingVolume;
            var payResult = await _genericRepository.GetAsync<String>(uri.ToString());
            return payResult;
        }

        public async Task<List<Sale>> MySales(string email)
        {
            string uri = Constants.BaseApiUrl + Constants.MySales + email;
            var sales = await _genericRepository.GetAsync<List<Sale>>(uri.ToString());
            return sales;
        }
    }
}
