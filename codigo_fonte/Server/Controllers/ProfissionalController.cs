namespace Controle.Atividades.Server.Controllers;

[ApiController]
[Route(Constantes.BaseUrlProfissional)]
public class ProfissionalController(IProfissionalServico profissionalServico) : ControllerBase
{
    [HttpGet (Constantes.ConsultaTodosAnalistas)]
    public async Task<IActionResult> ConsultaTodosAnalistas()
    {
        var resultado = await profissionalServico.ConsultaTodosAnalistas();

        return Ok(resultado);
    }

    [HttpGet(Constantes.ConsultaTodosLideres)]
    public async Task<IActionResult> ConsultaTodosLideres()
    {
        var resultado = await profissionalServico.ConsultaTodosLideres();

        return Ok(resultado);
    }
}