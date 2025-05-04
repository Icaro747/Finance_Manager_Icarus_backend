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
public class BancoController : CrudController<
    Banco,
    BancoRepository,
    CriarBancoDto,
    AtualizarBancoDto,
    ListarBancoDto>
{
    private readonly BancoRepository _bancoRepository;

    public BancoController(
        BancoRepository bancoRepository,
        IMapper mapper,
        UsuarioRepository usuarioRepository
    ) : base(bancoRepository, mapper, usuarioRepository)
    {
        _bancoRepository = bancoRepository;
    }

    [Authorize, HttpPost]
    public override async Task<IActionResult> Create([FromBody] CriarBancoDto dto)
    {
        try
        {
            var user = GetFromCurrentUser();
            if(user == null)
                return Unauthorized("Usuário não encontrado.");

            var newBanco = _mapper.Map<Banco>(dto);
            newBanco.Usuario_Id = user.Usuario_Id;

            _bancoRepository.Add(newBanco);
            await _bancoRepository.SaveAllAsync();

            return Ok();
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

            var lista = _bancoRepository.GetByUsuarioId(user.Usuario_Id);
            var dto = lista
                .Select(x => new GetGenericOptionsDto { Label = x.Nome, Value = x.Banco_Id })
                .ToList();

            return Ok(dto);
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex);
        }
    }

    protected override Guid GetEntityId(Banco entity)
    {
        return entity.Banco_Id;
    }

    protected override void SetEntityId(Banco entity, Guid id)
    {
        entity.Banco_Id = id;
    }
}