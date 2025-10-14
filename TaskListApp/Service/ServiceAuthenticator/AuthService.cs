using Microsoft.AspNetCore.Components;
using TaskListApp.Models;
using TaskListApp.Service.ServiceAuthenticator;

namespace TaskListApp.Service.Api
{
    public class AuthService : IAuthService
    {
        public User UserLog { get; set; }

        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorage;


        public AuthService(NavigationManager navigationManager, ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _navigationManager = navigationManager;
        }



        public async Task Logout()
        {
            await _localStorage.ClearTokenAsyn();
            _navigationManager.NavigateTo("/", forceLoad: true);
        }
    }
}
