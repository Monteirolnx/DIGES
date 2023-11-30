namespace Controle.Atividades.Domain.Entidades.Base;

public class ProfissionalDto
{
    public Guid Codigo { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public TipoAtivoInativo Status { get; set; }
}