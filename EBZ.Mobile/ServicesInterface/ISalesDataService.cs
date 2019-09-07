using EBZ.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EBZ.Mobile.ServicesInterface
{
    public interface ISalesDataService
    {
        Task<String> ValidateSalesPin(string pin);
        Task<string> ValidateCustomersPin(string email, string pin);
        Task<List<CustomerPricing>> GetCustomersPricing(string email);
        Task<string> PayForProduct(string salesUserPin, string user, int pricingId, double pricingCost, string pricingUnit, double pricingVolume);
        Task<List<Sale>> MySales(string email);
    }
}
