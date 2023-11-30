namespace Controle.Atividades.Client.Servicos;

public class ProfissionalClientServico(HttpClient httpClient) : IProfissionalServico
{
    public async Task<List<AnalistaDto>?> ConsultaTodosAnalistas()
    {
        var httpResponseMessage = await httpClient.GetAsync($"{Constantes.BaseUrlProfissional}{Constantes.ConsultaTodosAnalistas}");

        var resultado = await httpResponseMessage.Content.ReadFromJsonAsync<List<AnalistaDto>?>();

        return resultado;
    }

    public async Task<List<LiderDto>?> ConsultaTodosLideres()
    {
        var httpResponseMessage = await httpClient.GetAsync($"{Constantes.BaseUrlProfissional}{Constantes.ConsultaTodosLideres}");

        var resultado = await httpResponseMessage.Content.ReadFromJsonAsync<List<LiderDto>?>();

        return resultado;
    }
}