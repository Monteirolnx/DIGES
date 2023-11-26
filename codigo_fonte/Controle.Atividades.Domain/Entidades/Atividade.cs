namespace Controle.Atividades.Domain.Entidades;

[Table("atividade")]
public class Atividade
{
    [Key]
    [Column("cd_atividade")]
    public Guid Codigo { get; set; }

    [Column("nm_redmine")]
    public int? NumeroRedmine { get; set; }
    
    [Column("descricao")]
    public string Descricao { get; set; } = string.Empty;

    [Column("sistema")]
    public string Sistema { get; set; } = string.Empty;

    [Column("codigo_analista")]
    public Guid? CodigoAnalista { get; set; }
    
    [ForeignKey(nameof(CodigoAnalista))]
    public Analista? Analista { get; set; } 

    [Column("codigo_lider")]
    public Guid? CodigoLider{ get; set; }

    [ForeignKey(nameof(CodigoLider))]
    public Lider? Lider { get; set; }

    public ICollection<Observacao>? Historico { get; set; } = new List<Observacao>();

    [Column("dt_criacao")]
    public DateTime DtCriacao { get; set; }

    [Column("dt_modificacao")]
    public DateTime DtModificacao { get; set; }

    [Column("dt_fechamento")]
    public DateTime DtFechamento { get; set; }

    [Column("status")]
    public TipoAbertaFechada Status { get; set; }
}