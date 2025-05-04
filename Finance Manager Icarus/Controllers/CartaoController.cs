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
public class CartaoController : CrudController<
        Cartao,
        CartaoRepository,
        CriarCartaoDto,
        AtualizarCartaoDto,
        ListarCartaoDto>
{
    private readonly CartaoRepository _cartaoRepository;

    public CartaoController(
        CartaoRepository repository,
        IMapper mapper,
        UsuarioRepository usuarioRepository
    ) : base(repository, mapper, usuarioRepository)
    {
        _cartaoRepository = repository;
    }

    [Authorize, HttpGet("ByBancoIdAndUsuarioId")]
    public IActionResult GetByBancoIdAndUsuarioId([FromQuery] Guid bancoId)
    {
        try
        {
            var user = GetFromCurrentUser();
            if (user == null) return Unauthorized("Usuário não encontrado.");

            var cartoes = _cartaoRepository.GetByBancoIdAndUsuarioId(bancoId, user.Usuario_Id);
            var dtos = _mapper.Map<List<ListarCartaoDto>>(cartoes);

            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex);
        }
    }

    [Authorize, HttpGet("My")]
    public IActionResult GetMyBancos()
    {
        try
        {
            var user = GetFromCurrentUser();
            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            var lista = _cartaoRepository.GetByUsuarioId(user.Usuario_Id);
            var dto = lista
                .Select(x => new GetGenericOptionsDto { Label = x.Nome, Value = x.Cartao_Id })
                .ToList();

            return Ok(dto);
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex);
        }
    }

    protected override Guid GetEntityId(Cartao entity)
    {
        return entity.Cartao_Id;
    }

    protected override void SetEntityId(Cartao entity, Guid id)
    {
        entity.Cartao_Id = id;
    }
}
