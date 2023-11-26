namespace Controle.Atividades.Domain.Mappings;

[UsedImplicitly]
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Analista, AnalistaDto>().ReverseMap();
        CreateMap<Lider, LiderDto>().ReverseMap();
        CreateMap<Atividade, AtividadeDto>().ReverseMap();
        CreateMap<Observacao, ObservacaoDto>().ReverseMap();
    }
}