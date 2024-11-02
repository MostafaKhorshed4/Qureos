using Microsoft.EntityFrameworkCore;
using Qureos.DTO;
using Qureos.Entity;
using Qureos.Framework;
using Qureos.Interface;

namespace Qureos.Reposetories
{
    public class TasksCommentRepo : ITasksComment
    {

        private readonly QureosDbContext _dbContext;
        private readonly ResponseDTO _response;

        public TasksCommentRepo(QureosDbContext dbContext)
        {
            _dbContext = dbContext;
            _response = new ResponseDTO();
        }



        public async Task<ResponseDTO> Add(TasksComment obj)
        {
            try
            {
                await _dbContext.AddAsync(obj);

                // Avoid SaveChangesAsync if obj is invalid
                if (_dbContext.Entry(obj).IsKeySet)
                {
                    await _dbContext.SaveChangesAsync();
                    _response.Message = "Success";
                }
                else
                {
                    _response.Message = "Invalid Tasks object.";
                }
            }
            catch (Exception ex)
            {

                _response.Message = ex.Message;
            }
            return _response;
        }



        public async Task<ResponseDTO> GetCommentsByUserId(int UserId)
        {
            try
            {

                _response.Obj = _dbContext.Comments.FirstOrDefaultAsync(x => x.UserId == UserId);

            }
            catch (Exception ex)
            {

                _response.Message = ex.Message;
            }
            return _response;
        }
        public async Task<ResponseDTO> GetCommentsByTaskId(int TaskId)
        {
            try
            {

                _response.Obj = _dbContext.Comments.FirstOrDefaultAsync(x => x.TaskId == TaskId);

            }
            catch (Exception ex)
            {

                _response.Message = ex.Message;
            }
            return _response;
        }


    }
}
