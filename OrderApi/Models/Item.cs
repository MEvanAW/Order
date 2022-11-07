namespace OrderApi.Models
{
    public class Item
    {
        /// <summary>
        /// ID of the item
        /// </summary>
        /// <example>1</example>
        public uint id { get; set; }
        /// <summary>
        /// Code of the item
        /// </summary>
        /// <example>SGA32</example>
        public string item_code { get; set; }
        /// <summary>
        /// Description of the item
        /// </summary>
        /// <example>Samsung Galaxy A32</example>
        public string? description { get; set; }
        /// <summary>
        /// Quantity of the item
        /// </summary>
        /// <example>1</example>
        public uint quantity { get; set; }
        /// <summary>
        /// ID of the order the item is part of
        /// </summary>
        /// <example>1</example>
        public uint order_id { get; set; }

        // constructors
        public Item(string item_code, uint quantity)
        {
            this.item_code = item_code;
            this.quantity = quantity;
        }
        public Item()
        {

        }
    }
}
