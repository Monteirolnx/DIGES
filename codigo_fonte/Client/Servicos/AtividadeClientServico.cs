namespace Controle.Atividades.Client.Servicos;

public class AtividadeClientServico(HttpClient httpClient) : IAtividadeServico
{
    public async Task<bool> Adicionar(AtividadeDto atividadeDto)
    {
        var resultado = await httpClient.PostAsJsonAsync($"{Constantes.BaseUrlAtividade}{Constantes.AdicionaAtividade}", atividadeDto);

        return resultado.IsSuccessStatusCode;
    }

    public async Task<bool> Editar(AtividadeDto atividadeDto)
    {
        var resultado = await httpClient.PostAsJsonAsync($"{Constantes.BaseUrlAtividade}{Constantes.EditaAtividade}", atividadeDto);

        return resultado.IsSuccessStatusCode;
    }

    public async Task<bool> Excluir(AtividadeDto atividadeDto)
    {
        var resultado = await httpClient.PostAsJsonAsync($"{Constantes.BaseUrlAtividade}{Constantes.ExcluiAtividade}", atividadeDto);

        return resultado.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<AtividadeDto>?> ConsultarTodas()
    {
        var httpResponseMessage = await httpClient.GetAsync($"{Constantes.BaseUrlAtividade}{Constantes.ConsultaTodasAtividades}");

        var resultado = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<AtividadeDto>?>();

        return resultado;
    }

    public async Task<AtividadeDto?> ConsultarPorCodigo(Guid codigoAtividade)
    {
        var httpResponseMessage = await httpClient.GetAsync($"{Constantes.BaseUrlAtividade}{Constantes.ConsultaPorCodigoAtividade}{codigoAtividade}");

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            return null;
        }

        var resultado = await httpResponseMessage.Content.ReadFromJsonAsync<AtividadeDto?>();

        return resultado;
    }

    public async Task<bool> Finalizar(Guid atividadedtoCodigo)
    {
        var resultado = await httpClient.PostAsJsonAsync($"{Constantes.BaseUrlAtividade}{Constantes.ExcluiAtividade}", atividadedtoCodigo);

        return resultado.IsSuccessStatusCode;
    }

    public async Task<bool> Reabrir(Guid atividadedtoCodigo)
    {
        var resultado = await httpClient.PostAsJsonAsync($"{Constantes.BaseUrlAtividade}{Constantes.ExcluiAtividade}", atividadedtoCodigo);

        return resultado.IsSuccessStatusCode;
    }
}