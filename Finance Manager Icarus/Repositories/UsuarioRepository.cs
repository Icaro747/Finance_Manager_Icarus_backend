using Finance_Manager_Icarus.Data;
using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Repositories.Base;

namespace Finance_Manager_Icarus.Repositories;

/// <summary>
/// Repositório responsável pelas operações de banco de dados da entidade Usuario.
/// </summary>
public class UsuarioRepository : CrudRepository<Usuario>
{
    /// <summary>
    /// Inicializa uma nova instância do repositório Usuario.
    /// </summary>
    /// <param name="context">Contexto do banco de dados.</param>
    public UsuarioRepository(FinanceManagerIcarusContext context) : base(context)
    { }


    public Usuario? ValidateUserCredentials(string email, string password)
    {
        // Encontrar o usuário pelo nome de usuário
        var user = _dbSet.FirstOrDefault(u => u.Email == email);
        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Senha))
        {
            return user;
        }
        return null;
    }
}