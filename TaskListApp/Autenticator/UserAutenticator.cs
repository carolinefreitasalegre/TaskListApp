using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskListApp.Models;

namespace TaskListApp.Autenticator
{
    public static class UserAutenticator
    {
        public static string TokenGenerator(User user, string stringKey) 
        {
            if (user == null || user.Id == null || user.Ativo == false)
                throw new System.Exception("Usuário nulo/inválido");

            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Nome),
                //new Claim("Fornecedor", user.UsuarioFornecedor.ToString()),
                //new Claim("Perfil", user.GrupoPerfilAcoesPerfil ?? string.Empty),
            };

            var byteKey = Encoding.ASCII.GetBytes(stringKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var infotoken = tokenHandler.WriteToken(token);

            return infotoken;

        
        }
    }
}
