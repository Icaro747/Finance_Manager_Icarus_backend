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
public class NomeMovimentacaoController : CrudController<
    NomeMovimentacao,
    NomeMovimentacaoRepository,
    CriarNomeMovimentacaoDto,
    AtualizarNomeMovimentacaoDto,
    ListarNomeMovimentacaoDto>
{
    private readonly NomeMovimentacaoRepository _nomeMovimentacaoRepository;

    public NomeMovimentacaoController(
        NomeMovimentacaoRepository repository,
        IMapper mapper,
        UsuarioRepository usuarioRepository
    ) : base(repository, mapper, usuarioRepository)
    {
        _nomeMovimentacaoRepository = repository;
    }

    [Authorize, HttpGet("My")]
    public IActionResult GetMyNomeMovimentacao()
    {
        try
        {
            var user = GetFromCurrentUser();
            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            var lista = _nomeMovimentacaoRepository.GetByUsuarioId(user.Usuario_Id);
            var dto = _mapper.Map<List<ListarNomeMovimentacaoAllDataDto>>(lista);

            return Ok(dto);
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex);
        }
    }

    protected override Guid GetEntityId(NomeMovimentacao entity)
    {
        return entity.Nome_Movimentacao_Id;
    }

    protected override void SetEntityId(NomeMovimentacao entity, Guid id)
    {
        entity.Nome_Movimentacao_Id = id;
    }
}
