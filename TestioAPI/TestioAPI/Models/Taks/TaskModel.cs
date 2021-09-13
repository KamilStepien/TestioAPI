
namespace TestioAPI.Models.Taks
{
    public class TaskModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStatusEnum Status { get; set; }
    }
}
