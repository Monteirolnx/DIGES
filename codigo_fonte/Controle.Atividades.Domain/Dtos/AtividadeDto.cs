namespace Controle.Atividades.Domain.Dtos;

public class AtividadeDto
{
    public Guid Codigo { get; set; }

    public int? NumeroRedmine { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public string Sistema { get; set; } = string.Empty;

    public Guid? CodigoAnalista { get; set; }

    public AnalistaDto? Analista { get; set; } = new();

    public Guid? CodigoLider { get; set; }

    public LiderDto? Lider { get; set; } = new();

    public ICollection<ObservacaoDto>? Historico { get; set; } = new List<ObservacaoDto>();

    public DateTime DtCriacao { get; set; }

    public DateTime DtModificacao { get; set; }

    public DateTime DtFechamento { get; set; }

    public TipoAbertaFechada Status { get; set; }
}