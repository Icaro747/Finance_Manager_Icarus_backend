using AutoMapper;
using Finance_Manager_Icarus.Dtos;
using Finance_Manager_Icarus.Models;

namespace Finance_Manager_Icarus.Mapping.Profiles;

public class MapeamentoProfiles : Profile
{
    public MapeamentoProfiles()
    {
        CreateMap<CriarMapeamentoDto, Mapeamento>();
        CreateMap<AtualizarMapeamentoDto, Mapeamento>();
        CreateMap<Mapeamento, ListarMapeamentoDto>();
    }
}
