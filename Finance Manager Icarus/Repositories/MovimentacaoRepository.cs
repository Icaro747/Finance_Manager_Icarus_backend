using Finance_Manager_Icarus.Data;
using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Repositories.Base;

namespace Finance_Manager_Icarus.Repositories;

/// <summary>
/// Repositório responsável pelas operações de banco de dados da entidade Movimentacao.
/// </summary>
public class MovimentacaoRepository : CrudRepository<Movimentacao>
{
    /// <summary>
    /// Inicializa uma nova instância do repositório Movimentacao.
    /// </summary>
    /// <param name="context">Contexto do banco de dados.</param>
    public MovimentacaoRepository(FinanceManagerIcarusContext context) : base(context)
    { }
}
