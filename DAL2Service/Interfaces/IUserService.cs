using Base.Models;

namespace DAL2Service.Interfaces
{
    public interface IUserService
    {
        Task<string> Login(LoginModel model);
    }
}
