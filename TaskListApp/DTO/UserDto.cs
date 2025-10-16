using TaskListApp.Enum;

namespace TaskListApp.DTO
{
    public class UserDto
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public EPerfilType PerfilType { get; set; }
        public string Email { get; set; }
        public Boolean Ativo { get; set; }
    }
}
