namespace Controle.Atividades.Server.Controllers;

[ApiController]
[Route(Constantes.BaseUrlAtividade)]
public class AtividadeController(IAtividadeServico atividadeServico, IMemoryCache memoryCache) : ControllerBase
{
    [HttpPost(Constantes.AdicionaAtividade)]
    public async Task<IActionResult> Adicionar(AtividadeDto atividadeDto)
    {
        var resultado = await atividadeServico.Adicionar(atividadeDto);

        memoryCache.Remove(Constantes.MemoryCacheTodasAtividades);

        return Ok(resultado);
    }

    [HttpPost(Constantes.EditaAtividade)]
    public async Task<IActionResult> Editar(AtividadeDto atividadeDto)
    {
        var resultado = await atividadeServico.Editar(atividadeDto);

        memoryCache.Remove(Constantes.MemoryCacheTodasAtividades);

        return Ok(resultado);
    }

    [HttpPost(Constantes.ExcluiAtividade)]
    public async Task<IActionResult> Excluir(AtividadeDto atividadeDto)
    {
        var resultado = await atividadeServico.Excluir(atividadeDto);

        memoryCache.Remove(Constantes.MemoryCacheTodasAtividades);

        return Ok(resultado);
    }

    [HttpGet(Constantes.ConsultaTodasAtividades)]
    public async Task<IActionResult> ConsultarTodas()
    {
        if (memoryCache.TryGetValue(Constantes.MemoryCacheTodasAtividades, out var resultado))
        {
            return Ok(resultado);
        }

        resultado = await atividadeServico.ConsultarTodas();

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromHours(5));
        memoryCache.Set(Constantes.MemoryCacheTodasAtividades, resultado, cacheEntryOptions);

        return Ok(resultado);
    }

    [HttpGet(Constantes.ConsultaPorCodigoAtividade + "{codigoAtividade}")]
    public async Task<IActionResult> ConsultarPorCodigo(Guid codigoAtividade)
    {
        var resultado = await atividadeServico.ConsultarPorCodigo(codigoAtividade);

        return Ok(resultado);
    }

    [HttpPost(Constantes.FinalizaAtividade)]
    public async Task<IActionResult> Finalizar(AtividadeDto atividadeDto)
    {
        var resultado = await atividadeServico.Finalizar(atividadeDto);

        memoryCache.Remove(Constantes.MemoryCacheTodasAtividades);

        return Ok(resultado);
    }

    [HttpPost(Constantes.ReabreAtividade)]
    public async Task<IActionResult> Reabrir(AtividadeDto atividadeDto)
    {
        var resultado = await atividadeServico.Reabrir(atividadeDto);

        memoryCache.Remove(Constantes.MemoryCacheTodasAtividades);

        return Ok(resultado);
    }
}