using AutoMapper;
using Finance_Manager_Icarus.Dtos;
using Finance_Manager_Icarus.Models;

namespace Finance_Manager_Icarus.Mapping.Profiles;

public class ItensMapeamentoProfiles : Profile
{
    public ItensMapeamentoProfiles()
    {
        CreateMap<CriarItensMapeamentoDto, ItensMapeamento>();
        CreateMap<AtualizarItensMapeamentoDto, ItensMapeamento>();
        CreateMap<ItensMapeamento, ListarItensMapeamentoDto>();
    }
}