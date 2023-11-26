namespace Controle.Atividades.Domain.Entidades;

[Table("observacao")]
public class Observacao
{
    [Key]
    [Column("cd_observacao")]
    public Guid Codigo { get; set; }

    [Column("data")]
    public DateTime Data { get; set; }

    [Column("registro")]
    public string Registro { get; set; } = string.Empty;

    [Column("cd_atividade")]
    public Guid CodigoAtividade { get; set; }

    [ForeignKey(nameof(CodigoAtividade))]
    public Atividade Atividade { get; set; } = new();
}