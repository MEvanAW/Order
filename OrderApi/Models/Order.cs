namespace OrderApi.Models
{
    public class Order
    {
        public uint Id { get; set; }
        public string? CustomerName { get; set; }
        public DateTime OrderedAt { get; set; }
    }
}
