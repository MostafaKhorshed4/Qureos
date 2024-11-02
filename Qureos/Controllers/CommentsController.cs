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
    public class CommentsController : ControllerBase
    {
        ITasksComment _obj;
        public CommentsController(ITasksComment obj)
        {
            _obj = obj;
        }

        [Authorize(Roles = $"{nameof(Role.manager)},{nameof(Role.Employee)}")]
        [HttpPost]
        public async Task<ResponseDTO> AddComment(TasksComment tasks)
        {
            return await _obj.Add(tasks);
        }


        [Authorize(Roles = nameof(Role.manager))]
        [HttpGet]
        public async Task<ResponseDTO> GetCommentsByTaskId(int tasksId)
        {
            return await _obj.GetCommentsByTaskId(tasksId);
        }
        [Authorize(Roles = nameof(Role.manager))]
        [HttpGet]
        public async Task<ResponseDTO> GetTaskById(int UserId)
        {
            return await _obj.GetCommentsByUserId(UserId);
        }
    }
}
