using Microsoft.AspNetCore.Mvc;
using OrderApi.DataAccess;
using OrderApi.Dto;
using OrderApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderApi.Controllers
{
    [Route("orders")]
    public class OrderController : Controller
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public OrderController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <response code="200">All orders retrieved</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Order>), 200)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Get all orders",
            Description = "Get all orders.",
            OperationId = "GetAllOrders"
        )]
        public IActionResult Get()
        {
            return Ok(_dataAccessProvider.GetAllOrders());
        }

        /// <summary>
        /// Get an order
        /// </summary>
        /// <remarks>Retrieves a specific order by unique id.</remarks>
        /// <param name="id" example="1">The order id</param>
        /// <response code="200">Order retrieved</response>
        /// <response code="400">Id format is not recognized</response>
        /// <response code="404">Order not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Get an order",
            Description = "Retrieves a specific order by unique id.",
            OperationId = "GetOrder"
        )]
        public IActionResult Get(string id)
        {
            uint parsedId = 0;
            try
            {
                parsedId = Convert.ToUInt32(id);
            }
            catch
            {
                return BadRequest("Id format is not recognized.");
            }
            var order =  _dataAccessProvider.GetOrderSingleRecord(parsedId);
            if (order == null)
            {
                return NotFound("Order with ID " + parsedId + " is not found.");
            }
            return Ok(order);
        }

        /// <summary>
        /// Create an order
        /// </summary>
        /// <remarks>Create an order including its item(s). Item(s) are mandatory.</remarks>
        /// <param name="orderDto">Order to be made.</param>
        /// <response code="200">Order created.</response>
        /// <response code="400">Body format is not recognized.</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Create an order",
            Description = "Create an order including its item(s). Item(s) are mandatory.",
            OperationId = "CreateOrder"
        )]
        public IActionResult Create([FromBody]OrderDto orderDto)
        {
            if (ModelState.IsValid)
            {
                if (orderDto.Items == null)
                {
                    return BadRequest("Item is not detected on the body.");
                }
                Order order = new Order(orderDto.CustomerName, orderDto.OrderedAt);
                var items = ToItemList(orderDto.Items);
                bool ok = _dataAccessProvider.AddOrderRecord(order, items);
                if (ok)
                {
                    return Ok(order);
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest("Body format is not recognized.");
        }

        /// <summary>
        /// Update an order
        /// </summary>
        /// <remarks>Update order by ID including its items. Previous items are discarded.</remarks>
        /// <param name="id" example="1">The order id</param>
        /// <param name="orderDto">Order to be updated.</param>
        /// <response code="200">Order updated.</response>
        /// <response code="400">Body format is not recognized.</response>
        /// <response code="404">Order is not found.</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Update an order",
            Description = "Update order by ID including its items. Previous items are discarded.",
            OperationId = "UpdateOrder"
        )]
        public IActionResult Edit(string id, [FromBody]OrderDto orderDto)
        {
            if (ModelState.IsValid)
            {
                if (orderDto.Items == null)
                {
                    return BadRequest("Item is not detected on the body.");
                }
                uint parsedId;
                try
                {
                    parsedId = Convert.ToUInt32(id);
                }
                catch
                {
                    return BadRequest("Id format is not recognized.");
                }
                var order = _dataAccessProvider.GetOrderSingleRecord(parsedId);
                if (order == null)
                {
                    return NotFound("Order with ID " + parsedId + " is not found.");
                }
                order.ordered_at = orderDto.OrderedAt;
                order.customer_name = orderDto.CustomerName;
                var items = ToItemList(orderDto.Items);
                _dataAccessProvider.UpdateOrderRecord(order, items);
                return Ok(order);
            }
            return BadRequest("Body format is not recognized.");
        }

        /// <summary>
        /// Delete an order
        /// </summary>
        /// <remarks>Delete order by ID including its items.</remarks>
        /// <param name="id">ID number of the order to be deleted.</param>
        /// <response code="200">Order deleted.</response>
        /// <response code="400">ID format is not recognized.</response>
        /// <response code="404">Order is not found.</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Delete an order",
            Description = "Delete order by ID including its items.",
            OperationId = "DeleteOrder"
        )]
        public IActionResult Delete(string id)
        {
            uint parsedId = 0;
            try
            {
                parsedId = Convert.ToUInt32(id);
            }
            catch
            {
                return BadRequest("ID format is not recognized.");
            }
            try
            {
                _dataAccessProvider.DeleteOrderRecord(parsedId);
            }
            catch(ArgumentNullException)
            {
                return NotFound("Order with ID " + parsedId + " is not found.");
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("Order with ID " + parsedId + " has been successfully deleted.");
        }

        private List<Item> ToItemList(IEnumerable<ItemDto> items)
        {
            var returnList = new List<Item>();
            foreach (var item in items)
            {
                var newItem = new Item(item.ItemCode, item.Quantity);
                if (item.Description != null)
                    newItem.description = item.Description;
                returnList.Add(newItem);
            }
            return returnList;
        }
    }
}
