using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qureos.DTO;
using Qureos.Entity;
using Qureos.Interface;

namespace Qureos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _obj;

        public UserController(IUser obj)
        {
            _obj = obj;
        }



        [HttpPost("Login")]
        public async Task<ResponseDTO> Login(LoginDTO credentials)
        {
            return await _obj.AuthorizeUser(credentials);
        }

        [HttpPost("SignUp")]
        public async Task<ResponseDTO> SignUp(User user)
        {
            return await _obj.Add(user);
        }

    }

}
