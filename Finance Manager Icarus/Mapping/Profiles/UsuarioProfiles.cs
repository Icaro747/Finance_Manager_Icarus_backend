using AutoMapper;
using Finance_Manager_Icarus.Dtos;
using Finance_Manager_Icarus.Models;

namespace Finance_Manager_Icarus.Mapping.Profiles;

public class UsuarioProfiles : Profile
{
    public UsuarioProfiles()
    {
        CreateMap<CriarUsuarioDto, Usuario>();
        CreateMap<AtualizarUsuarioDto, Usuario>();
        CreateMap<Usuario, ListarUsuarioDto>();
    }
}
