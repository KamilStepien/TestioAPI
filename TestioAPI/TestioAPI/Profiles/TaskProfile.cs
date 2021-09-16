using AutoMapper;
using TestioAPI.Entities;
using TestioAPI.Models.Taks;

namespace TestioAPI.Profiles
{
    public class TaskProfile: Profile
    {
        public TaskProfile()
        {
            CreateMap<Tasks, TaskModel>();
                
        }
    }
}
