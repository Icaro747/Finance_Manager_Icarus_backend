using AutoMapper;
using Finance_Manager_Icarus.Dtos;
using Finance_Manager_Icarus.Models;

namespace Finance_Manager_Icarus.Mapping.Profiles;

public class CategoriaProfiles : Profile
{
    public CategoriaProfiles()
    {
        CreateMap<CriarCategoriaDto, Categoria>();
        CreateMap<AtualizarCategoriaDto, Categoria>();
        CreateMap<Categoria, ListarCategoriaDto>();
    }
}
