using Microsoft.EntityFrameworkCore;
using Qureos.DTO;
using Qureos.Entity;
using Qureos.Framework;
using Qureos.Interface;

namespace Qureos.Reposetories
{
    public class ProjectRepo : IProject
    {

        private readonly QureosDbContext _dbContext;
        private readonly ResponseDTO _response;

        public ProjectRepo(QureosDbContext dbContext) 
        {
            _dbContext = dbContext;
            _response = new ResponseDTO();
        }

        public async Task<ResponseDTO> Add(Project obj)
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
                    _response.Message = "Invalid project object.";
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
                var project = await _dbContext.Projects.FindAsync(id);
                if (project != null)
                {
                    _dbContext.Projects.Remove(project);
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

                // Fetch the total count of projects for pagination metadata
                int totalCount = await _dbContext.Projects.CountAsync();

                // Fetch the paginated list of projects
                _response.Obj = await _dbContext.Projects
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

                _response.Obj = _dbContext.Projects.FirstOrDefaultAsync(x=>x.Equals(id));

            }
            catch (Exception ex)
            {

                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> Update(Project obj)
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
                    _response.Message = "Invalid project object.";
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
