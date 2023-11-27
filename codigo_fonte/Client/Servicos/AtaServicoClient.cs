namespace Controle.Atividades.Client.Servicos;

public class AtaServicoClient(HttpClient httpClient) : IAtaServico
{
    public async Task<AtaDto?> Gerar()
    {
        var httpResponseMessage = await httpClient.GetAsync("/api/ata/v1/gerar");

        var resultado = await httpResponseMessage.Content.ReadFromJsonAsync<AtaDto?>();

        return resultado;
    }
}