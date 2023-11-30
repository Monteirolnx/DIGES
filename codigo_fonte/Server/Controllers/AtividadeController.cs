namespace Controle.Atividades.Server.Controllers;

[ApiController]
[Route(Constantes.BaseUrlAtividade)]
public class AtividadeController(IAtividadeServico atividadeServico) : ControllerBase
{
    [HttpPost(Constantes.AdicionaAtividade)]
    public async Task<IActionResult> Adicionar(AtividadeDto atividadeDto)
    {
        var resultado = await atividadeServico.Adicionar(atividadeDto);

        return Ok(resultado);
    }

    [HttpPost(Constantes.EditaAtividade)]
    public async Task<IActionResult> Editar(AtividadeDto atividadeDto)
    {
        var resultado = await atividadeServico.Editar(atividadeDto);

        return Ok(resultado);
    }

    [HttpPost(Constantes.ExcluiAtividade)]
    public async Task<IActionResult> Excluir(AtividadeDto atividadeDto)
    {
        var resultado = await atividadeServico.Excluir(atividadeDto);

        return Ok(resultado);
    }

    [HttpGet(Constantes.ConsultaTodasAtividades)]
    public async Task<IActionResult> ConsultarTodas()
    {
        var resultado = await atividadeServico.ConsultarTodas();

        return Ok(resultado);
    }

    [HttpGet(Constantes.ConsultaPorCodigoAtividade + "{codigoAtividade}")]
    public async Task<IActionResult> ConsultarPorCodigo(Guid codigoAtividade)
    {
        var resultado = await atividadeServico.ConsultarPorCodigo(codigoAtividade);

        return Ok(resultado);
    }

    [HttpPost(Constantes.FinalizaAtividade)]
    public async Task<IActionResult> Finalizar(Guid codigoAtividade)
    {
        var resultado = await atividadeServico.Finalizar(codigoAtividade);

        return Ok(resultado);
    }

    [HttpPost(Constantes.ReabreAtividade)]
    public async Task<IActionResult> Reabrir(Guid codigoAtividade)
    {
        var resultado = await atividadeServico.Reabrir(codigoAtividade);

        return Ok(resultado);
    }
}