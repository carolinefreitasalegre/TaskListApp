using Microsoft.AspNetCore.Authorization;
using TaskListApp.Enum;

namespace TaskListApp.Filters
{
    public class AuthorizePerfilAttribute : AuthorizeAttribute
    {
        public AuthorizePerfilAttribute(EPerfilType perfil)
        {
            Roles = perfil.ToString();
        }
    }
}
