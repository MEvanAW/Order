using OrderApi.Dto;
using OrderApi.Models;

namespace OrderApi.DataAccess
{
    public interface IDataAccessProvider
    {
        bool AddOrderRecord(Order order, IEnumerable<Item> items);
        void UpdateOrderRecord(Order order, List<Item> items);
        void DeleteOrderRecord(uint id);
        Order? GetOrderSingleRecord(uint id);
        List<Order> GetAllOrders();
        bool CreateUser(UserDto user);
    }
}
