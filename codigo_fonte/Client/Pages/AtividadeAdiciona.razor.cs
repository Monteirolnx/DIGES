namespace Controle.Atividades.Client.Pages;

[UsedImplicitly]
public partial class AtividadeAdiciona
{
    #region Injects
    [Inject]
    protected IAutenticacaoServico AutenticacaoServico { get; set; } = default!;

    [Inject]
    protected IAtividadeServico AtividadeServico { get; set; } = default!;

    [Inject]
    protected IJSRuntime JsRuntime { get; set; } = default!;

    [Inject]
    protected IProfissionalServico ProfissionalServico { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    protected NotificationService NotificationService { get; set; } = default!;
    #endregion

    #region Fields
    private readonly AtividadeDto _atividadeNova = new();

    private IEnumerable<AnalistaDto>? _analistasDto;
    private IEnumerable<LiderDto>? _lideresDto;

    private AnalistaDto? _analistaSelecionado;
    private LiderDto? _liderSelecionado;

    private bool _carregando = true;
    private string _observacao = string.Empty;
    #endregion

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
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
                await ConsultaTodosAnalistas();
                
                await ConsultaTodosLideres();

                _carregando = false;

                StateHasChanged();
            }
        }
        catch (Exception ex)
        {
            NotificationService.Exception(ex);
            await JsRuntime.LogarErroConsole(ex);
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

    private async Task Enviar(AtividadeDto atividadeDto)
    {
        try
        {
            DefinirAnalista(atividadeDto);

            DefinirLider(atividadeDto);

            DefinirObservacao(atividadeDto);

            await AdicionarAtividade(atividadeDto);
        }
        catch (Exception ex)
        {
            NotificationService.Exception(ex);
            await JsRuntime.LogarErroConsole(ex);
        }
    }

    private void DefinirAnalista(AtividadeDto atividadeDto)
    {
        if (_analistaSelecionado != null && _analistaSelecionado.Codigo != Guid.Empty)
        {
            atividadeDto.CodigoAnalista = _analistaSelecionado.Codigo;
            atividadeDto.Analista = _analistaSelecionado;
        }
        else
        {
            atividadeDto.Analista = null;
        }
    }

    private void DefinirLider(AtividadeDto atividadeDto)
    {
        if (_liderSelecionado != null && _liderSelecionado.Codigo != Guid.Empty)
        {
            atividadeDto.CodigoLider = _liderSelecionado.Codigo;
            atividadeDto.Lider = _liderSelecionado;
        }
        else
        {
            atividadeDto.Lider = null;
        }
    }

    private void DefinirObservacao(AtividadeDto atividadeDto)
    {
        if (!string.IsNullOrEmpty(_observacao))
        {
            atividadeDto.Historico = new List<ObservacaoDto>
            {
                new()
                {
                    Data = DateTime.Now,
                    Registro = _observacao
                }
            };
        }
        else
        {
            atividadeDto.Historico = null;
        }
    }

    private async Task AdicionarAtividade(AtividadeDto atividadeDto)
    {
        if (await AtividadeServico.Adicionar(atividadeDto))
        {
            NotificationService.Sucesso("Atividade adicionada.");
            NavigationManager.NavigateTo(Constantes.PaginaAtividadeConsultaTodas);
        }
    }

    private void OnAnalistaChanged(AnalistaDto? analistaDto)
    {
        _analistaSelecionado = analistaDto;

        StateHasChanged();
    }

    private void OnLiderChanged(LiderDto? liderDto)
    {
        _liderSelecionado = liderDto;

        StateHasChanged();
    }

    private void Cancelar()
    {
        NavigationManager.NavigateTo(Constantes.PaginaAtividadeConsultaTodas);
    }
}