using Microsoft.EntityFrameworkCore;
using Qureos.DTO;
using Qureos.Entity;
using Qureos.Framework;
using Qureos.Interface;

namespace Qureos.Reposetories
{
    public class TaskRepo : ITasks
    {

        private readonly QureosDbContext _dbContext;
        private readonly ResponseDTO _response;

        public TaskRepo(QureosDbContext dbContext)
        {
            _dbContext = dbContext;
            _response = new ResponseDTO();
        }

        public async Task<ResponseDTO> Add(ProjectTasks obj)
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

        public async Task<ResponseDTO> Delete(int id)
        {
            try
            {
                var task = await _dbContext.ProjectTasks.FindAsync(id);
                if (task != null)
                {
                    _dbContext.ProjectTasks.Remove(task);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                int skip = (pageNumber - 1) * pageSize;

                int totalCount = await _dbContext.ProjectTasks.CountAsync();

                _response.Obj = await _dbContext.ProjectTasks
                                               .Skip(skip)
                                               .Take(pageSize)
                                               .ToListAsync();
            }
            catch (Exception ex)
            {

                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetById(int id)
        {
            try
            {

                _response.Obj = _dbContext.ProjectTasks.FirstOrDefaultAsync(x => x.TaskId==id);

            }
            catch (Exception ex)
            {

                _response.Message = ex.Message;
            }
            return _response;
        }
        public async Task<ResponseDTO> GetOverdueTasks()
        {
            try
            {

                _response.Obj = _dbContext.ProjectTasks.Where(task => task.EndDate < DateTime.Now &&  task.Status != Status.Completed).ToListAsync(); 

            }
            catch (Exception ex)
            {

                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> Update(ProjectTasks obj)
        {
            try
            {
                _dbContext.Update(obj);

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
    }
}
