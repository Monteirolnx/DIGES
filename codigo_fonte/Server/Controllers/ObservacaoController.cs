namespace Controle.Atividades.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ObservacaoController(IObservacaoServico observacaoServico) : ControllerBase
{

    [HttpPost("v1/editar-observacao")]
    public async Task<IActionResult> EditarObservacao(ObservacaoDto observacaoDto)
    {
        var resultado = await observacaoServico.Editar(observacaoDto);

        return Ok(resultado);
    }
}