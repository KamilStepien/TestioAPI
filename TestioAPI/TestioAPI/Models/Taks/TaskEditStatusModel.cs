using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestioAPI.Models.Taks
{
    public class TaskEditStatusModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public TaskStatusEnum NewStatus { get; set; }
    }
}
