using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TestioAPI.Entities;
using TestioAPI.Models.Taks;
using Xunit;

namespace TestioAPITest.AutomapperTest
{
    public class TaskProfileTest : AutoMapperTest
    {

        [Fact]
        [Category("AutoMapper")]
        public void Map_Task_to_TaskModel()
        {

            var current = new Task
            {
                Id = 1,
                Description = "Description",
                Name = "Name",
                Status = TaskStatusEnum.Create,
                UserId = 1
            };


            var expected = _mapper.Map<TaskModel>(current);


            Assert.Equal(current.Id, expected.Id);
            Assert.Equal(current.Description, expected.Description);
            Assert.Equal(current.Name, expected.Name);
            Assert.Equal(current.Status, expected.Status);
            Assert.Equal(current.UserId, expected.UserId);

        }
    }
}
