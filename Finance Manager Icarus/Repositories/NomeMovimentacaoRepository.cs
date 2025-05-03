using Finance_Manager_Icarus.Data;
using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Repositories.Base;

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
}
