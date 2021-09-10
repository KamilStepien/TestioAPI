using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TestioAPI.Context;
using TestioAPI.Entities;
using TestioAPI.Extensions.Results;
using TestioAPI.Modles.Auth;
using BC = BCrypt.Net.BCrypt;


namespace TestioAPI.Services
{

    public interface IAccountService
    {
        Result Register(UserRegisterModel model);
        DataResult<string> Login(UserLoginModel model);
    }


    public class AccountService : IAccountService
    {
        private readonly TestioDBContext _context;

        public AccountService(TestioDBContext context)
        {
            _context = context;
        }

        public DataResult<string> Login(UserLoginModel model)
        {
            if (model == null)
            {
                return DataResult<string>.Error("Model is null");
            }

            try
            {
                var userdb = _context.Users.SingleOrDefault(x => x.Login == model.Login);

                if (userdb == null)
                {
                    return DataResult<string>.Error($"User don't existing in database login({model.Login})").Log();
                }

                if (BC.Verify(model.Password, userdb.Password))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superkey1qaz@WSX"));
                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokenOptions = new JwtSecurityToken(
                        issuer: "https://localhost:5001",
                        audience: "https://localhost:5001",
                        claims: new List<Claim>()
                        {
                             new Claim(ClaimTypes.Name, userdb.Id.ToString()),
                        },
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signingCredentials
                        );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return DataResult<string>.Succes(tokenString);
                }

                return DataResult<string>.Error("Password is incorrect");

            }
            catch(Exception e)
            {
                return DataResult<string>.Error(e.Message).Log();
            }
          
        }

        public Result Register(UserRegisterModel model)
        {
            if (model == null)
            {
                return DataResult<string>.Error("Model is null").Log();
            }

            try
            {
                var userdb = _context.Users.FirstOrDefault(x => x.Login == model.Login);
                if (userdb != null)
                {
                    return Result.Error($"Login is existing in database login({model.Login})").Log();
                }

                var user = new User
                {
                    Login = model.Login,
                    Password = BC.HashPassword(model.Password),
                    Email = model.Email
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return Result.Succes();
            }
            catch (Exception e)
            {
                return Result.Error(e.Message);
            }
        }
    }
}
