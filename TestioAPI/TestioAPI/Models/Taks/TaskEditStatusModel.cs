using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestioAPI.Models.Taks
{
    public class TaskEditStatusModel
    {
        public int Id { get; set; }
        public TaskStatusEnum NewStatus { get; set; }
    }
}
