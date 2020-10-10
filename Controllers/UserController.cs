using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            var userDto = await _mediator.Send(userId);
            if (userDto == null) return NotFound();

            return Ok(userDto);
        }
    }
}