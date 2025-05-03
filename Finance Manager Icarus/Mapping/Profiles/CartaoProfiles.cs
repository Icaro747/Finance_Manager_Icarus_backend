using AutoMapper;
using Finance_Manager_Icarus.Dtos;
using Finance_Manager_Icarus.Models;

namespace Finance_Manager_Icarus.Mapping.Profiles;

public class CartaoProfiles : Profile
{
    public CartaoProfiles()
    {
        CreateMap<CriarCartaoDto, Cartao>();
        CreateMap<AtualizarCartaoDto, Cartao>();
        CreateMap<Cartao, ListarCartaoDto>();
    }
}