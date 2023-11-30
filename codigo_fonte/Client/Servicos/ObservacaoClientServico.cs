namespace Controle.Atividades.Client.Servicos;

public class ObservacaoClientServico(HttpClient httpClient) : IObservacaoServico
{
    public async Task<bool> Editar(ObservacaoDto observacaoDto)
    {
        var resultado = await httpClient.PostAsJsonAsync($"{Constantes.BaseUrlObservacao}{Constantes.EditaObservacao}", observacaoDto);

        return resultado.IsSuccessStatusCode;
    }

    public async Task<bool> Excluir(ObservacaoDto observacaoDto)
    {
        var resultado = await httpClient.PostAsJsonAsync($"{Constantes.BaseUrlObservacao}{Constantes.ExcluiObservacao}", observacaoDto);

        return resultado.IsSuccessStatusCode;
    }
}