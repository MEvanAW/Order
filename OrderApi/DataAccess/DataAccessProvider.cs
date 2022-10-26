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

        public void AddOrderRecord(Order order)
        {
            _context.orders.Add(order);
            _context.SaveChanges();
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
