using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TaskListApp.Enum;
using TaskListApp.Models;

namespace TaskListApp.Service.ServiceAuthenticator
{
    public class LocalStorageService : ILocalStorageService
    {
        private const string KEY_TOKEN = "token";

        protected IJSRuntime JSRuntime { get; }

        public LocalStorageService(IJSRuntime runtime)
        {
            JSRuntime = runtime;
        }



        public async Task ClearTokenAsyn()
        {
            await RemoveAsync(KEY_TOKEN);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var data = await JSRuntime.InvokeAsync<T>("localStorage.getItem", key);
            return data;
        }

        public async Task<User> GetDateUSerAsync()
        {
            User? userLoading = null;
            try
            {
                var token = await GetTokenAsync();

                if(token != null)
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                    var claims = jsonToken?.Claims;

                    if(claims != null && claims.Any())
                    {
                        var id = claims.FirstOrDefault(claim => "nameid".Equals(claim.Type, StringComparison.InvariantCultureIgnoreCase))?.Value;
                        var nome = claims.FirstOrDefault(claim => "nome".Equals(claim.Type, StringComparison.InvariantCultureIgnoreCase))?.Value;
                        var email = claims.FirstOrDefault(claim => "email".Equals(claim.Type, StringComparison.InvariantCultureIgnoreCase))?.Value;
                        var perfil = claims.FirstOrDefault(claim => "perfil".Equals(claim.Type, StringComparison.InvariantCultureIgnoreCase))?.Value;

                        userLoading = new User
                        {
                            Id = Convert.ToInt32(id),
                            Nome = nome,
                            Email = email,
                            //PerfilType = Enum.Parse<EPerfilType>(perfil)

                        };
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return await Task.FromResult(userLoading);
        }

        public async Task<string> GetTokenAsync()
        {
            try
            {
                var token = await GetAsync<string>(KEY_TOKEN);
                return StringReverse(token);
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }

        public async Task<bool> InsertUpdateTokenAsync(string token)
        {
            await SaveAsync(KEY_TOKEN, StringReverse(token));

            return true;
        }

        public async Task<bool> RemoveAsync(string key)
        {
            await JSRuntime.InvokeVoidAsync("localStorage.removeItem", key);
            return true;
        }

        public async Task<bool> SaveAsync(string key, object value)
        {
            await JSRuntime.InvokeVoidAsync("localStorage.getItem", KEY_TOKEN, value);
            return true;
        }

        public async Task<bool> TokenActive()
        {
            try
            {
                var token = await GetTokenAsync();
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return false;

                var expiryDateTime = jwtToken.ValidTo;

                return await Task.FromResult(expiryDateTime > DateTime.UtcNow);
            }
            catch (Exception)
            {

                return false;
            }
        }

        private string StringReverse(string original)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = original.Length - 1; i >0 ; i--)
            {
                stringBuilder.Append(original[i]);
            }
            return stringBuilder.ToString();
        }
    }
}
