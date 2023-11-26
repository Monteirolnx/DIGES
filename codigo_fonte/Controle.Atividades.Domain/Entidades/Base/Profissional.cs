namespace Controle.Atividades.Domain.Entidades.Base;

public abstract class Profissional
{
    [Key]
    [Column("codigo")]
    public Guid Codigo { get; set; }

    [Column("nome")]
    public string Nome { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("ativo")]
    public TipoAtivoInativo Status { get; set; }
}