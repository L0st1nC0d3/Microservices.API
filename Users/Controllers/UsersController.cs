using Business;
using Business.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly UsersManager _manager;

        public UsersController(IPublishEndpoint publishEndpoint, UsersManager manager)
        {
            _publishEndpoint = publishEndpoint;
            _manager = manager;
        }

        [HttpPost("users")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<ActionResult<int>> CreateUser([FromBody] string userName)
        {
            var userId = _manager.AddNewUser(userName);
            await _publishEndpoint.Publish<ICreatedUserEvent>(new
            {
                UserId = userId
            });
            return Ok(userId);
        }

        [HttpGet("users/{id:int}")]
        [ProducesResponseType(typeof(int), 200)]
        public ActionResult<int> GetUser([FromRoute] int id)
        {
            return Ok(_manager.GetUserById(id));
        }

        [HttpDelete("users/{id:int}")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<ActionResult<int>> DeleteUser([FromRoute] int id)
        {
            var userId = _manager.DeleteUserById(id);
            await _publishEndpoint.Publish<IDeletedUserEvent>(new
            {
                Id = userId
            });
            return Ok(userId);
        }
    }
}