using System;

namespace EBZ.Mobile.Models
{
    public class Sale
    {
        public int PricingId { get; set; }
        public string PricingDetail { get; set; }
        public double PricingCost { get; set; }
        public string VolumeDetail { get; set; }
        public double Total { get; set; }
        public string TotalDetail { get; set; }
        public DateTime Time { get; set; }
        public DateTime Date { get; set; }
        public string DateDetail { get; set; }
        public string Customer { get; set; }
    }
}