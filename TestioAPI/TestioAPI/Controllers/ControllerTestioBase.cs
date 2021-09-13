using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestioAPI.Controllers
{
    public class ControllerTestioBase:ControllerBase
    {
        public int UserId => Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);
    }
}
