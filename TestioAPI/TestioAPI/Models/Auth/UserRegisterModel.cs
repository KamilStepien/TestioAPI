using System.ComponentModel.DataAnnotations;

namespace TestioAPI.Models.Auth
{
    public class UserRegisterModel
    {
        [Required]
        [MaxLength(20)]
        public string Login { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
        [MaxLength(30)]
        public string Email { get; set; }
    }
}
