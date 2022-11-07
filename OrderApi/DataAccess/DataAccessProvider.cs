using OrderApi.Models;

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
                try { item.OrderId = (uint) orderId; }
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
            var list = _context.orders.ToList();
            foreach (var order in list)
            {
                order.Items = _context.items.Where(i => i.OrderId == order.id).ToList();
            }
            return list;
        }

        public Order? GetOrderSingleRecord(uint id)
        {
            var order = _context.orders.FirstOrDefault(o => o.id == id);
            if (order != null)
                order.Items = _context.items.Where(i => i.OrderId == order.id).ToList();
            return order;
        }

        public void UpdateOrderRecord(Order order)
        {
            _context.orders.Update(order);
            _context.SaveChanges();
        }
    }
}
