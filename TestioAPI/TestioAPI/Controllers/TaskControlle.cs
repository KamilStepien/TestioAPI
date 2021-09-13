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
    public class TaskController : ControllerBase
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

            if (!ModelState.IsValid)
            {
                TLogger.Log().Msc("Model is not valid").Error();
                return BadRequest("Model is not valid");
            }

            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);
            var result = _task.AddTask(userId, model);

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
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);

            var result = _task.GetTasks(userId, taskStatus);

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
            if (!ModelState.IsValid)
            {
                TLogger.Log().Msc("Model is not valid").Error();
                return BadRequest("Model is not valid");
            }

            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);

            var result = _task.EditTask(userId, model);
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
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);
            var result = _task.DeleteTask(userId, taskId);

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
            if (!ModelState.IsValid)
            {
                TLogger.Log().Msc("Model is not valid").Error();
                return BadRequest("Model is not valid");
            }

            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);
            var result = _task.EditStatus(userId, model);

            if (result.IsNotSucces)
            {
                return BadRequest(result.ToString());
            }

            return Ok(result.Data);
        }

       

    }
}


