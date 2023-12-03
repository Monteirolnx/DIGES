namespace Controle.Atividades.Server.Servicos;

public class ObservacaoServerServico(Contexto contexto, IMapper mapper, IMemoryCache memoryCache) : IObservacaoServico
{
    public async Task<bool> Editar(ObservacaoDto observacaoDto)
    {
        memoryCache.Remove(Constantes.MemoryCacheServerTodasAtividades);

        var observacaoExistente = await contexto.Historico.FirstOrDefaultAsync(o => o.Codigo == observacaoDto.Codigo);

        if (observacaoExistente == null)
        {
            return false;
        }

        mapper.Map(observacaoDto, observacaoExistente);

        contexto.Historico.Update(observacaoExistente);

        var resultado = await contexto.SaveChangesAsync();

        return resultado > 0;
    }

    public async Task<bool> Excluir(ObservacaoDto observacaoDto)
    {
        memoryCache.Remove(Constantes.MemoryCacheServerTodasAtividades);

        var observacaoExistente = await contexto.Historico.FirstOrDefaultAsync(o => o.Codigo == observacaoDto.Codigo);

        if (observacaoExistente == null)
        {
            return false;
        }

        contexto.Historico.Remove(observacaoExistente);

        var resultado = await contexto.SaveChangesAsync();

        return resultado > 0;
    }
}