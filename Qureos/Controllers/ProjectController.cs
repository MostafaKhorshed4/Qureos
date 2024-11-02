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
    public class ProjectController : ControllerBase
    {
        private readonly IProject _obj;

        public ProjectController(IProject obj)
        {
            _obj = obj;
        }


        [Authorize(Roles = nameof(Role.manager))]
        [HttpPost]
        public async Task<ResponseDTO> CreateProject(Project project)
        {
            return await _obj.Add(project);
        }

        public async Task<ResponseDTO> UpdateTask(Project project)
        {
            return await _obj.Update(project);
        }
        [Authorize(Roles = nameof(Role.Employee))]
        [HttpGet]
        public async Task<ResponseDTO> GetAll(int Page, int PageSize)
        {
            return await _obj.GetAll(Page, PageSize);
        }
        [Authorize(Roles = nameof(Role.Employee))]
        [HttpPost]
        public async Task<ResponseDTO> GetProjectById(int projectId)
        {
            return await _obj.GetById(projectId);
        }
       
        [Authorize(Roles = nameof(Role.manager))]
        [HttpDelete]
        public async Task<ResponseDTO> DeleteProject(int Id)
        {
            return await _obj.Delete(Id);
        }
    }
}
