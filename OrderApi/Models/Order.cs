namespace OrderApi.Models
{
    public class Order
    {
        public uint id { get; set; }
        public string? customer_name { get; set; }
        public DateTime ordered_at { get; set; }
    }
}
