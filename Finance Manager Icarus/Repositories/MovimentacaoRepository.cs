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

    public Movimentacao? GetVerificaoMovimentacao(Movimentacao movimentacao)
    {
        return _dbSet.Where(x =>
                x.Data.Date == movimentacao.Data.Date &&
                x.Valor == movimentacao.Valor &&
                x.Descricao == movimentacao.Descricao
            ).FirstOrDefault();
    }

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
            .Where(x => 
                x.NomeMovimentacao.Usuario_Id == usuario_id &&
                x.Nome_Movimentacao_Id == nome_id
            )
            .ToList();
    }

    public List<Movimentacao> GetByUsuarioIdAndRange(Guid usuarioId, DateTime dataInicio, DateTime dataFim)
    {
        return _dbSet
            .Include(m => m.NomeMovimentacao)
                .ThenInclude(n => n.Categoria)
            .Where(m => m.NomeMovimentacao.Usuario_Id == usuarioId &&
                        m.Data >= dataInicio && m.Data <= dataFim)
            .ToList();
    }

    /// <summary>
    /// Calcula a média mensal de gastos por categoria considerando zeros nos meses sem movimentação
    /// </summary>
    /// <param name="usuarioId">ID do usuário</param>
    /// <param name="dataReferencia">Data de referência</param>
    /// <param name="quantidadeMeses">Quantidade de meses para calcular a média</param>
    /// <returns>Dicionário com categoria e sua respectiva média mensal</returns>
    public Dictionary<string, decimal> GetMediaMensalPorCategoriaComZeros(
        Guid usuarioId, DateTime dataReferencia, int quantidadeMeses = 12)
    {
        var gastosPorCategoria = GetGastosPorCategoriaUltimosNMesesComZeros(usuarioId, dataReferencia, quantidadeMeses);

        return gastosPorCategoria.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.Average()
        );
    }

    /// <summary>
    /// Busca gastos por categoria nos últimos N meses, preenchendo com zero os meses sem movimentação
    /// </summary>
    /// <param name="usuarioId">ID do usuário</param>
    /// <param name="dataReferencia">Data de referência para calcular os meses anteriores</param>
    /// <param name="quantidadeMeses">Quantidade de meses para analisar</param>
    /// <returns>Dicionário com categoria como chave e lista de valores mensais como valor</returns>
    public Dictionary<string, List<decimal>> GetGastosPorCategoriaUltimosNMesesComZeros(
        Guid usuarioId, DateTime dataReferencia, int quantidadeMeses = 12)
    {
        // Calcular período dos últimos N meses
        var inicioMesReferencia = new DateTime(dataReferencia.Year, dataReferencia.Month, 1);
        var inicioPeriodo = inicioMesReferencia.AddMonths(-quantidadeMeses);
        var fimPeriodo = inicioMesReferencia.AddDays(-1);

        // Gerar lista de todos os meses do período
        var mesesPeriodo = Enumerable.Range(0, quantidadeMeses)
            .Select(i => inicioPeriodo.AddMonths(i))
            .Select(d => new DateTime(d.Year, d.Month, 1))
            .ToList();

        // Buscar movimentações do período
        var movimentacoes = _dbSet
            .Include(x => x.NomeMovimentacao)
                .ThenInclude(x => x.Categoria)
            .Where(m => m.NomeMovimentacao.Usuario_Id == usuarioId &&
                        m.Data >= inicioPeriodo && m.Data <= fimPeriodo &&
                        !m.Entrada &&
                        m.NomeMovimentacao.Categoria != null)
            .ToList();

        // Agrupar por categoria e mês
        var gastosPorCategoriaEMes = movimentacoes
            .GroupBy(m => new {
                Categoria = m.NomeMovimentacao.Categoria!.Nome,
                AnoMes = new DateTime(m.Data.Year, m.Data.Month, 1)
            })
            .ToDictionary(g => g.Key, g => g.Sum(m => m.Valor));

        // Obter todas as categorias
        var todasCategorias = movimentacoes
            .Select(m => m.NomeMovimentacao.Categoria!.Nome)
            .Distinct()
            .ToList();

        // Montar resultado com zeros para meses sem movimentação
        var resultado = new Dictionary<string, List<decimal>>();

        foreach (var categoria in todasCategorias)
        {
            var valoresPorMes = mesesPeriodo.Select(mes => {
                var chave = new { Categoria = categoria, AnoMes = mes };
                return gastosPorCategoriaEMes.ContainsKey(chave) ? gastosPorCategoriaEMes[chave] : 0m;
            }).ToList();

            resultado[categoria] = valoresPorMes;
        }

        return resultado;
    }

}
