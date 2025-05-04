using Finance_Manager_Icarus.Data;
using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Repositories.Base;
using Microsoft.EntityFrameworkCore;

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
                .ThenInclude(x => x.Categoria)
            .Where(m => 
                m.NomeMovimentacao.Usuario_Id == usuarioId &&
                m.Data >= date1 
                && m.Data <= date2
            )
            .ToList();
    }

    public List<Movimentacao> GetByUsuarioIdAndFilterByRangeData(Guid usuarioId, DateTime date1, DateTime date2, string tipo)
    {
        switch (tipo)
        {
            case "cartao":
                return _dbSet
                    .Include(x => x.Cartao)
                        .ThenInclude(x => x.Banco)
                    .Include(x => x.Banco)
                    .Include(x => x.NomeMovimentacao)
                        .ThenInclude(x => x.Categoria)
                    .Where(m =>
                        m.NomeMovimentacao.Usuario_Id == usuarioId &&
                        m.Data >= date1 &&
                        m.Data <= date2 &&
                        m.Cartao_Id != null
                    )
                    .ToList();

            case "corrente":
                return _dbSet
                    .Include(x => x.Cartao)
                        .ThenInclude(x => x.Banco)
                    .Include(x => x.Banco)
                    .Include(x => x.NomeMovimentacao)
                        .ThenInclude(x => x.Categoria)
                    .Where(m =>
                        m.NomeMovimentacao.Usuario_Id == usuarioId &&
                        m.Data >= date1 &&
                        m.Data <= date2 &&
                        m.Banco_Id != null
                    )
                    .ToList();

            default:
                throw new ArgumentException($"Tipo de movimentação inválido: {tipo}");
        }
    }


    public List<Movimentacao> GetByUsuarioId(Guid usuario_id)
    {
        return _dbSet
            .Include(x => x.NomeMovimentacao)
                .ThenInclude(x => x.Categoria)
            .Include(x => x.TipoMovimentacao)
            .Where(x => x.NomeMovimentacao.Usuario_Id == usuario_id)
            .ToList();
    }

    public List<Movimentacao> GetByNomeIdAndUsuarioId(Guid nome_id, Guid usuario_id)
    {
        return _dbSet
            .Include(x => x.NomeMovimentacao)
            .Where(x => x.NomeMovimentacao.Usuario_Id == usuario_id && x.Nome_movimentacao_Id == nome_id)
            .ToList();
    }
}
