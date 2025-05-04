using AutoMapper;
using Finance_Manager_Icarus.Controllers.Base;
using Finance_Manager_Icarus.Dtos;
using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance_Manager_Icarus.Controllers;

[ApiController]
[Route("[controller]")]
public class MovimentacaoController : CrudController<
        Movimentacao,
        MovimentacaoRepository,
        CriarMovimentacaoDto,
        AtualizarMovimentacaoDto,
        ListarMovimentacaoDto>
{
    private readonly MovimentacaoRepository _movimentacaoRepository;
    private readonly TipoMovimentacaoRepository _tipoMovimentacaoRepository;
    private readonly NomeMovimentacaoRepository _nomeMovimentacaoRepository;

    public MovimentacaoController(
        MovimentacaoRepository repository,
        IMapper mapper,
        UsuarioRepository usuarioRepository,
        TipoMovimentacaoRepository tipoMovimentacaoRepository,
        NomeMovimentacaoRepository nomeMovimentacaoRepository
    ) : base(repository, mapper, usuarioRepository)
    {
        _movimentacaoRepository = repository;
        _tipoMovimentacaoRepository = tipoMovimentacaoRepository;
        _nomeMovimentacaoRepository = nomeMovimentacaoRepository;
    }

    [Authorize, HttpPost]
    public override async Task<IActionResult> Create([FromBody] CriarMovimentacaoDto createDto)
    {
        try
        {
            var user = GetFromCurrentUser();
            if (user == null) return Unauthorized("Usuário não encontrado.");

            var movimentacao = _mapper.Map<Movimentacao>(createDto);

            if (!movimentacao.PossuiFonteValida())
                return UnprocessableEntity("É necessário informar pelo menos um dos IDs: Cartao_Id ou Banco_Id.");

            var tm = _tipoMovimentacaoRepository.GetByNomeAndUsuarioId(createDto.TipoMovimentacao, user.Usuario_Id);
            tm ??= new TipoMovimentacao
            { 
                    Nome = createDto.TipoMovimentacao,
                    Usuario_Id = user.Usuario_Id
            };

            movimentacao.TipoMovimentacao = tm;

            var nm = _nomeMovimentacaoRepository.GetByNomeAndUsuarioId(createDto.NomeMovimentacao, user.Usuario_Id);
            nm ??= new NomeMovimentacao
            { 
                Nome = createDto.NomeMovimentacao,
                Usuario_Id = user.Usuario_Id 
            };

            movimentacao.NomeMovimentacao = nm;

            _movimentacaoRepository.Add(movimentacao);
            await _movimentacaoRepository.SaveAllAsync();

            var dto = _mapper.Map<ListarMovimentacaoDto>(movimentacao);
            return CreatedAtAction(nameof(GetById), new { id = movimentacao.Movimentacao_Id }, dto);
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex);
        }
    }

    [Authorize, HttpGet("ByNome")]
    public IActionResult GetMyMovimentacao([FromQuery] Guid nome_id)
    {
        try
        {
            var user = GetFromCurrentUser();
            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            var lista = _movimentacaoRepository.GetByNomeIdAndUsuarioId(nome_id, user.Usuario_Id);
            var dto = _mapper.Map<List<ListarMovimentacaoDto>>(lista);

            return Ok(dto);
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex);
        }
    }

    [Authorize, HttpGet("My")]
    public IActionResult GetMyMovimentacao()
    {
        try
        {
            var user = GetFromCurrentUser();
            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            var lista = _movimentacaoRepository.GetByUsuarioId(user.Usuario_Id);
            var dto = _mapper.Map<List<ListarMovimentacaoAllDataDto>>(lista);

            return Ok(dto);
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex);
        }
    }

    protected override Guid GetEntityId(Movimentacao entity)
    {
        return entity.Movimentacao_Id;
    }

    protected override void SetEntityId(Movimentacao entity, Guid id)
    {
        entity.Movimentacao_Id = id;
    }
}
