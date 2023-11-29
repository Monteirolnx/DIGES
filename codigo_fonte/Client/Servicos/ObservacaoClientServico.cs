namespace Controle.Atividades.Client.Servicos;

public class ObservacaoClientServico(HttpClient httpClient) : IObservacaoServico
{
    public async Task<bool> Editar(ObservacaoDto observacaoDto)
    {
        var resultado = await httpClient.PostAsJsonAsync("/api/observacao/v1/editar-observacao", observacaoDto);

        return resultado.IsSuccessStatusCode;
    }
}