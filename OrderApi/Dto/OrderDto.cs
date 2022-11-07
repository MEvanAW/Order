namespace OrderApi.Dto
{
    public class OrderDto
    {
        /// <summary>
        /// Name of the customer placing the order
        /// </summary>
        /// <example>Fulan</example>
        public string? CustomerName { get; set; }
        /// <summary>
        /// DateTime the order was placed
        /// </summary>
        public DateTime OrderedAt { get; set; }
        /// <summary>
        /// Items included in the order
        /// </summary>
        public IEnumerable<ItemDto>? Items { get; set; }
    }
}
