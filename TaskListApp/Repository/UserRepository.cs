using Dapper;
using TaskListApp.DataContext;
using TaskListApp.Enum;
using TaskListApp.Models;
using TaskListApp.Repository.Interfaces;

namespace TaskListApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDapperContext _dapperContext;
        public UserRepository(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public void EnableDesable(int id, bool ativo)
        {
            throw new NotImplementedException();
        }

        public bool IsAllowed(int idUser, EPerfilType perfilType)
        {
            throw new NotImplementedException();
        }

        public User SaveOrUpdate(User user)
        {
            using (var connection = _dapperContext.CreateConnection()) 
            {
                var sql = @"IF EXISTS (SELECT 1 FROM [User] WHERE Id = @Id)
                            BEGIN
                                UPDATE [User]
                                SET Nome = @Nome,
                                    Senha = @Senha,
                                    Email = @Email,
                                    PerfilType = @PerfilType,
                                    Ativo = @Ativo
                                WHERE Id = @Id;

                                SELECT * FROM [User] WHERE Id = @Id;
                            END
                            ELSE
                            BEGIN
                                INSERT INTO [User] (Nome, Senha, Email, PerfilType, Ativo)
                                VALUES (@Nome, @Senha, @Email, @PerfilType, @Ativo);

                                SELECT * FROM [User] WHERE Id = SCOPE_IDENTITY();
                            END";

                connection.Execute(sql, new
                {
                    id = user.Id,
                    nome = user.Nome,
                    senha = user.Senha,
                    perfilType = user.PerfilType,
                    email = user.Email,
                    ativo = user.Ativo,

                });
                return user;
            }
        }

        public IList<User> Users()
        {
            using(var connection = _dapperContext.CreateConnection())
            {
                var sql = @"SELECT * FROM [User]";

                return (IList<User>)connection.Query<User>(sql);

            }
        }

        public User? ValidAuthenticator(string nome, string senha)
        {
            User? user;

            using(var connection = _dapperContext.CreateConnection())
            {
                var sql = @"SELECT * FROM [USER] WHERE [Nome]=@nome";
                user = (User)connection.Query<User>(sql, new
                {
                    nome = nome
                });

                if (user != null && String.Equals(user.Senha, senha) && user.Ativo == true)
                    return user;
                else
                    throw new Exception("Usuário ou senha não confere.");
            }
        }
    }
}
