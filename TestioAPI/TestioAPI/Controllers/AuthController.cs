using Microsoft.AspNetCore.Mvc;
using TestioAPI.Context;
using TestioAPI.Modles.Auth;
using TestioAPI.Services;

namespace TestioAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AuthController(TestioDBContext context, IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] UserLoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Model is not valid");
            }

            var result = _accountService.Login(model);

            if(result.IsNotSucces)
            {
                return Unauthorized(result.ToString());
            }

            return Ok(new { Token = result.Data });
        }

        [HttpPost, Route("register")]
        public IActionResult Registger([FromBody] UserRegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Model is not valid ");
            }

            var result = _accountService.Register(model);

            if(result.IsNotSucces)
            {
                return BadRequest(result.ToString());
            }

            return Ok();
        }
    }
}
