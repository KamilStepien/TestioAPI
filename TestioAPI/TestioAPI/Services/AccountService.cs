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
using TestioAPI.Models.Auth;
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
                return DataResult<string>.Error("Nieprzekazano danych do logowania");
            }

            try
            {
                var userdb = _context.Users.SingleOrDefault(x => x.Login == model.Login);

                if (userdb == null)
                {
                    return DataResult<string>.Warning($"Nie ma użytkownika o loginie: ({model.Login})").Log();
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

                return DataResult<string>.Warning("Hasło jest nieprawidłowe");

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
                return Result.Error("Nieprzekazano danych do rejestracji").Log();
            }

            try
            {
                var userdb = _context.Users.FirstOrDefault(x => x.Login == model.Login);
                if (userdb != null)
                {
                    return Result.Warning($"Użytkownik o podanym loginie istniej już w bazie danych ({model.Login})").Log();
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
