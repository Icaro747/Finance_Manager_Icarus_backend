using AutoMapper;
using Finance_Manager_Icarus.Dtos;
using Finance_Manager_Icarus.Models;

namespace Finance_Manager_Icarus.Mapping.Profiles;

public class NomeMovimentacaoProfiles : Profile
{
    public NomeMovimentacaoProfiles()
    {
        CreateMap<CriarNomeMovimentacaoDto, NomeMovimentacao>();
        CreateMap<AtualizarNomeMovimentacaoDto, NomeMovimentacao>();
        CreateMap<NomeMovimentacao, ListarNomeMovimentacaoDto>();
    }
}