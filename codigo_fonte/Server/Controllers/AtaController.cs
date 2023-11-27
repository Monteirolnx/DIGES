namespace Controle.Atividades.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AtaController(IAtaServico ataServico) : ControllerBase
{
    [HttpGet("v1/gerar")]
    public async Task<IActionResult> Gerar()
    {
        var resultado = await ataServico.Gerar();

        return Ok(resultado);
    }
}