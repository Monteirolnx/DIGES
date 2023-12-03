namespace Controle.Atividades.Client.Servicos;

public class AtividadeClientServico(HttpClient httpClient, ISessionStorageService sessionStorageService) : IAtividadeServico
{
    public async Task<bool> Adicionar(AtividadeDto atividadeDto)
    {
        await sessionStorageService.RemoveItemAsync(Constantes.MemoryCacheClientTodasAtividades);

        var resultado = await httpClient.PostAsJsonAsync($"{Constantes.BaseUrlAtividade}{Constantes.AdicionaAtividade}", atividadeDto);

        return resultado.IsSuccessStatusCode;
    }

    public async Task<bool> Editar(AtividadeDto atividadeDto)
    {
        await sessionStorageService.RemoveItemAsync(Constantes.MemoryCacheClientTodasAtividades);

        var resultado = await httpClient.PostAsJsonAsync($"{Constantes.BaseUrlAtividade}{Constantes.EditaAtividade}", atividadeDto);

        return resultado.IsSuccessStatusCode;
    }

    public async Task<bool> Excluir(AtividadeDto atividadeDto)
    {
        await sessionStorageService.RemoveItemAsync(Constantes.MemoryCacheClientTodasAtividades);

        var resultado = await httpClient.PostAsJsonAsync($"{Constantes.BaseUrlAtividade}{Constantes.ExcluiAtividade}", atividadeDto);

        return resultado.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<AtividadeDto>?> ConsultarTodas()
    {
        var resultadoCache = await sessionStorageService.GetItemAsync<IEnumerable<AtividadeDto>>(Constantes.MemoryCacheClientTodasAtividades);

        if (resultadoCache is not null)
        {
            return resultadoCache;
        }

        var httpResponseMessage = await httpClient.GetAsync($"{Constantes.BaseUrlAtividade}{Constantes.ConsultaTodasAtividades}");
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            return null;
        }

        var atividades = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<AtividadeDto>>();
        if (atividades == null)
        {
            return null;
        }

        var resultado = atividades.ToList();
        await sessionStorageService.SetItemAsync(Constantes.MemoryCacheClientTodasAtividades, resultado);
        return resultado;
    }

    public async Task<AtividadeDto?> ConsultarPorCodigo(Guid codigoAtividade)
    {
        await sessionStorageService.RemoveItemAsync(Constantes.MemoryCacheClientTodasAtividades);

        var httpResponseMessage = await httpClient.GetAsync($"{Constantes.BaseUrlAtividade}{Constantes.ConsultaPorCodigoAtividade}{codigoAtividade}");

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            return null;
        }

        var resultado = await httpResponseMessage.Content.ReadFromJsonAsync<AtividadeDto?>();

        return resultado;
    }

    public async Task<bool> Finalizar(AtividadeDto atividadeDto)
    {
        await sessionStorageService.RemoveItemAsync(Constantes.MemoryCacheClientTodasAtividades);

        var resultado = await httpClient.PostAsJsonAsync($"{Constantes.BaseUrlAtividade}{Constantes.FinalizaAtividade}", atividadeDto);

        return resultado.IsSuccessStatusCode;
    }

    public async Task<bool> Reabrir(AtividadeDto atividadeDto)
    {
        await sessionStorageService.RemoveItemAsync(Constantes.MemoryCacheClientTodasAtividades);

        var resultado = await httpClient.PostAsJsonAsync($"{Constantes.BaseUrlAtividade}{Constantes.ReabreAtividade}", atividadeDto);

        return resultado.IsSuccessStatusCode;
    }
}