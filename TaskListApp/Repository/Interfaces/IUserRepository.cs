using TaskListApp.Enum;
using TaskListApp.Models;

namespace TaskListApp.Repository.Interfaces
{
    public interface IUserRepository
    {
        User SaveOrUpdate(User user);
        User? ValidAuthenticator(String nome, String senha);
        void EnableDesable(int id, Boolean ativo);
        IList<User> Users();
        bool IsAllowed(int idUser, EPerfilType perfilType);
    }
}
