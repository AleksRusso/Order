namespace Order.Models
{
    public class OrderModel
    {
        public List<Orders> Orders { get; set; }
        public int[] Number { get; set; }
        public List<string> ProviderFilters { get; set; }
        public List<int> NumberFilters { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string[] Providers { get; set; }
    }
}
