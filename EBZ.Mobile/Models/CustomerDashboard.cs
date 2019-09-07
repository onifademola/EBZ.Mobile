using System;
using System.Collections.Generic;

namespace EBZ.Mobile.Models
{
    public class CustomerDashboard
    {
        public double? Balance { get; set; }
        public string Category { get; set; }
        public double? CategoryPrice { get; set; }
        public int Last6MonthsTransactionCount { get; set; }
        public double? LastTransactionAmount { get; set; }
        public DateTime? LastTransactionDate { get; set; }
        public int Last6MonthsRechargeCount { get; set; }
        public double? LastRechargeAmount { get; set; }
        public DateTime? LastRechargeDate { get; set; }
        public List<CategoryPricingList> CategoryList { get; set; }

    }

    public class CategoryPricingList
    {
        public string Name { get; set; }
        public double? Price { get; set; }

    }
}
