using Finance_Manager_Icarus.Data;
using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Repositories.Base;

namespace Finance_Manager_Icarus.Repositories;

/// <summary>
/// Repositório responsável pelas operações de banco de dados da entidade Categoria.
/// </summary>
public class CategoriaRepository : CrudRepository<Categoria>
{
    /// <summary>
    /// Inicializa uma nova instância do repositório Categoria.
    /// </summary>
    /// <param name="context">Contexto do banco de dados.</param>
    public CategoriaRepository(FinanceManagerIcarusContext context) : base(context)
    { }

    public List<Categoria> GetByUsuarioId(Guid usuario_id)
    {
        return _dbSet.Where(x => x.Usuario_Id == usuario_id).ToList();
    }
}
