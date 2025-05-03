using AutoMapper;
using Finance_Manager_Icarus.Dtos;
using Finance_Manager_Icarus.Models;

namespace Finance_Manager_Icarus.Mapping.Profiles;

public class BancoProfiles : Profile
{
    public BancoProfiles()
    {
        CreateMap<CriarBancoDto, Banco>();
        CreateMap<AtualizarBancoDto, Banco>();
        CreateMap<Banco, ListarBancoDto>();
    }
}
