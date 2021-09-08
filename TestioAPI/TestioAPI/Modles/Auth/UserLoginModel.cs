using System.ComponentModel.DataAnnotations;

namespace TestioAPI.Modles.Auth
{
    public class UserLoginModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

