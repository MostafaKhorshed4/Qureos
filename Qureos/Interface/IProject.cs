using Qureos.DTO;
using Qureos.Entity;

namespace Qureos.Interface
{
    public interface IProject
    {
        public Task<ResponseDTO> GetAll(int pageNumber = 1, int pageSize = 10);
        public Task<ResponseDTO> Add(Project obj);
        public Task<ResponseDTO> Update(Project obj);
        public Task<ResponseDTO> Delete(int id);
        public Task<ResponseDTO> GetById(int id);
    }
}
