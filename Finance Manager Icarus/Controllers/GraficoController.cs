using Finance_Manager_Icarus.Controllers.Base;
using Finance_Manager_Icarus.Dtos;
using Finance_Manager_Icarus.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance_Manager_Icarus.Controllers;

[ApiController]
[Route("[controller]")]
public class GraficoController : ControllerFinanceManagerIcarusData
{
    private readonly BancoRepository _bancoRepository;
    private readonly CartaoRepository _cartaoRepository;
    private readonly MovimentacaoRepository _movimentacaoRepository;
    private readonly TipoMovimentacaoRepository _tipoMovimentacaoRepository;
    private readonly NomeMovimentacaoRepository _nomeMovimentacaoRepository;

    public GraficoController(
        UsuarioRepository usuarioRepository,
        BancoRepository bancoRepository,
        CartaoRepository cartaoRepository,
        MovimentacaoRepository movimentacaoRepository,
        TipoMovimentacaoRepository tipoMovimentacaoRepository,
        NomeMovimentacaoRepository nomeMovimentacaoRepository
    ) : base(usuarioRepository)
    {
        _bancoRepository = bancoRepository;
        _cartaoRepository = cartaoRepository;
        _movimentacaoRepository = movimentacaoRepository;
        _tipoMovimentacaoRepository = tipoMovimentacaoRepository;
        _nomeMovimentacaoRepository = nomeMovimentacaoRepository;
    }

    [Authorize, HttpGet("Linha/ByRangeData")]
    public IActionResult GetTypeLinhaByRangeData([FromQuery] DateTime date1, DateTime date2)
    {
        try
        {
            var user = GetFromCurrentUser();
            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            // 1. Buscar movimentações do usuário no intervalo
            var movimentacoes = _movimentacaoRepository.GetByUsuarioIdAndFilterByRangeData(user.Usuario_Id, date1, date2);

            if (!movimentacoes.Any())
                return Ok(new { message = "Nenhuma movimentação encontrada." });

            // 2. Agrupar por NomeMovimentacao e data (dia)
            var agrupado = movimentacoes
                .GroupBy(m => new
                {
                    Nome = m.NomeMovimentacao.Nome,
                    Data = m.Data.Date // Agrupar por dia. Pode usar mês se quiser.
                })
                .Select(g => new
                {
                    g.Key.Nome,
                    g.Key.Data,
                    Valor = g.Sum(m => m.Valor)
                })
                .ToList();

            // 3. Obter todas as datas no intervalo como labels
            var datas = Enumerable.Range(0, (date2.Date - date1.Date).Days + 1)
                .Select(offset => date1.Date.AddDays(offset))
                .ToList();

            var labels = datas.Select(d => d.ToString("dd/MM/yyyy")).ToList();

            // 4. Agrupar valores por NomeMovimentacao com lista por data
            // Constrói os datasets por NomeMovimentacao
            var datasets = agrupado
                .GroupBy(x => x.Nome)
                .Select(g => new DatasetDto
                {
                    Name = g.Key,
                    Data = datas.Select(data =>
                        g.FirstOrDefault(x => x.Data == data)?.Valor ?? 0
                    ).ToList(),
                    Type = "line"
                })
                .ToList();

            // 5. Montar DTO para retornar
            // Monta o DTO do gráfico
            var graficoDto = new GraficoDto
            {
                Type = "line",
                Labels = labels,
                Datasets = datasets
            };

            return Ok(graficoDto);
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex);
        }
    }
}
