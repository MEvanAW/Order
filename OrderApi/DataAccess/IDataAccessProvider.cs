using OrderApi.Models;
using System.Collections.Generic;

namespace OrderApi.DataAccess
{
    public interface IDataAccessProvider
    {
        bool AddOrderRecord(Order order, IEnumerable<Item> items);
        void UpdateOrderRecord(Order order);
        void DeleteOrderRecord(uint id);
        Order? GetOrderSingleRecord(uint id);
        List<Order> GetAllOrders();
    }
}
