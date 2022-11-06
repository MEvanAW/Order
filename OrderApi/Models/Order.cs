using System.ComponentModel.DataAnnotations;

namespace OrderApi.Models
{
    public class Order
    {
        /// <summary>
        /// ID of the order
        /// </summary>
        /// <example>1</example>
        public uint id { get; set; }
        /// <summary>
        /// Name of the customer placing the order
        /// </summary>
        /// <example>Fulan</example>
        public string? customer_name { get; set; }
        /// <summary>
        /// DateTime the order was placed
        /// </summary>
        public DateTime ordered_at { get; set; }
    }
}
