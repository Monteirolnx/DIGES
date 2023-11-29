namespace Controle.Atividades.Client.Servicos;

public class AtividadeClientServico(HttpClient httpClient) : IAtividadeServico
{
    public async Task<bool> Adicionar(AtividadeDto atividadeDto)
    {
        var resultado = await httpClient.PostAsJsonAsync("/api/atividade/v1/adicionar-atividade", atividadeDto);

        return resultado.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<AtividadeDto>?> ConsultaTodas()
    {
        var httpResponseMessage = await httpClient.GetAsync("/api/atividade/v1/consulta-atividades");

        var resultado = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<AtividadeDto>?>();

        return resultado;
    }

    public async Task<AtividadeDto?> ConsultarPorCodigo(Guid codigoAtividade)
    {
        var httpResponseMessage = await httpClient.GetAsync($"/api/atividade/v1/consulta-codigo/{codigoAtividade}");

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            return null;
        }

        var resultado = await httpResponseMessage.Content.ReadFromJsonAsync<AtividadeDto?>();

        return resultado;
    }
}