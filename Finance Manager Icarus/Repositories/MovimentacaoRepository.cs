using Finance_Manager_Icarus.Data;
using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    public List<Movimentacao> GetByUsuarioIdAndFilterByRangeData(Guid usuarioId, DateTime date1, DateTime date2)
    {
        return _dbSet
            .Include(x => x.Cartao)
                .ThenInclude(x => x.Banco)
            .Include(x => x.Banco)
            .Include(x => x.NomeMovimentacao)
            .Where(m => m.NomeMovimentacao.Usuario_Id == usuarioId && m.Data >= date1 && m.Data <= date2)
            .ToList();
    }
}
