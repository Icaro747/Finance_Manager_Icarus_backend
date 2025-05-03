using Finance_Manager_Icarus.Data;
using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Repositories.Base;

namespace Finance_Manager_Icarus.Repositories;

/// <summary>
/// Repositório responsável pelas operações de banco de dados da entidade Cartao.
/// </summary>
public class CartaoRepository : CrudRepository<Cartao>
{
    /// <summary>
    /// Inicializa uma nova instância do repositório Cartao.
    /// </summary>
    /// <param name="context">Contexto do banco de dados.</param>
    public CartaoRepository(FinanceManagerIcarusContext context) : base(context)
    { }
}
