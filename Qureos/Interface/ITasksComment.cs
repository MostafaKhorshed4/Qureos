using Qureos.DTO;
using Qureos.Entity;

namespace Qureos.Interface
{
    public interface ITasksComment
    {
        Task<ResponseDTO> Add(TasksComment obj);
        Task<ResponseDTO> GetCommentsByTaskId(int TaskId);
        Task<ResponseDTO> GetCommentsByUserId(int UserId);
    }
}
