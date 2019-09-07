namespace EBZ.Mobile.Models
{
    public class CustomerPricing
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int PricingId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUom { get; set; }
        public double? Cost { get; set; }
        public string Description { get; set; }
        public double? CustomerBalance { get; set; }
        public string CostView { get; set; }
        public string CustomerBalanceView { get; set; }
    }
}
