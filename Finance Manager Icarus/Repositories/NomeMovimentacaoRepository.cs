using Finance_Manager_Icarus.Data;
using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Finance_Manager_Icarus.Repositories;

/// <summary>
/// Repositório responsável pelas operações de banco de dados da entidade NomeMovimentacao.
/// </summary>
public class NomeMovimentacaoRepository : CrudRepository<NomeMovimentacao>
{
    /// <summary>
    /// Inicializa uma nova instância do repositório NomeMovimentacao.
    /// </summary>
    /// <param name="context">Contexto do banco de dados.</param>
    public NomeMovimentacaoRepository(FinanceManagerIcarusContext context) : base(context)
    { }

    public NomeMovimentacao? GetByNomeAndUsuarioId(string nome, Guid usuario_id)
    {
        return _dbSet.FirstOrDefault(nm => nm.Nome == nome && nm.Usuario_Id == usuario_id);
    }

    public List<NomeMovimentacao> GetByUsuarioId(Guid usuario_id)
    {
        return _dbSet
            .Include(x => x.Categoria)
            .Where(x => x.Usuario_Id == usuario_id)
            .ToList();
    }
}
