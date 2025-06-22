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
            var movimentacoes = _movimentacaoRepository
                .GetByUsuarioIdAndFilterByRangeData(user.Usuario_Id, date1, date2);

            if (!movimentacoes.Any())
                return Ok(new { message = "Nenhuma movimentação encontrada." });

            // 2. Agrupar por Categoria (ou "não classificado") e data (dia)
            var agrupado = movimentacoes
                .GroupBy(m => new
                {
                    Categoria = m.NomeMovimentacao.Categoria?.Nome ?? "não classificado",
                    Data = m.Data.Date
                })
                .Select(g => new
                {
                    Nome = g.Key.Categoria,
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

            // 4.1 Adiciona linha com soma total de todas as categorias por dia
            var totalPorDia = datas.Select(data =>
                agrupado.Where(x => x.Data == data).Sum(x => x.Valor)
            ).ToList();

            datasets.Add(new DatasetDto
            {
                Name = "Total",
                Data = totalPorDia,
                Type = "line"
            });

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

    [Authorize, HttpGet("Pizza/ByRangeData")]
    public IActionResult GetPizzaByRangeData([FromQuery] DateTime date1, DateTime date2)
    {
        try
        {
            var user = GetFromCurrentUser();
            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            // 1. Buscar movimentações do usuário no intervalo
            var movimentacoes = _movimentacaoRepository
                .GetByUsuarioIdAndFilterByRangeData(user.Usuario_Id, date1, date2);

            if (!movimentacoes.Any())
                return Ok(new { message = "Nenhuma movimentação encontrada." });

            // 2. Agrupar por Categoria (ou "não classificado") e somar os valores
            var agrupado = movimentacoes
                .GroupBy(m => m.NomeMovimentacao.Categoria?.Nome ?? "não classificado")
                .Select(g => new
                {
                    Nome = g.Key,
                    Valor = g.Sum(m => m.Valor)
                })
                .OrderByDescending(g => g.Valor)
                .ToList();

            // 3. Separar os 4 maiores e somar o restante
            var top4 = agrupado.Take(4).ToList();
            var outrosValor = agrupado.Skip(4).Sum(g => g.Valor);

            if (outrosValor > 0)
            {
                top4.Add(new { Nome = "Outros", Valor = outrosValor });
            }

            // 4. Montar o DTO
            var graficoDto = new GraficoDto
            {
                Type = "pie",
                Labels = top4.Select(g => g.Nome).ToList(),
                Datasets = new List<DatasetDto>
                {
                    new DatasetDto
                    {
                        Name = "",
                        Data = top4.Select(g => g.Valor).ToList()
                    }
                }
            };

            return Ok(graficoDto);
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex.Message);
        }
    }

    [Authorize, HttpGet("Pizza/ByRangeDataAndByTipoGasto")]
    public IActionResult GetPizzaByRangeDataByTipoGasto([FromQuery] DateTime date1, DateTime date2, string tipo)
    {
        try
        {
            var user = GetFromCurrentUser();
            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            // 1. Buscar movimentações do usuário no intervalo
            var movimentacoes = _movimentacaoRepository
                .GetByUsuarioIdAndFilterByRangeData(user.Usuario_Id, date1, date2, tipo);

            if (!movimentacoes.Any())
                return Ok(new { message = "Nenhuma movimentação encontrada." });

            // 2. Agrupar por Categoria (ou "não classificado") e somar os valores
            var agrupado = movimentacoes
                .GroupBy(m => m.NomeMovimentacao.Categoria?.Nome ?? "não classificado")
                .Select(g => new
                {
                    Nome = g.Key,
                    Valor = g.Sum(m => m.Valor)
                })
                .OrderByDescending(g => g.Valor)
                .ToList();

            // 3. Separar os 4 maiores e somar o restante
            var top4 = agrupado.Take(4).ToList();
            var outrosValor = agrupado.Skip(4).Sum(g => g.Valor);

            if (outrosValor > 0)
            {
                top4.Add(new { Nome = "Outros", Valor = outrosValor });
            }

            // 4. Montar o DTO
            var graficoDto = new GraficoDto
            {
                Type = "pie",
                Labels = top4.Select(g => g.Nome).ToList(),
                Datasets = new List<DatasetDto>
                {
                    new DatasetDto
                    {
                        Name = "",
                        Data = top4.Select(g => g.Valor).ToList()
                    }
                }
            };

            return Ok(graficoDto);
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex.Message);
        }
    }

    [Authorize, HttpGet("Column/ComparativoPorCategoria")]
    public IActionResult GetComparativoPorCategoria([FromQuery] DateTime referencia)
    {
        try
        {
            var user = GetFromCurrentUser();
            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            // Gastos do mês atual por categoria
            var inicioMesAtual = new DateTime(referencia.Year, referencia.Month, 1);
            var fimMesAtual = inicioMesAtual.AddMonths(1).AddDays(-1);

            var movimentacoesMesAtual = _movimentacaoRepository
                .GetByUsuarioIdAndRange(user.Usuario_Id, inicioMesAtual, fimMesAtual)
                .Where(m => !m.Entrada && m.NomeMovimentacao.Categoria != null)
                .GroupBy(m => m.NomeMovimentacao.Categoria!.Nome)
                .ToDictionary(g => g.Key, g => g.Sum(m => m.Valor));

            // Média dos últimos 12 meses por categoria (incluindo zeros)
            var mediaUltimos12Meses = _movimentacaoRepository
                .GetMediaMensalPorCategoriaComZeros(user.Usuario_Id, referencia, 12);

            // Todas as categorias (combinando mês atual e histórico)
            var todasCategorias = movimentacoesMesAtual.Keys
                .Union(mediaUltimos12Meses.Keys)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            if (!todasCategorias.Any())
            {
                return Ok(new { message = "Não há dados suficientes para comparação." });
            }

            // Montar o DTO do gráfico
            var graficoDto = new GraficoDto
            {
                Type = "column",
                Labels = todasCategorias,
                Datasets = new List<DatasetDto>
                {
                    new DatasetDto
                    {
                        Name = "Mês Atual",
                        Data = todasCategorias.Select(cat =>
                            movimentacoesMesAtual.ContainsKey(cat) ? movimentacoesMesAtual[cat] : 0
                        ).ToList()
                    },
                    new DatasetDto
                    {
                        Name = "Média dos Últimos 12 Meses",
                        Data = todasCategorias.Select(cat =>
                            mediaUltimos12Meses.ContainsKey(cat) ? Math.Round(mediaUltimos12Meses[cat], 2) : 0
                        ).ToList()
                    }
                }
            };

            return Ok(graficoDto);
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex.Message);
        }
    }

    [Authorize, HttpGet("Analise/ComparacaoAnoAtual")]
    public IActionResult GetAnaliseComparacaoAnoAtual([FromQuery] DateTime mesReferencia)
    {
        try
        {
            var user = GetFromCurrentUser();
            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            var ano = mesReferencia.Year;
            var mes = mesReferencia.Month;

            // Buscar todas as movimentações do ano
            var dataFim = mesReferencia.Date;
            var dataInicio = dataFim.AddMonths(-12).AddDays(1);

            var movimentacoes = _movimentacaoRepository
                .GetByUsuarioIdAndRange(user.Usuario_Id, dataInicio, dataFim)
                .Where(m => !m.Entrada) // Apenas gastos
                .ToList();

            // Agrupar por mês
            var gastosPorMes = movimentacoes
                .GroupBy(m => m.Data.Month)
                .ToDictionary(g => g.Key, g => g.Sum(m => m.Valor));

            // Valor atual do mês de referência
            var valorAtual = gastosPorMes.ContainsKey(mes) ? gastosPorMes[mes] : 0;

            // Calcular média dos outros meses (excluindo o mês atual)
            var outrosMeses = gastosPorMes.Where(kvp => kvp.Key != mes).Select(kvp => kvp.Value).ToList();

            if (!outrosMeses.Any())
            {
                return Ok(new AnaliseFinanceiraDto
                {
                    Titulo = "Comparado ao Ano Atual",
                    ValorAtual = valorAtual,
                    VariacaoPercentual = 0,
                    Status = "normal",
                    Mensagem = "Não há dados suficientes para comparação"
                });
            }

            var mediaOutrosMeses = outrosMeses.Average();
            var variacaoPercentual = mediaOutrosMeses > 0
                ? ((valorAtual - mediaOutrosMeses) / mediaOutrosMeses) * 100
                : 0;

            var status = ClassificarStatus(variacaoPercentual);

            return Ok(new AnaliseFinanceiraDto
            {
                Titulo = "Comparado ao Ano Atual",
                ValorAtual = valorAtual,
                VariacaoPercentual = Math.Round(variacaoPercentual, 2),
                Status = status,
                MediaComparacao = Math.Round(mediaOutrosMeses, 2),
                Mensagem = GerarMensagemAnalise(variacaoPercentual, "utimos 12 meses")
            });
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex.Message);
        }
    }

    [Authorize, HttpGet("Analise/ComparacaoMesmoMesAnosAnteriores")]
    public IActionResult GetAnaliseComparacaoMesmoMesAnosAnteriores([FromQuery] DateTime mesReferencia)
    {
        try
        {
            var user = GetFromCurrentUser();
            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            var ano = mesReferencia.Year;
            var mes = mesReferencia.Month;

            // Buscar movimentações dos últimos 3 anos no mesmo mês
            var anosAnteriores = new List<int> { ano - 1, ano - 2, ano - 3 };
            var gastosAnosAnteriores = new List<decimal>();

            foreach (var anoAnterior in anosAnteriores)
            {
                var inicioMes = new DateTime(anoAnterior, mes, 1);
                var fimMes = inicioMes.AddMonths(1).AddDays(-1);

                var movimentacoes = _movimentacaoRepository
                    .GetByUsuarioIdAndRange(user.Usuario_Id, inicioMes, fimMes)
                    .Where(m => !m.Entrada)
                    .Sum(m => m.Valor);

                if (movimentacoes > 0)
                    gastosAnosAnteriores.Add(movimentacoes);
            }

            // Valor atual do mês de referência
            var inicioMesAtual = new DateTime(ano, mes, 1);
            var fimMesAtual = inicioMesAtual.AddMonths(1).AddDays(-1);

            var valorAtual = _movimentacaoRepository
                .GetByUsuarioIdAndRange(user.Usuario_Id, inicioMesAtual, fimMesAtual)
                .Where(m => !m.Entrada)
                .Sum(m => m.Valor);

            if (!gastosAnosAnteriores.Any())
            {
                return Ok(new AnaliseFinanceiraDto
                {
                    Titulo = $"Comparado ao mesmo mês em anos anteriores",
                    ValorAtual = valorAtual,
                    VariacaoPercentual = 0,
                    Status = "normal",
                    Mensagem = "Não há dados de anos anteriores para comparação"
                });
            }

            var mediaAnosAnteriores = gastosAnosAnteriores.Average();
            var variacaoPercentual = mediaAnosAnteriores > 0
                ? ((valorAtual - mediaAnosAnteriores) / mediaAnosAnteriores) * 100
                : 0;

            var status = ClassificarStatus(variacaoPercentual);

            return Ok(new AnaliseFinanceiraDto
            {
                Titulo = $"Comparado ao mesmo mês em anos anteriores",
                ValorAtual = valorAtual,
                VariacaoPercentual = Math.Round(variacaoPercentual, 2),
                Status = status,
                MediaComparacao = Math.Round(mediaAnosAnteriores, 2),
                Mensagem = GerarMensagemAnalise(variacaoPercentual, "anos anteriores")
            });
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex.Message);
        }
    }

    [Authorize, HttpGet("Analise/ComparacaoCategoriaMesmoAno")]
    public IActionResult GetAnaliseComparacaoCategoriaMesmoAno([FromQuery] DateTime mesReferencia, [FromQuery] Guid categoriaId)
    {
        try
        {
            var user = GetFromCurrentUser();
            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            var ano = mesReferencia.Year;
            var mes = mesReferencia.Month;

            // Buscar todas as movimentações da categoria no ano
            var inicioAno = new DateTime(ano, 1, 1);
            var fimAno = new DateTime(ano, 12, 31);

            var movimentacoes = _movimentacaoRepository
                .GetByUsuarioIdAndRange(user.Usuario_Id, inicioAno, fimAno)
                .Where(m => !m.Entrada && m.NomeMovimentacao.Categoria_Id == categoriaId)
                .ToList();

            if (!movimentacoes.Any())
            {
                return Ok(new AnaliseFinanceiraDto
                {
                    Titulo = "Comparado à categoria no ano",
                    ValorAtual = 0,
                    VariacaoPercentual = 0,
                    Status = "normal",
                    Mensagem = "Não há movimentações para esta categoria"
                });
            }

            // Agrupar por mês
            var gastosPorMes = movimentacoes
                .GroupBy(m => m.Data.Month)
                .ToDictionary(g => g.Key, g => g.Sum(m => m.Valor));

            // Valor atual do mês de referência
            var valorAtual = gastosPorMes.ContainsKey(mes) ? gastosPorMes[mes] : 0;

            // Calcular média dos outros meses (excluindo o mês atual)
            var outrosMeses = gastosPorMes.Where(kvp => kvp.Key != mes).Select(kvp => kvp.Value).ToList();

            if (!outrosMeses.Any())
            {
                return Ok(new AnaliseFinanceiraDto
                {
                    Titulo = "Comparado à categoria no ano",
                    ValorAtual = valorAtual,
                    VariacaoPercentual = 0,
                    Status = "normal",
                    Mensagem = "Não há dados suficientes para comparação da categoria"
                });
            }

            var mediaOutrosMeses = outrosMeses.Average();
            var variacaoPercentual = mediaOutrosMeses > 0
                ? ((valorAtual - mediaOutrosMeses) / mediaOutrosMeses) * 100
                : 0;

            var status = ClassificarStatus(variacaoPercentual);

            // Buscar nome da categoria para exibição
            var nomeCategoria = movimentacoes.FirstOrDefault()?.NomeMovimentacao?.Categoria?.Nome ?? "Categoria";

            return Ok(new AnaliseFinanceiraDto
            {
                Titulo = $"Categoria '{nomeCategoria}' comparada ao ano",
                ValorAtual = valorAtual,
                VariacaoPercentual = Math.Round(variacaoPercentual, 2),
                Status = status,
                MediaComparacao = Math.Round(mediaOutrosMeses, 2),
                Mensagem = GerarMensagemAnalise(variacaoPercentual, "outros meses da categoria")
            });
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex.Message);
        }
    }

    [Authorize, HttpGet("Analise/ResumoCompleto")]
    public IActionResult GetAnaliseResumoCompleto([FromQuery] DateTime mesReferencia, [FromQuery] Guid? categoriaId = null)
    {
        try
        {
            var user = GetFromCurrentUser();
            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            var analises = new List<AnaliseFinanceiraDto>();

            // 1. Comparação com o ano atual
            var analiseAnoAtual = GetAnaliseComparacaoAnoAtual(mesReferencia);
            if (analiseAnoAtual is OkObjectResult okResult1)
                analises.Add((AnaliseFinanceiraDto)okResult1.Value);

            // 2. Comparação com anos anteriores
            var analiseAnosAnteriores = GetAnaliseComparacaoMesmoMesAnosAnteriores(mesReferencia);
            if (analiseAnosAnteriores is OkObjectResult okResult2)
                analises.Add((AnaliseFinanceiraDto)okResult2.Value);

            // 3. Comparação por categoria (se especificada)
            if (categoriaId.HasValue)
            {
                var analiseCategoria = GetAnaliseComparacaoCategoriaMesmoAno(mesReferencia, categoriaId.Value);
                if (analiseCategoria is OkObjectResult okResult3)
                    analises.Add((AnaliseFinanceiraDto)okResult3.Value);
            }

            return Ok(new
            {
                MesReferencia = mesReferencia.ToString("MMMM yyyy"),
                Analises = analises,
                ResumoGeral = GerarResumoGeral(analises)
            });
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex.Message);
        }
    }

    // Métodos auxiliares privados
    private string ClassificarStatus(decimal variacaoPercentual)
    {
        if (variacaoPercentual >= -15 && variacaoPercentual <= 15)
            return "normal";
        else if ((variacaoPercentual >= -30 && variacaoPercentual < -15) ||
                 (variacaoPercentual > 15 && variacaoPercentual <= 30))
            return "atencao";
        else
            return "alerta";
    }

    private string GerarMensagemAnalise(decimal variacao, string contexto)
    {
        if (variacao > 30)
            return $"Gastou bem mais do que {contexto} (+{variacao:F1}%)";
        else if (variacao > 15)
            return $"Gastou mais do que {contexto} (+{variacao:F1}%)";
        else if (variacao >= -15)
            return $"Seu gasto está dentro do esperado comparado a {contexto} ({variacao:F1}%)";
        else if (variacao >= -30)
            return $"Gastou menos do que {contexto} ({variacao:F1}%)";
        else
            return $"Gastou bem menos do que {contexto} ({variacao:F1}%)";
    }

    private string GerarResumoGeral(List<AnaliseFinanceiraDto> analises)
    {
        var alertas = analises.Count(a => a.Status == "alerta");
        var atencoes = analises.Count(a => a.Status == "atencao");

        if (alertas > 0)
            return $"⚠️ {alertas} análise(s) em alerta - Requer atenção imediata";
        else if (atencoes > 0)
            return $"🟡 {atencoes} análise(s) em atenção - Monitorar de perto";
        else
            return "✅ Gastos dentro do padrão esperado";
    }

}
