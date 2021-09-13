using System.ComponentModel.DataAnnotations;

namespace TestioAPI.Models.Auth
{
    public class UserLoginModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

