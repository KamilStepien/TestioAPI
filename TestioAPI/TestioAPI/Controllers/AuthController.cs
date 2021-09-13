using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestioAPI.Context;
using TestioAPI.Extensions.Logger;
using TestioAPI.Models.Auth;
using TestioAPI.Services;

namespace TestioAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerTestioBase
    {
        private readonly IAccountService _accountService;
        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] UserLoginModel model)
        {

            if (CheckIsValidModel())
            {
                return ModelNotValidRespons();
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
            if (CheckIsValidModel())
            {
                return ModelNotValidRespons();
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
