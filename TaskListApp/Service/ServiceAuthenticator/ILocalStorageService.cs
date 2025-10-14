using TaskListApp.Models;

namespace TaskListApp.Service.ServiceAuthenticator
{
    public interface ILocalStorageService
    {
        Task<bool> InsertUpdateTokenAsync(string token);
        Task ClearTokenAsyn();
        Task<T> GetAsync<T>(string key);
        Task<User> GetDateUSerAsync();
        Task<string> GetTokenAsync();
        Task<bool> RemoveAsync(string key);
        Task<bool> SaveAsync(string key, object value);
        Task<bool> TokenActive();
    }
}
