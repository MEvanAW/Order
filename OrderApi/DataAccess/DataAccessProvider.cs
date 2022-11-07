using OrderApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace OrderApi.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly PostgreSqlContext _context;
        
        public DataAccessProvider(PostgreSqlContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add an order to DB
        /// </summary>
        /// <param name="order">The order</param>
        /// <param name="items">The items associated with the order</param>
        /// <returns>True if no error occurs. False if an error occurs.</returns>
        public bool AddOrderRecord(Order order, IEnumerable<Item> items)
        {
            var entity = _context.orders.Add(order);
            var orderId = entity.CurrentValues["id"];
            foreach (Item item in items)
            {
                try { item.order_id = (uint) orderId; }
                catch { return false; }
            }
            _context.items.AddRange(items);
            _context.SaveChanges();
            return true;
        }

        public void DeleteOrderRecord(uint id)
        {
            var entity = GetOrderSingleRecord(id);
            if (entity == null)
            {
                throw new ArgumentNullException(null, "Order with ID " + id + " is not found.");
            }
            _context.orders.Remove(entity);
            _context.SaveChanges();
        }

        public List<Order> GetAllOrders()
        {
            return _context.orders.ToList();
        }

        public Order? GetOrderSingleRecord(uint id)
        {
            return _context.orders.FirstOrDefault(o => o.id == id);
        }

        public void UpdateOrderRecord(Order order)
        {
            _context.orders.Update(order);
            _context.SaveChanges();
        }
    }
}
