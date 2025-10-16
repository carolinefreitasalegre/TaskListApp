
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;

namespace TaskListApp.Service.ServiceAuthenticator
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly NavigationManager _navigationManager;

        public HttpClientFactory(ILocalStorageService localStorageService, NavigationManager navigationManager)
        {
            _localStorageService = localStorageService;
            _navigationManager = navigationManager;
        }
        public async Task<HttpClient> HttpClientWithToken()
        {
            var urlBase = _navigationManager.BaseUri;
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(urlBase);

            var token = await _localStorageService.GetTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("beares", token);

            return await Task.FromResult(httpClient);
        }
    }
}
