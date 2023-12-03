namespace Controle.Atividades.Client.Pages;

[UsedImplicitly]
public partial class AtividadeEdita
{
    [Parameter]
    public string CodigoAtividade { get; set; } = string.Empty;

    #region Injects
    [Inject]
    protected IAutenticacaoServico AutenticacaoServico { get; set; } = default!;

    [Inject]
    protected IAtividadeServico AtividadeServico { get; set; } = default!;

    [Inject]
    protected DialogService DialogService { get; set; } = default!;

    [Inject]
    protected IJSRuntime JsRuntime { get; set; } = default!;

    [Inject]
    protected IProfissionalServico ProfissionalServico { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    protected NotificationService NotificationService { get; set; } = default!;

    [Inject]
    protected TooltipService TooltipService { get; set; } = default!;
    #endregion

    #region Fields
    private AtividadeDto atividadeAtual = new();
    private AnalistaDto? analistaSelecionado;
    private LiderDto? liderSelecionado; 

    private IEnumerable<AnalistaDto>? analistasDto;
    private IEnumerable<LiderDto>? lideresDto;
    private IEnumerable<ObservacaoDto>? ultimaObservacao;

    private bool carregando = true;

    private string? observacao = string.Empty;
    #endregion

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!AutenticacaoServico.UsuarioEstaLogado)
        {
            NavigationManager.NavigateTo(Constantes.PaginaLogin);
        }
        else
        {
            await MontarMemoria(firstRender);
        }
    }
    
    private async Task MontarMemoria(bool firstRender)
    {
        if (firstRender)
        {
            await ConsultarAtividade();

            await ConsultaTodosAnalistas();

            await ConsultaTodosLideres();

            carregando = false;

            StateHasChanged();
        }
    }

    private async Task ConsultaTodosLideres()
    {
        var consultaTodosLideres = await ProfissionalServico.ConsultaTodosLideres();
        if (consultaTodosLideres != null)
        {
            lideresDto =
                consultaTodosLideres.Where(x => x.Status == TipoAtivoInativo.Ativo);
        }
    }

    private async Task ConsultaTodosAnalistas()
    {
        var consultaTodosAnalistas = await ProfissionalServico.ConsultaTodosAnalistas();
        if (consultaTodosAnalistas != null)
        {
            analistasDto =
                consultaTodosAnalistas.Where(x => x.Status == TipoAtivoInativo.Ativo);
        }
    }

    private async Task ConsultarAtividade()
    {
        try
        {
            if (Guid.TryParse(CodigoAtividade, out var codigoAtividade))
            {
                atividadeAtual = await AtividadeServico.ConsultarPorCodigo(codigoAtividade) ??
                                 throw new InvalidOperationException();

                if (atividadeAtual.Analista != null)
                {
                    analistaSelecionado = atividadeAtual.Analista;
                }

                if (atividadeAtual.Lider != null)
                {
                    liderSelecionado = atividadeAtual.Lider;
                }

                if (atividadeAtual.Historico != null)
                {
                    ultimaObservacao = atividadeAtual.Historico.OrderByDescending(h => h.Data).Take(1).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            NotificationService.Exception(ex);
            await JsRuntime.LogarErroConsole(ex);
        }
    }
    
    private void OnAnalistaChanged(AnalistaDto? analistaDto)
    {
        analistaSelecionado = analistaDto ?? null;

        StateHasChanged();
    }

    private void OnLiderChanged(LiderDto? liderDto)
    {
        liderSelecionado = liderDto ?? null;

        StateHasChanged();
    }

    private async Task Enviar(AtividadeDto atividadeDto)
    {
        try
        {
            if (analistaSelecionado is not null && analistaSelecionado.Codigo != Guid.Empty)
            {
                atividadeDto.CodigoAnalista = analistaSelecionado.Codigo;
                atividadeDto.Analista = analistaSelecionado;
            }
            else
            {
                atividadeDto.CodigoAnalista = null;
                atividadeDto.Analista = null;
            }

            if (liderSelecionado is not null &&  liderSelecionado.Codigo != Guid.Empty)
            {
                atividadeDto.CodigoLider = liderSelecionado.Codigo;
                atividadeDto.Lider = liderSelecionado;
            }
            else
            {
                atividadeDto.CodigoLider = null;
                atividadeDto.Lider = null;
            }

            if (!string.IsNullOrEmpty(observacao))
            {
                atividadeDto.Historico?.Add(new ObservacaoDto
                {
                    Data = DateTime.Now,
                    Registro = observacao
                });
            }

            if (await AtividadeServico.Editar(atividadeDto))
            {
                NotificationService.Sucesso("Atividade editada.");
            }
            NavigationManager.NavigateTo(Constantes.PaginaAtividadeConsultaTodas);
        }
        catch (Exception ex)
        {
            NotificationService.Exception(ex);
            await JsRuntime.LogarErroConsole(ex);
        }
    }

    private void Cancelar()
    {
        NavigationManager.NavigateTo("/atividade-consulta-todas");
    }

    private async Task MostrarHistorico()
    {
        await DialogService.OpenAsync<AtividadeHistorico>($"Atividade: {atividadeAtual.NumeroRedmine} - {atividadeAtual.Descricao}",
            new Dictionary<string, object> { { "codigoAtividade", atividadeAtual.Codigo.ToString() } },
            new DialogOptions { Width = "1200px", Height = "700px", Resizable = true, Draggable = true });

        await ConsultarAtividade();

        StateHasChanged();
    }
}