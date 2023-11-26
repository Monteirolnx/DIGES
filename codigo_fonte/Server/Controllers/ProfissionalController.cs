namespace Controle.Atividades.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfissionalController(IProfissionalServico profissionalServico) : ControllerBase
{
    [HttpGet ("v1/consulta-todos-analistas")]
    public async Task<IActionResult> ConsultaTodosAnalistas()
    {
        var resultado = await profissionalServico.ConsultaTodosAnalistas();

        return Ok(resultado);
    }

    [HttpGet("v1/consulta-todos-lideres")]
    public async Task<IActionResult> ConsultaTodosLideres()
    {
        var resultado = await profissionalServico.ConsultaTodosLideres();

        return Ok(resultado);
    }
}