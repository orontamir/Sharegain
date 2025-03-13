using Base.Models;
using DAL2Service.DAL.Sql;
using DAL2Service.Helpers;
using DAL2Service.Interfaces;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Text;

namespace DAL2Service.Services
{
    public class UserService : DalService, IUserService
    {
        readonly JwtService _jwtService;
        public UserService(RepositoryBase repo, JwtService jwtService) : base(repo)
        {
            _jwtService = jwtService;
        }
        public async Task<string> Login(LoginModel model)
        {
           
            var hashedPassword = HashHelper.CalculateHash(model.Password, model.Name);

            var userEntity = await Repository.Login(model.Name, hashedPassword);
            if (userEntity != null)
            {
                return _jwtService.GenerateSecurityToken_ByName(userEntity.Name, userEntity.Id);
            }
            else
            {
                return null;
            }
        }
    }
}
