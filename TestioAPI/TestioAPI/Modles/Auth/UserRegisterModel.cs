using System.ComponentModel.DataAnnotations;

namespace TestioAPI.Modles.Auth
{
    public class UserRegisterModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
