using Microsoft.AspNetCore.Mvc;
using OrderApi.DataAccess;
using OrderApi.Models;

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

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dataAccessProvider.GetAllOrders());
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            uint parsedId = 0;
            try
            {
                parsedId = Convert.ToUInt32(id);
            }
            catch
            {
                return BadRequest();
            }
            var order =  _dataAccessProvider.GetOrderSingleRecord(parsedId);
            if (order == null)
            {
                return NotFound("Order with ID " + parsedId + " is not found.");
            }
            return Ok(order);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Order order)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddOrderRecord(order);
                return Ok(order);
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult Edit([FromBody]Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dataAccessProvider.UpdateOrderRecord(order);
                }
                catch
                {
                    return NotFound("Order with ID " + order.Id + " is not found.");
                }
                return Ok(order);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            uint parsedId = 0;
            try
            {
                parsedId = Convert.ToUInt32(id);
            }
            catch
            {
                return BadRequest();
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
            return Ok();
        }
    }
}
