using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using TestioAPI.Context;
using TestioAPI.Entities;
using TestioAPI.Extensions.Logger;
using TestioAPI.Models.Taks;
using TestioAPI.Services;

namespace TestioAPI.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : ControllerTestioBase
    {
        private readonly ITaskService _task;
        public TaskController(ITaskService task)
        {
            _task = task;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add([FromBody] TaskAddModel model)
        {

            if(CheckIsValidModel())
            {
                return ModelNotValidRespons();
            }

            var result = _task.AddTask(UserId, model);

            if (result.IsNotSucces)
            {
                return BadRequest(result.ToString());
            }

            return Ok(result.Data);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetTasks([FromQuery(Name = "taskStatus")] int? taskStatus)
        {

            var result = _task.GetTasks(UserId, taskStatus);

            if (result.IsNotSucces)
            {
                return BadRequest(result.ToString());
            }

            return Ok(result.Data);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Edit(TaskEditModel model)
        {
            if (CheckIsValidModel())
            {
                return ModelNotValidRespons();
            }

            var result = _task.EditTask(UserId, model);
            if(result.IsNotSucces)
            {
                return BadRequest(result);
            }

            return Ok(result.Data);
        }

        [HttpDelete,Route("{taskId}")]
        [Authorize]
        public IActionResult Delete(int taskId)
        {
            var result = _task.DeleteTask(UserId, taskId);

            if(result.IsNotSucces)
            {
                return BadRequest(result.ToString());
            }

            return Ok(result.Data);
        }


        [HttpPut, Route("editStatus")]
        [Authorize]
        public IActionResult EditStatus([FromBody] TaskEditStatusModel model)
        {
            if (CheckIsValidModel())
            {
                return ModelNotValidRespons();
            }

            var result = _task.EditStatus(UserId, model);

            if (result.IsNotSucces)
            {
                return BadRequest(result.ToString());
            }

            return Ok(result.Data);
        }

       

    }
}


