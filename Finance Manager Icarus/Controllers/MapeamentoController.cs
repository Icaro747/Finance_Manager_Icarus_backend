using AutoMapper;
using Finance_Manager_Icarus.Controllers.Base;
using Finance_Manager_Icarus.Dtos;
using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Finance_Manager_Icarus.Controllers;

[ApiController]
[Route("[controller]")]
public class MapeamentoController : CrudController<
    Mapeamento,
    MapeamentoRepository,
    CriarMapeamentoDto,
    AtualizarMapeamentoDto,
    ListarMapeamentoDto>
{
    private readonly MapeamentoRepository _mapeamentoRepository;

    public MapeamentoController(
        MapeamentoRepository repository,
        IMapper mapper,
        UsuarioRepository usuarioRepository
    ) : base(repository, mapper, usuarioRepository)
    {
        _mapeamentoRepository = repository;
    }

    protected override void BeforeCreate(Mapeamento entity, CriarMapeamentoDto createDto)
    {
        var user = GetFromCurrentUser() ??
            throw new InvalidOperationException("Usuário não encontrado.");

        entity.Usuario_Id = user.Usuario_Id;
    }

    protected override Guid GetEntityId(Mapeamento entity)
    {
        return entity.Mapeamento_Id;
    }

    protected override void SetEntityId(Mapeamento entity, Guid id)
    {
        entity.Mapeamento_Id = id;
    }
}
