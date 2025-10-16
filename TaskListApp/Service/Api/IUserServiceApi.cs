using TaskListApp.DTO;
using TaskListApp.Models;

namespace TaskListApp.Service.Api
{
    public interface IUserServiceApi
    {
        Task<User> Save(UserDto user);
        Task<User> Update(UserDto user);
        Task<IList<User>?> Users();
        Task<User?> GetById(int id);
        Task<TokenInfoDto?> Authenticator(LoginDto login);
    }
}
