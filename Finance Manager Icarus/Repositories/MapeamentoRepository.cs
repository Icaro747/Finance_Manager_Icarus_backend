using Finance_Manager_Icarus.Data;
using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Finance_Manager_Icarus.Repositories;

/// <summary>
/// Repositório responsável pelas operações de banco de dados da entidade Mapeamento.
/// </summary>
public class MapeamentoRepository : CrudRepository<Mapeamento>
{
    /// <summary>
    /// Inicializa uma nova instância do repositório Mapeamento.
    /// </summary>
    /// <param name="context">Contexto do banco de dados.</param>
    public MapeamentoRepository(FinanceManagerIcarusContext context) : base(context)
    { }

    public List<Mapeamento> GetByUsuarioId(Guid usuario_id)
    {
        return _dbSet
            .Include(x => x.ItensMapeamentos)
            .Where(x => x.Usuario_Id == usuario_id)
            .ToList();
    }
}
