using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TestioAPI.Context;
using TestioAPI.Entities;
using TestioAPI.Modles.Auth;
using BC = BCrypt.Net.BCrypt;

namespace TestioAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TestioDBContext _context;
        public AuthController(TestioDBContext context)
        {
            _context = context;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] UserLoginModel model)
        {
            if (model == null)
            {
                return BadRequest("Model is null");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest("Model is not valid");
            }

            var userdb = _context.Users.SingleOrDefault(x => x.Login == model.Login);

            if(userdb == null)
            {
                return BadRequest("User don't existing in database ");
            }

            if (BC.Verify(model.Password, userdb.Password)) 
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superkey1qaz@WSX"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44377",
                    audience: "https://localhost:44377",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCredentials
                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });

            }

            return Unauthorized();
        }

        [HttpPost, Route("register")]
        public IActionResult Registger([FromBody] UserRegisterModel model)
        {
            if(model == null)
            {
                return BadRequest("Model is null");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest("Model is not valid ");
            }

            try
            {
                var userdb  = _context.Users.FirstOrDefault(x => x.Login == model.Login);
                if(userdb != null)
                {
                    return BadRequest("Login is existing in database");
                }

                var user = new User
                {
                    Login = model.Login,
                    Password = BC.HashPassword(model.Password),
                    Email = model.Email
                };
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}
