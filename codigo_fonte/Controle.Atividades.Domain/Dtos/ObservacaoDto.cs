namespace Controle.Atividades.Domain.Dtos;

public class ObservacaoDto
{
    public Guid Codigo { get; set; }

    public DateTime Data { get; set; }

    public string Registro { get; set; } = string.Empty;
}