using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskListApp.Autenticator;
using TaskListApp.DTO;
using TaskListApp.Enum;
using TaskListApp.Filters;
using TaskListApp.Models;
using TaskListApp.Repository.Interfaces;

namespace TaskListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        private long? GetUserLog()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            long userId = long.Parse(claim.Value);
            return userId;
        }

        [AuthorizePerfil(EPerfilType.Admin)]
        [HttpGet]
        public ActionResult<User> GetUsers()
        {
            var users = _userRepository.Users();
            return Ok(users);
        }

        [AuthorizePerfil(EPerfilType.Admin)]
        [HttpPost]
        public ActionResult<User> AddUser(UserDto userRequest)
        {
            User user = new User();

            user.Nome = userRequest.Nome;
            user.PerfilType = userRequest.PerfilType;
            user.Email = userRequest.Email;
            user.Senha  = userRequest.Senha;
            user.Ativo = user.Ativo;

            var retorno = _userRepository.SaveOrUpdate(user);
            return Ok(retorno);

            
        }

        [AuthorizePerfil(EPerfilType.Admin)]
        [HttpPut]
        public ActionResult<User> UpdateUser(UserDto userRequest)
        {
            User user = new User();

            user.Nome = userRequest.Nome;
            user.PerfilType = userRequest.PerfilType;
            user.Email = userRequest.Email;
            user.Senha = userRequest.Senha;
            user.Ativo = user.Ativo;

            var retorno = _userRepository.SaveOrUpdate(user);
            return Ok(retorno);
        }

        [HttpPost("autenticar")]
        public ActionResult<User> Autenticar(LoginDto login)
        {
            try
            {
                User? user = _userRepository.ValidAuthenticator(login.Email!, login.Senha!);
                if(user != null)
                {
                    string key = _configuration.GetSection("Token").Value!;
                    string infoToken = UserAutenticator.TokenGenerator(user, key);

                    return Ok(new TokenInfoDto { Token = infoToken, Usuario = user });
                }
                else
                {
                    throw new Exception("Usuário não identificado.");
                }
            }
            catch (Exception ex)
            {
                Thread.Sleep(new Random().Next(1000, 3000));
                throw ex;
            }
        }

    }
}
