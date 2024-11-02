using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qureos.DTO;
using Qureos.Entity;
using Qureos.Interface;

namespace Qureos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasks _obj;

        public TasksController(ITasks obj)
        {
            _obj = obj;
        }

        [Authorize(Roles = nameof(Role.manager))]
        [HttpPost]
        public async Task<ResponseDTO> CreateTask(ProjectTasks tasks)
        {
            return await _obj.Add(tasks);
        }
        [Authorize(Roles = nameof(Role.manager))]
        [HttpPost]
        public async Task<ResponseDTO> UpdateTask(ProjectTasks tasks)
        {
            return await _obj.Update(tasks);
        }
        [Authorize(Roles = nameof(Role.Employee))]
        [HttpGet]
        public async Task<ResponseDTO> GetAll( int Page , int PageSize)
        {
            return await _obj.GetAll(Page,PageSize);
        }
        [Authorize(Roles = nameof(Role.Employee))]
        [HttpGet]
        public async Task<ResponseDTO> GetTaskById(int tasksId)
        {
            return await _obj.GetById(tasksId);
        }
        [Authorize(Roles = nameof(Role.manager))]
        [HttpPost]
        public async Task<ResponseDTO> GetOverdueTasks()
        {
            return await _obj.GetOverdueTasks();
        }
        [Authorize(Roles = nameof(Role.manager))]
        [HttpDelete]
        public async Task<ResponseDTO> DeleteTask(int Id)
        {
            return await _obj.Delete(Id);
        }
       



    }
}
