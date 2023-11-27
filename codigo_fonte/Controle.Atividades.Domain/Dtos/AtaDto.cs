namespace Controle.Atividades.Domain.Dtos;

public class AtaDto
{
    public string AtividadesCriadas { get; set; } = string.Empty;

    public string AtividadesAtualizadas { get; set; } = string.Empty;

    public string AtividadesFinalizadas { get; set; } = string.Empty;

    public IEnumerable<AtividadeDto>? AtividadesDto { get; set; }
}