using System.ComponentModel.DataAnnotations.Schema;

namespace OrderApi.Models
{
    public class Order
    {
        /// <summary>
        /// ID of the order
        /// </summary>
        /// <example>1</example>
        [Column("id")]
        public uint ID { get; set; }
        /// <summary>
        /// Name of the customer placing the order
        /// </summary>
        /// <example>Fulan</example>
        public string? customer_name { get; set; }
        /// <summary>
        /// DateTime the order was placed
        /// </summary>
        public DateTime ordered_at { get; set; }
        /// <summary>
        /// Items associated with the order
        /// </summary>
        public IEnumerable<Item>? Items { get; set; }

        // Constructors
        public Order(string? customer_name, DateTime ordered_at)
        {
            this.customer_name = customer_name;
            this.ordered_at = ordered_at;
        }
        public Order()
        {

        }
    }
}
