using Qureos.DTO;
using Qureos.Entity;

namespace Qureos.Interface
{
    public interface ITasks
    {
        public Task<ResponseDTO> GetAll(int pageNumber = 1, int pageSize = 10);
        public Task<ResponseDTO> Add (ProjectTasks obj);
        public Task<ResponseDTO> Update (ProjectTasks obj);
        public Task<ResponseDTO> Delete(int id);
        public Task<ResponseDTO> GetById(int id);
        Task<ResponseDTO> GetOverdueTasks();
    }
}
