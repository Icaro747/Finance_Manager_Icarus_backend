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
public class CategoriaController : CrudController<
    Categoria,
    CategoriaRepository,
    CriarCategoriaDto,
    AtualizarCategoriaDto,
    ListarCategoriaDto>
{
    private readonly CategoriaRepository _categoriaRepository;

    public CategoriaController(
        CategoriaRepository repository,
        IMapper mapper,
        UsuarioRepository usuarioRepository
        ) : base(repository, mapper, usuarioRepository)
    {
        _categoriaRepository = repository;
    }

    [Authorize, HttpGet("My")]
    public IActionResult GetMyCategoria()
    {
        try
        {
            var user = GetFromCurrentUser();
            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            var lista = _categoriaRepository.GetByUsuarioId(user.Usuario_Id);
            var dto = lista
                .Select(x => new GetGenericOptionsDto 
                { 
                    Label = x.Nome,
                    Value = x.Categoria_Id
                })
                .ToList();

            return Ok(dto);
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex);
        }
    }

    protected override void BeforeCreate(Categoria categoria, CriarCategoriaDto dto)
    {
        var user = GetFromCurrentUser() ?? 
            throw new InvalidOperationException("Usuário não encontrado.");
        
        categoria.Usuario_Id = user.Usuario_Id;
    }

    protected override Guid GetEntityId(Categoria entity)
    {
        return entity.Categoria_Id;
    }

    protected override void SetEntityId(Categoria entity, Guid id)
    {
        entity.Categoria_Id = id;
    }
}
