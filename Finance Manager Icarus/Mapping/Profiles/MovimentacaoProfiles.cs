using AutoMapper;
using Finance_Manager_Icarus.Dtos;
using Finance_Manager_Icarus.Models;

namespace Finance_Manager_Icarus.Mapping.Profiles;

public class MovimentacaoProfiles : Profile
{
    public MovimentacaoProfiles()
    {
        CreateMap<CriarMovimentacaoDto, Movimentacao>()
            .ForMember(dest => dest.NomeMovimentacao, opt => opt.Ignore())
            .ForMember(dest => dest.TipoMovimentacao, opt => opt.Ignore());
        CreateMap<AtualizarMovimentacaoDto, Movimentacao>();
        CreateMap<Movimentacao, ListarMovimentacaoDto>();
    }
}