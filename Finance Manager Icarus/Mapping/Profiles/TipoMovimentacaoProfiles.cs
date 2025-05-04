using AutoMapper;
using Finance_Manager_Icarus.Dtos;
using Finance_Manager_Icarus.Models;

namespace Finance_Manager_Icarus.Mapping.Profiles;

public class TipoMovimentacaoProfiles : Profile
{
    public TipoMovimentacaoProfiles()
    {
        CreateMap<CriarTipoMovimentacaoDto, TipoMovimentacao>();
        CreateMap<AtualizarTipoMovimentacaoDto, TipoMovimentacao>();
        CreateMap<TipoMovimentacao, ListarTipoMovimentacaoDto>();
    }
}