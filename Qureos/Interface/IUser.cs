using Qureos.DTO;
using Qureos.Entity;

namespace Qureos.Interface
{
    public interface IUser
    {
        Task<ResponseDTO> Add(User obj);
        Task<ResponseDTO> AuthorizeUser(LoginDTO login);
    }
}
