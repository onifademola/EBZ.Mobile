
using EBZ.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EBZ.Mobile.ServicesInterface
{
    public interface ICustomerDataService
    {
        Task<List<MarketerCustomer>> GetCustomersForMarketer(string marketerEmail);
        Task<double> GetCustomerBalance(string email);
        Task<List<CustomerPricing>> GetCustomersPricing(string email);
        Task<Customer> RegisterNewCustomerByMarketer(string customerEmail, string customerPhone, string birthDay, string birthMonth, int categoryId, string marketerEmail);
        Task<List<Category>> GetCustomerCategories();
        Task<List<Sale>> MyRecentTransactions(string email);
        Task<List<Recharge>> MyRecentRecharges(string email);
        Task<CustomerDashboard> MyDashboard(string email);
    }
}
