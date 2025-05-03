using Finance_Manager_Icarus.Data;
using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Repositories.Base;

namespace Finance_Manager_Icarus.Repositories;

/// <summary>
/// Repositório responsável pelas operações de banco de dados da entidade Banco.
/// </summary>
public class BancoRepository : CrudRepository<Banco>
{
    /// <summary>
    /// Inicializa uma nova instância do repositório Banco.
    /// </summary>
    /// <param name="context">Contexto do banco de dados.</param>
    public BancoRepository(FinanceManagerIcarusContext context) : base(context)
    { }
}
