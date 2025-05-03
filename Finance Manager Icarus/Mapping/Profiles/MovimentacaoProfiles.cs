using AutoMapper;
using Finance_Manager_Icarus.Dtos;
using Finance_Manager_Icarus.Models;

namespace Finance_Manager_Icarus.Mapping.Profiles;

public class MovimentacaoProfiles : Profile
{
    public MovimentacaoProfiles()
    {
        CreateMap<CriarMovimentacaoDto, Movimentacao>();
        CreateMap<AtualizarMovimentacaoDto, Movimentacao>();
        CreateMap<Movimentacao, ListarMovimentacaoDto>();
    }
}