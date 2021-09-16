using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestioAPI.Models.Taks;

namespace TestioAPI.Entities
{
    [Table("Tasks")]
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column("t_user_id")]
        public int UserId { get; set; }
        [Required]
        [Column("t_name", TypeName = "varchar(50)")]
        public string Name { get; set; }
        [Required]
        [Column("t_description", TypeName = "varchar(200)")]
        public string Description { get; set; }
        [Required]
        [Range(1, 3)]
        public TaskStatusEnum Status { get; set; }

        public Users User { get; set; }
    }
}
