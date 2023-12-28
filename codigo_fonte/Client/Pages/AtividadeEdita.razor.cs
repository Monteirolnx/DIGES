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
    private AtividadeDto _atividadeAtual = new();
    private AnalistaDto? _analistaSelecionado;
    private LiderDto? _liderSelecionado; 

    private IEnumerable<AnalistaDto>? _analistasDto;
    private IEnumerable<LiderDto>? _lideresDto;
    private IEnumerable<ObservacaoDto>? _ultimaObservacao;

    private bool _carregando = true;

    private string? _observacao = string.Empty;
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

            _carregando = false;

            StateHasChanged();
        }
    }

    private async Task ConsultaTodosLideres()
    {
        var consultaTodosLideres = await ProfissionalServico.ConsultaTodosLideres();
        if (consultaTodosLideres != null)
        {
            _lideresDto =
                consultaTodosLideres.Where(x => x.Status == TipoAtivoInativo.Ativo);
        }
    }

    private async Task ConsultaTodosAnalistas()
    {
        var consultaTodosAnalistas = await ProfissionalServico.ConsultaTodosAnalistas();
        if (consultaTodosAnalistas != null)
        {
            _analistasDto =
                consultaTodosAnalistas.Where(x => x.Status == TipoAtivoInativo.Ativo);
        }
    }

    private async Task ConsultarAtividade()
    {
        try
        {
            if (Guid.TryParse(CodigoAtividade, out var codigoAtividade))
            {
                _atividadeAtual = await AtividadeServico.ConsultarPorCodigo(codigoAtividade) ??
                                 throw new InvalidOperationException();

                if (_atividadeAtual.Analista != null)
                {
                    _analistaSelecionado = _atividadeAtual.Analista;
                }

                if (_atividadeAtual.Lider != null)
                {
                    _liderSelecionado = _atividadeAtual.Lider;
                }

                if (_atividadeAtual.Historico != null)
                {
                    _ultimaObservacao = _atividadeAtual.Historico.OrderByDescending(h => h.Data).Take(1).ToList();
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
        _analistaSelecionado = analistaDto ?? null;

        StateHasChanged();
    }

    private void OnLiderChanged(LiderDto? liderDto)
    {
        _liderSelecionado = liderDto ?? null;

        StateHasChanged();
    }

    private async Task Enviar(AtividadeDto atividadeDto)
    {
        try
        {
            if (_analistaSelecionado is not null && _analistaSelecionado.Codigo != Guid.Empty)
            {
                atividadeDto.CodigoAnalista = _analistaSelecionado.Codigo;
                atividadeDto.Analista = _analistaSelecionado;
            }
            else
            {
                atividadeDto.CodigoAnalista = null;
                atividadeDto.Analista = null;
            }

            if (_liderSelecionado is not null &&  _liderSelecionado.Codigo != Guid.Empty)
            {
                atividadeDto.CodigoLider = _liderSelecionado.Codigo;
                atividadeDto.Lider = _liderSelecionado;
            }
            else
            {
                atividadeDto.CodigoLider = null;
                atividadeDto.Lider = null;
            }

            if (!string.IsNullOrEmpty(_observacao))
            {
                atividadeDto.Historico?.Add(new ObservacaoDto
                {
                    Data = DateTime.Now,
                    Registro = _observacao
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
        await DialogService.OpenAsync<AtividadeHistorico>($"Atividade: {_atividadeAtual.NumeroRedmine} - {_atividadeAtual.Descricao}",
            new Dictionary<string, object> { { "codigoAtividade", _atividadeAtual.Codigo.ToString() } },
            new DialogOptions { Width = "1200px", Height = "700px", Resizable = true, Draggable = true });

        await ConsultarAtividade();

        StateHasChanged();
    }
}