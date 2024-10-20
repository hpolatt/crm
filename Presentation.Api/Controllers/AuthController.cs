using Core.Application.Features.Auth.Commands.Login;
using Core.Application.Features.Auth.Commands.Logout;
using Core.Application.Features.Auth.Commands.Register;
using Core.Application.Features.Auth.Queries.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommandRequest request)
        {
            var response = await mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommandRequest request)
        {
            var response = await mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers(){
            var response = await mediator.Send(new GetAllUsersRequest());
            return StatusCode(StatusCodes.Status200OK, response);   
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutCommandRequest request) {
            var response = await mediator.Send(request);
            return StatusCode(StatusCodes.Status201Created, response);
        }
    }
}
