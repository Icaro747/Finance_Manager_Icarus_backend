using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Finance_Manager_Icarus.Controllers.Base;

public class ControllerFinanceManagerIcarusData : ControllerBase
{
    private readonly UsuarioRepository _usuarioRepository;

    public ControllerFinanceManagerIcarusData(UsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    /// <summary>
    /// Busca os dados do usuário atual.
    /// </summary>
    protected Usuario? GetFromCurrentUser()
    {
        try
        {
            var sid = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
            if (Guid.TryParse(sid, out Guid userId))
                return _usuarioRepository.GetById(userId);
            else
                return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
