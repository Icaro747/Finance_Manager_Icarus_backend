using AutoMapper;
using Finance_Manager_Icarus.Controllers.Base;
using Finance_Manager_Icarus.Dtos;
using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Finance_Manager_Icarus.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : CrudController<
    Usuario, UsuarioRepository, CriarUsuarioDto, AtualizarUsuarioDto, ListarUsuarioDto>
{
    public UsuarioController(
        UsuarioRepository repository,
        IMapper mapper
    ) : base(repository, mapper, repository)
    { }

    protected override Guid GetEntityId(Usuario entity)
    {
        return entity.Usuario_Id;
    }

    protected override void SetEntityId(Usuario entity, Guid id)
    {
        entity.Usuario_Id = id;
    }

    protected override void BeforeCreate(Usuario user, CriarUsuarioDto dto)
    {
        user.HashPassword(dto.Senha);
    }
}
