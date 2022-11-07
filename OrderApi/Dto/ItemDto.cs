namespace OrderApi.Dto
{
    public class ItemDto
    {
        /// <summary>
        /// Code of the item
        /// </summary>
        /// <example>SGA32</example>
        public string ItemCode { get; set; }
        /// <summary>
        /// Description of the item
        /// </summary>
        /// <example>Samsung Galaxy A32</example>
        public string? Description { get; set; }
        /// <summary>
        /// Quantity of the item
        /// </summary>
        /// <example>1</example>
        public uint Quantity { get; set; }
    }
}
