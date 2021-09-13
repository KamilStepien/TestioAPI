using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestioAPI.Extensions.Logger;

namespace TestioAPI.Controllers
{
    public class ControllerTestioBase:ControllerBase
    {
        private string _modelIsNotValidMessage = "Model is not valid";
        public int UserId => Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);

        public bool CheckIsValidModel()
        {

            if (!ModelState.IsValid)
            {
                TLogger.Log().Msc(_modelIsNotValidMessage).Error();
                return false;
            }

            return true;
        }

        public IActionResult ModelNotValidRespons() => BadRequest(_modelIsNotValidMessage);

    }
}

