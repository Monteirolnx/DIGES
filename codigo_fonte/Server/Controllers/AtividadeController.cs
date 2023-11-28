namespace Controle.Atividades.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AtividadeController(IAtividadeServico atividadeServico) : ControllerBase
{
    [HttpPost("v1/adicionar-atividade")]
    public async Task<IActionResult> AdicionarAtividade(AtividadeDto atividadeDto)
    {
        var resultado = await atividadeServico.Adicionar(atividadeDto);

        return Ok(resultado);
    }

    [HttpGet("v1/consulta-atividades")]
    public async Task<IActionResult> ConsultaAtividades()
    {
        var resultado = await atividadeServico.ConsultaTodas();

        return Ok(resultado);
    }

    [HttpGet("v1/consulta-codigo/{codigoAtividade}")]
    public async Task<IActionResult> ConsultarPorCodigo(Guid codigoAtividade)
    {
        var resultado = await atividadeServico.ConsultarPorCodigo(codigoAtividade);

        return Ok(resultado);
    }
}