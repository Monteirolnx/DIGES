namespace Controle.Atividades.Client.Servicos;

public class AtaClientServico(HttpClient httpClient) : IAtaServico
{
    public async Task<AtaDto?> Gerar()
    {
        var httpResponseMessage = await httpClient.GetAsync($"{Constantes.BaseUrlAta}{Constantes.GeraAta}");

        var resultado = await httpResponseMessage.Content.ReadFromJsonAsync<AtaDto?>();

        return resultado;
    }
}