using TaskListApp.Models;

namespace TaskListApp.Service.Api
{
    public interface IAuthService
    {
        public User UserLog { get; set; }
        Task Logout();
    }
}
