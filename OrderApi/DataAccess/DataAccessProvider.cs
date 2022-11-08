using Microsoft.AspNetCore.Components.Web.Virtualization;
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
                try
                {
                    item.OrderID = (uint) orderId;
                }
                catch
                {
                    return false;
                }
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
                order.Items = GetItemsOfOrderId(order.ID);
            }
            return list;
        }

        public Order? GetOrderSingleRecord(uint id)
        {
            var order = _context.orders.FirstOrDefault(o => o.ID == id);
            if (order != null)
            {
                order.Items = GetItemsOfOrderId(order.ID);
            }
            return order;
        }

        public void UpdateOrderRecord(Order order, List<Item> items)
        {
            int len = order.Items!.Count();
            bool isLonger = false;
            if (len > items.Count)
            {
                len = items.Count;
                isLonger = true;
            }
            for (int i = 0; i < len; i++)
            {
                order.Items!.ElementAt(i).item_code = items[i].item_code;
                order.Items!.ElementAt(i).description = items[i].description;
                order.Items!.ElementAt(i).quantity = items[i].quantity;
            }
            if (!isLonger)
            {
                order.Items = order.Items!.Concat(items.GetRange(len, items.Count-len)).ToList();
            }
            _context.orders.Update(order);
            if (isLonger)
            {
                List<Item> itemList;
                if (order.Items is List<Item>)
                {
                    itemList = (List<Item>) order.Items;
                }
                else
                {
                    itemList = order.Items!.ToList();
                    // TODO: check if itemList needs to be assigned to order.Items
                }
                List<Item> itemRange = itemList.GetRange(len, itemList.Count - len);
                _context.items.RemoveRange(itemRange);
                itemRange.Clear();
            }
            _context.SaveChanges();
        }

        private List<Item> GetItemsOfOrderId(uint id)
        {
            return _context.items.Where(i => i.OrderID == id).ToList();
        }
    }
}
