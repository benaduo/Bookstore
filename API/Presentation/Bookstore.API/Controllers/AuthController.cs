using Bookstore.Application.Logics.AuthLogics;
using Bookstore.Application.Logics.AuthLogics.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    public class AuthController : BaseApiController
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserModel model)
        {
            return Ok(await Mediator.Send(new RegisterUser { Model = model }));
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            return Ok(await Mediator.Send(new Login { Model = model }));
        }
    }
}
