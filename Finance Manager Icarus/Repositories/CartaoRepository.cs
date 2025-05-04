using Finance_Manager_Icarus.Data;
using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Repositories.Base;
using Microsoft.EntityFrameworkCore;

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

    public List<Cartao> GetByBancoId(Guid id)
    {
        return _dbSet
            .Where(c => c.Banco_Id == id)
            .ToList();
    }

    public List<Cartao> GetByBancoIdAndUsuarioId(Guid bancoId, Guid usuarioId)
    {
        return _dbSet
            .Include(x => x.Banco)
            .Where(c => c.Banco_Id == bancoId && c.Banco.Usuario_Id == usuarioId)
            .ToList();
    }

    public List<Cartao> GetByUsuarioId(Guid usuarioId)
    {
        return _dbSet
            .Include(x => x.Banco)
            .Where(x => x.Banco.Usuario_Id == usuarioId)
            .ToList();
    }
}
