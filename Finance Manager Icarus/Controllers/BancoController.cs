using AutoMapper;
using Finance_Manager_Icarus.Controllers.Base;
using Finance_Manager_Icarus.Dtos;
using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Finance_Manager_Icarus.Controllers;

[ApiController]
[Route("[controller]")]
public class BancoController : CrudController<
    Banco, BancoRepository, CriarBancoDto, AtualizarBancoDto, ListarBancoDto>
{
    public BancoController(BancoRepository bancoRepository, IMapper mapper) 
    : base(bancoRepository, mapper)
    {
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