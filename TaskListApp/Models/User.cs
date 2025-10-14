using System.ComponentModel.DataAnnotations.Schema;
using TaskListApp.Enum;

namespace TaskListApp.Models
{
    [Table("[User]")]

    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public EPerfilType PerfilType { get; set; }
        public string Email { get; set; }
        public Boolean Ativo { get; set; }
    }
}
