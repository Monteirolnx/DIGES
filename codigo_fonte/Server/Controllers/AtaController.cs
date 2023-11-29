namespace Controle.Atividades.Server.Controllers;

[ApiController]
[Route(Constantes.BaseUrlAta)]
public class AtaController(IAtaServico ataServico) : ControllerBase
{
    [HttpGet(Constantes.GeraAta)]
    public async Task<IActionResult> Gerar()
    {
        var resultado = await ataServico.Gerar();

        return Ok(resultado);
    }
}