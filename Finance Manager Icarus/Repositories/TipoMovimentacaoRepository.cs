using Finance_Manager_Icarus.Data;
using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Repositories.Base;

namespace Finance_Manager_Icarus.Repositories;

/// <summary>
/// Repositório responsável pelas operações de banco de dados da entidade TipoMovimentacao.
/// </summary>
public class TipoMovimentacaoRepository : CrudRepository<TipoMovimentacao>
{
    /// <summary>
    /// Inicializa uma nova instância do repositório TipoMovimentacao.
    /// </summary>
    /// <param name="context">Contexto do banco de dados.</param>
    public TipoMovimentacaoRepository(FinanceManagerIcarusContext context) : base(context)
    { }

    public TipoMovimentacao? GetByNome(string nome)
    {
        return _dbSet.FirstOrDefault(tm => tm.Nome == nome);
    }
    public TipoMovimentacao? GetByNomeAndUsuarioId(string nome, Guid usuario_id)
    {
        return _dbSet.FirstOrDefault(tm => tm.Nome == nome && tm.Usuario_Id == usuario_id);
    }

}
