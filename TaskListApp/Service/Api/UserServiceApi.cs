using TaskListApp.DTO;
using System.Net.Http.Json;
using TaskListApp.Models;
using TaskListApp.Service.ServiceAuthenticator;
using System.Net;

namespace TaskListApp.Service.Api
{
    public class UserServiceApi : IUserServiceApi
    {
        private readonly ServiceAuthenticator.IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorageService;
        private readonly string _baseUrl;

        public UserServiceApi(ServiceAuthenticator.IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageService = localStorageService;
            _baseUrl = "/api/user";
        }

        public async Task<TokenInfoDto?> Authenticator(LoginDto login)
        {
            using(var httpClient = await _httpClientFactory.HttpClientWithToken())
            {
                var result = await httpClient.PostAsJsonAsync($"{_baseUrl + "/autenticar"}", login);

                if (result.IsSuccessStatusCode)
                    return await result.Content.ReadFromJsonAsync<TokenInfoDto>();
                else if(result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Não autorizado");
                }
                else
                {
                    throw new Exception(result.Content.ReadAsStringAsync().Result);
                }
                        
            }
        }

        public async Task<User?> GetById(int id)
        {
            using (var httpClient = await _httpClientFactory.HttpClientWithToken())
            {
                var result = await httpClient.GetAsync($"{_baseUrl}/{id}");

                if (result.StatusCode == HttpStatusCode.OK)
                    return await result.Content.ReadFromJsonAsync<User>();
                else if (result.StatusCode == HttpStatusCode.NoContent)
                    return default(User);
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException("Não autorizado.");
                else
                    throw new Exception(result.Content.ReadAsStringAsync().Result);

            }
        }

        public async Task<User> Save(UserDto user)
        {
             using(var httpClint = await _httpClientFactory.HttpClientWithToken())
            {
                var result = await httpClint.PostAsJsonAsync(_baseUrl, user);

                if (result.StatusCode == HttpStatusCode.OK)
                    return await result.Content.ReadFromJsonAsync<User>();
                else if (result.StatusCode == HttpStatusCode.NoContent)
                    return default(User);
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Não autorizado.");
                }
                else
                    throw new Exception(result.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task<User> Update(UserDto user)
        {
            using(var httpClient = await _httpClientFactory.HttpClientWithToken())
            {
                var result = await httpClient.PutAsJsonAsync(_baseUrl, user);

                if(result.StatusCode == HttpStatusCode.OK)
                    return await result.Content.ReadFromJsonAsync<User>();
                else if (result.StatusCode == HttpStatusCode.NoContent)
                    return default(User);
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Não autorizado.");
                }
                else
                    throw new Exception(result.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task<IList<User>?> Users()
        {
            using(var httpClient = await _httpClientFactory.HttpClientWithToken())
            {
                var result = await httpClient.GetAsync(_baseUrl);

                if (result.StatusCode == HttpStatusCode.OK)
                    return await result.Content.ReadFromJsonAsync<IList<User>>();
                else if (result.StatusCode == HttpStatusCode.NoContent)
                    return null;
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Não autorizado.");
                }
                else
                    throw new Exception(result.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
