namespace Controle.Atividades.Client.Servicos;

public class ProfissionalServicoClient(HttpClient httpClient) : IProfissionalServico
{
    public async Task<List<AnalistaDto>?> ConsultaTodosAnalistas()
    {
        var httpResponseMessage = await httpClient.GetAsync("/api/profissional/v1/consulta-todos-analistas");

        var resultado = await httpResponseMessage.Content.ReadFromJsonAsync<List<AnalistaDto>?>();

        return resultado;
    }

    public async Task<List<LiderDto>?> ConsultaTodosLideres()
    {
        var httpResponseMessage = await httpClient.GetAsync("/api/profissional/v1/consulta-todos-lideres");

        var resultado = await httpResponseMessage.Content.ReadFromJsonAsync<List<LiderDto>?>();

        return resultado;
    }
}