using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Qureos.DTO;
using Qureos.Entity;
using Qureos.Framework;
using Qureos.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Qureos.Reposetories
{
    public class UserRepo: IUser
    {
        private readonly IConfiguration _configuration;

        private readonly QureosDbContext _dbContext;
        private readonly ResponseDTO _response;

        public UserRepo(QureosDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _response = new ResponseDTO();
            _configuration = configuration;
        }
        public async Task<ResponseDTO> Add(User obj)
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
                    _response.Message = "Invalid User object.";
                }
            }
            catch (Exception ex)
            {

                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> AuthorizeUser(LoginDTO login)
        {
            try
            {
                User user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == login.Username && x.Password == login.Password);

                if (user == null)
                {
                    _response.Obj = null;
                    _response.Message = "Invalid credentials";
                }
                else
                {
                   string Token =  GenerateJwtToken(user);

                    _response.Obj = Token;
                }
            }
            catch (Exception ex)
            {

                _response.Message = ex.Message;
            }
            return _response;
        }

        private string GenerateJwtToken(User user)
        {
            var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(24),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
