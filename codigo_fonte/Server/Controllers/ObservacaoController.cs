namespace Controle.Atividades.Server.Controllers;

[ApiController]
[Route(Constantes.BaseUrlObservacao)]
public class ObservacaoController(IObservacaoServico observacaoServico, IMemoryCache memoryCache) : ControllerBase
{

    [HttpPost(Constantes.EditaObservacao)]
    public async Task<IActionResult> Editar(ObservacaoDto observacaoDto)
    {
        var resultado = await observacaoServico.Editar(observacaoDto);


        memoryCache.Remove(Constantes.MemoryCacheTodasAtividades);

        return Ok(resultado);
    }

    [HttpPost(Constantes.ExcluiObservacao)]
    public async Task<IActionResult> Excluir(ObservacaoDto observacaoDto)
    {
        var resultado = await observacaoServico.Excluir(observacaoDto);
        
        memoryCache.Remove(Constantes.MemoryCacheTodasAtividades);

        return Ok(resultado);
    }
}