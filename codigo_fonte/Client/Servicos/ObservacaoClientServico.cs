namespace Controle.Atividades.Client.Servicos;

public class ObservacaoClientServico(HttpClient httpClient, ISessionStorageService sessionStorageService) : IObservacaoServico
{
    public async Task<bool> Editar(ObservacaoDto observacaoDto)
    {
        await sessionStorageService.RemoveItemAsync(Constantes.MemoryCacheClientTodasAtividades);

        var resultado = await httpClient.PostAsJsonAsync($"{Constantes.BaseUrlObservacao}{Constantes.EditaObservacao}", observacaoDto);

        return resultado.IsSuccessStatusCode;
    }

    public async Task<bool> Excluir(ObservacaoDto observacaoDto)
    {
        await sessionStorageService.RemoveItemAsync(Constantes.MemoryCacheClientTodasAtividades);

        var resultado = await httpClient.PostAsJsonAsync($"{Constantes.BaseUrlObservacao}{Constantes.ExcluiObservacao}", observacaoDto);

        return resultado.IsSuccessStatusCode;
    }
}