using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestioAPI.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column("u_login", TypeName = "varchar(20)")]
        public string Login { get; set; }
        [Required]
        [Column("u_password", TypeName = "varchar(20)")]
        public string Password { get; set; }
        [Column("u_email", TypeName = "varchar(30)")]
        public string Email { get; set; }

        public ICollection<Task> Tasks { get; set; }

    }
}
