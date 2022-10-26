using OrderApi.Models;
using System.Collections.Generic;

namespace OrderApi.DataAccess
{
    public interface IDataAccessProvider
    {
        void AddOrderRecord(Order order);
        void UpdateOrderRecord(Order order);
        void DeleteOrderRecord(uint id);
        Order? GetOrderSingleRecord(uint id);
        List<Order> GetAllOrders();
    }
}
