using System;
using System.Collections.Generic;
using System.Linq;
using TestioAPI.Context;
using TestioAPI.Entities;
using TestioAPI.Extensions.Logger;
using TestioAPI.Extensions.Results;
using TestioAPI.Models.Taks;

namespace TestioAPI.Services
{
    public interface ITaskService
    {
        DataResult<TaskModel> AddTask(int userId, TaskAddModel model);
        DataResult<TaskModel> EditStatus(int userId,TaskEditStatusModel model);
        DataResult<TaskModel> EditTask(int userId,TaskEditModel model);
        DataResult<List<TaskModel>> GetTasks(int userId, int? taskStatus);
        DataResult<bool> DeleteTask(int userId, int taskId);

    }
    public class TaskService : ITaskService
    {
        public readonly TestioDBContext _context;
        public TaskService(TestioDBContext context)
        {
            _context = context;
        }

        public DataResult<TaskModel> AddTask(int userId, TaskAddModel model)
        {
            TLogger.Log().Msc("Dodawanie zadania").Information();
            try
            {
                var taskdb = new Task
                {
                    UserId = userId,
                    Status = TaskStatusEnum.Create,
                    Name = model.Name,
                    Description = model.Description
                };

                _context.Tasks.Add(taskdb);
                _context.SaveChanges();

                return DataResult<TaskModel>.Succes(new TaskModel
                {
                    Id = taskdb.Id,
                    UserId = taskdb.UserId,
                    Name = taskdb.Name,
                    Description = taskdb.Description,
                    Status = taskdb.Status
                });
            }
            catch(Exception ex)
            {
                return DataResult<TaskModel>.Error($"Błąd podczas dodawnia zadania ex:{ex.Message}").Log();
            }
           
        }

        public DataResult<bool> DeleteTask(int userId,int taskId)
        {
            TLogger.Log().Msc($"Usuwanie  zadania o id: {taskId}").Information();
            try
            {
                var taskdb = _context.Tasks.FirstOrDefault(t => t.Id == taskId);

                if (taskdb?.UserId != userId)
                {
                    return DataResult<bool>.Error($"Użytkownik o id: {userId} nie może usunąć zadania o id: {taskId}").Log();
                }

                _context.Tasks.Remove(taskdb);
                _context.SaveChanges();

                return DataResult<bool>.Succes(true);
            }
            catch(Exception ex)
            {
                return DataResult<bool>.Error($"Błąd podaczas usuwania zadania o id: {taskId}, ex: {ex.Message}").Log();
            }
        }

        public DataResult<TaskModel> EditStatus(int userId,TaskEditStatusModel model)
        {
            TLogger.Log().Msc($"Zmiana status zadania o id: {model.Id}").Information();
            try
            {
                var taskdb = _context.Tasks.FirstOrDefault(t => t.Id == model.Id);

                if(taskdb?.UserId != userId)
                {
                    return DataResult<TaskModel>.Error($"Użytkownik o id: {userId} nie może zmienić statusu zadania o id: {model.Id}").Log();
                }

                taskdb.Status = model.NewStatus;
                _context.SaveChanges();

                return DataResult<TaskModel>.Succes(new TaskModel
                {
                    Id = taskdb.Id,
                    UserId = taskdb.UserId,
                    Name = taskdb.Name,
                    Description = taskdb.Description,
                    Status = taskdb.Status
                });

            }
            catch(Exception ex)
            {
                return DataResult<TaskModel>.Error($"Błąd podaczas zmiany status zadania o id: { model.Id}, ex: {ex.Message}").Log();
            }
        }

        public DataResult<TaskModel> EditTask(int userId, TaskEditModel model)
        {
            TLogger.Log().Msc($"Edycja zadania o id: {model.Id}").Information();
            try
            {
                var taskdb = _context.Tasks.FirstOrDefault(t => t.Id == model.Id);
                if (taskdb?.UserId != userId)
                {
                    return DataResult<TaskModel>.Error($"Użytkownik o id: {userId} nie może edytować zadania o id: {model.Id}").Log();
                }

                taskdb.Name = model.Name;
                taskdb.Description = model.Description;

                _context.SaveChanges();

                return DataResult<TaskModel>.Succes(new TaskModel
                {
                    Id = taskdb.Id,
                    UserId = taskdb.UserId,
                    Name = taskdb.Name,
                    Description = taskdb.Description,
                    Status = taskdb.Status
                });

            }
            catch(Exception ex)
            {
                return DataResult<TaskModel>.Error($"Błąd podaczas edycji zadania o id: { model.Id}, ex: {ex.Message}").Log();
            }

        }

        public DataResult<List<TaskModel>> GetTasks(int userId, int? taskStatus)
        {
            TLogger.Log().Msc($"Pobieranie zadań użytkowanika o id: {userId}").Information();
            try
            {
                var tasksdb = _context.Tasks.Where(t => t.UserId == userId);
                if(taskStatus.HasValue)
                {
                    tasksdb = tasksdb.Where(t => (int)t.Status == taskStatus.Value);
                }

                var tasksList = tasksdb.Select(x => new TaskModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Name = x.Name,
                    Description = x.Description,
                    Status = x.Status
                }).ToList();

                return DataResult<List<TaskModel>>.Succes(tasksList);

            }
            catch(Exception ex)
            {
                return DataResult<List<TaskModel>>.Error($"Błąd podczas pobierania zadan użytkowanika o id: {userId}, ex:{ex.Message}").Log();
            }
          
        }
    }
}
