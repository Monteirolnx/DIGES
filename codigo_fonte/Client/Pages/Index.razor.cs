namespace Controle.Atividades.Client.Pages;

public partial class Index
{
    [Parameter]
    public string? CodigoAtividade { get; set; }

    #region Injects
    [Inject]
    protected IAtividadeServico AtividadeServico { get; set; } = default!;

    [Inject]
    protected IJSRuntime JsRuntime { get; set; } = default!;
    
    [Inject]
    protected NotificationService NotificationService { get; set; } = default!;
    #endregion

    private IEnumerable<AtividadeDto>? atividadesDto;
    private bool carregando = true;
    private DataItem[]? atividades;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            await MontarMemoria(firstRender);
        }
        catch (Exception ex)
        {
            NotificationService.Exception(ex);
            await JsRuntime.LogarErroConsole(ex);
        }
    }

    private async Task MontarMemoria(bool firstRender)
    {
        try
        {
            if (firstRender)
            {
                atividadesDto = await AtividadeServico.ConsultarTodas();
                InicializarDados();
                carregando = false;
                StateHasChanged();
            }
        }
        catch (Exception ex)
        {
            NotificationService.Exception(ex);
            await JsRuntime.LogarErroConsole(ex);
        }
    }

    private void InicializarDados()
    {
        if (atividadesDto != null)
        {
            atividades = new DataItem[]
            {
                new()
                {
                    Data = ($"{DateTime.Now.Year}-01-01"),
                    Quantidade = atividadesDto.Count(x => x.DtCriacao.Month == 1 && x.DtCriacao.Year == DateTime.Now.Year)
                },
                new()
                {
                    Data = ($"{DateTime.Now.Year}-02-01"),
                    Quantidade = atividadesDto.Count(x => x.DtCriacao.Month == 2 && x.DtCriacao.Year == DateTime.Now.Year)
                },
                new()
                {
                    Data = ($"{DateTime.Now.Year}-03-01"),
                    Quantidade = atividadesDto.Count(x => x.DtCriacao.Month == 3 && x.DtCriacao.Year == DateTime.Now.Year)
                },
                new()
                {
                    Data = ($"{DateTime.Now.Year}-04-01"),
                    Quantidade = atividadesDto.Count(x => x.DtCriacao.Month == 4 && x.DtCriacao.Year == DateTime.Now.Year)
                },
                new()
                {
                    Data = ($"{DateTime.Now.Year}-05-01"),
                    Quantidade = atividadesDto.Count(x => x.DtCriacao.Month == 5 && x.DtCriacao.Year == DateTime.Now.Year)
                },
                new()
                {
                    Data = ($"{DateTime.Now.Year}-06-01"),
                    Quantidade = atividadesDto.Count(x => x.DtCriacao.Month == 6 && x.DtCriacao.Year == DateTime.Now.Year)
                },
                new()
                {
                    Data = ($"{DateTime.Now.Year}-07-01"),
                    Quantidade = atividadesDto.Count(x => x.DtCriacao.Month == 7 && x.DtCriacao.Year == DateTime.Now.Year)
                },
                new()
                {
                    Data = ($"{DateTime.Now.Year}-08-01"),
                    Quantidade = atividadesDto.Count(x => x.DtCriacao.Month == 8 && x.DtCriacao.Year == DateTime.Now.Year)
                },
                new()
                {
                    Data = ($"{DateTime.Now.Year}-09-01"),
                    Quantidade = atividadesDto.Count(x => x.DtCriacao.Month == 9 && x.DtCriacao.Year == DateTime.Now.Year)
                },
                new()
                {
                    Data = ($"{DateTime.Now.Year}-10-01"),
                    Quantidade = atividadesDto.Count(x => x.DtCriacao.Month == 10 && x.DtCriacao.Year == DateTime.Now.Year)
                },
                new()
                {
                    Data = ($"{DateTime.Now.Year}-11-01"),
                    Quantidade = atividadesDto.Count(x => x.DtCriacao.Month == 11 && x.DtCriacao.Year == DateTime.Now.Year)
                },
                new()
                {
                    Data = ($"{DateTime.Now.Year}-12-01"),
                    Quantidade = atividadesDto.Count(x => x.DtCriacao.Month == 12 && x.DtCriacao.Year == DateTime.Now.Year)
                }
            };
        }
    }


    private class DataItem
    {
        public string Data { get; set; } = string.Empty;

        public double Quantidade { get; set; }
    }


    private static string FormatarMes(object value)
    {
        return value != null ? Convert.ToDateTime(value).ToString("MMM") : string.Empty;
    }
}