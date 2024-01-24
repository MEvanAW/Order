using Microsoft.AspNetCore.Mvc;
using OrderApi.DataAccess;
using OrderApi.Dto;
using OrderApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderApi.Controllers
{
    [Route("users")]
    [SwaggerTag("Register, login, update, and delete Users")]
    public class UserController : Controller
    {
        private readonly IDataAccessProvider _dataAccessProvider;
        private readonly string BAD_BODY = "Missing attribute or body format is not recognized.";

        public UserController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <remarks>Register a new user.</remarks>
        /// <param name="userDto">Order to be made.</param>
        /// <response code="201 ">User created.</response>
        /// <response code="400">Body format is not recognized.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Register a new user",
            Description = "Register a new user.",
            OperationId = "RegisterUser"
        )]
        public IActionResult Register([FromBody, SwaggerRequestBody("The order payload", Required = true)] UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                return Created("", "Dummy OK Created");
            }
            return BadRequest(BAD_BODY);
        }
    }
}
