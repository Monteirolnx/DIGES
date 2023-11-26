namespace Controle.Atividades.Client.Servicos;

public class AtividadeServicoClient(HttpClient httpClient) : IAtividadeServico
{
    public async Task<bool> Adicionar(AtividadeDto atividadeDto)
    {
        var response = await httpClient.PostAsJsonAsync("/api/atividade/v1/adicionar-atividade", atividadeDto);

        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<AtividadeDto>?> ConsultaAtividades()
    {
        var httpResponseMessage = await httpClient.GetAsync("/api/atividade/v1/consulta-atividades");

        var resultado = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<AtividadeDto>?>();

        return resultado;
    }
}