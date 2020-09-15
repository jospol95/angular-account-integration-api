using System;
using System.Threading.Tasks;
using AuthorizationAPI.Application;
using AuthorizationAPI.Application.Commands;
using AuthorizationAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class AuthController : BaseController
    {

        public AuthController(IMediator mediator) : base(mediator)
        {
            
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand newUser)
        {
            try
            {
                var addedUser = await _mediator.Send(newUser);
                return Ok(addedUser);
            }
            catch (EmailAlreadyRegisteredException emailAlreadyRegisteredException)
            {
                return Conflict(emailAlreadyRegisteredException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand user)
        {
            try
            {
                var existingUser = await _mediator.Send(user);
                return Ok("token");
            }
            catch (EmailAndOrPasswordIncorrectException emailAndOrPasswordIncorrectException)
            {
                return StatusCode(403);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        
        // public async Task<IActionResult> LoginOrRegisterWithGoogle(string googleUserToken)
        // {
        //     return Ok();
        // }
        //
        // public async Task<IActionResult> LoginOrRegisterWithFacebook(string facebookUserToken)
        // {
        //     return Ok();
        // }

        // private string GenerateTokenForLoggedUser()
        // {
        //     return Ok("yes");
        // }

    }
}