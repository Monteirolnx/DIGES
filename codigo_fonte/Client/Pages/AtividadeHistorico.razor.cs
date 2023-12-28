namespace Controle.Atividades.Client.Pages;

public partial class AtividadeHistorico
{
    [Parameter]
    public string? CodigoAtividade { get; set; }

    #region Injects
    [Inject]
    protected IAtividadeServico AtividadeServico { get; set; } = default!;

    [Inject]
    protected IJSRuntime JsRuntime { get; set; } = default!;

    [Inject]
    protected IObservacaoServico ObservacaoServico { get; set; } = default!;

    [Inject]
    protected NotificationService NotificationService { get; set; } = default!;
    #endregion

    #region Fields
    private AtividadeDto? _atividadeAtual;

    private bool _carregando = true;
    #endregion

    private RadzenDataGrid<ObservacaoDto> _observacaoGrid = default!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await ConsultaAtividade();

            _carregando = false;
            StateHasChanged();
            await _observacaoGrid.LastPage();
        }
    }

    private async Task ConsultaAtividade()
    {
        if (Guid.TryParse(CodigoAtividade, out var codigoAtividade))
        {
            _atividadeAtual = await AtividadeServico.ConsultarPorCodigo(codigoAtividade);
            if (_atividadeAtual?.Historico != null)
            {
                _atividadeAtual.Historico = _atividadeAtual.Historico
                    .OrderBy(h => h.Data)
                    .ToList();
            }
        }
    }
    
    private async Task EditarLinha(ObservacaoDto observacaoDto)
    {
        await _observacaoGrid.EditRow(observacaoDto);
    }

    private async Task OnEditarLinha(ObservacaoDto observacaoDto)
    {
        try
        {
            if (await ObservacaoServico.Editar(observacaoDto))
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Observação editada.", Duration = 2000 });
            }
        }
        catch (Exception ex)
        {
            NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Ocorreu um erro técnico. Entre em contato com o suporte.", Duration = 2000 });
            await JsRuntime.InvokeVoidAsync("console.log", ex.ToString());
            throw;
        }
    }

    private async Task SalvarLinha(ObservacaoDto observacaoDto)
    {
        await _observacaoGrid.UpdateRow(observacaoDto);
    }

    private async Task DeletarLinha(ObservacaoDto observacaoDto)
    {
        try
        {
            if (await ObservacaoServico.Excluir(observacaoDto))
            {
                NotificationService.Notify(new NotificationMessage
                { Severity = NotificationSeverity.Success, Summary = "Observação excluída.", Duration = 2000 });

                await ConsultaAtividade();
                
                StateHasChanged();
                await _observacaoGrid.Reload();
                await _observacaoGrid.LastPage();
            }
        }
        catch (Exception ex)
        {
            NotificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Ocorreu um erro técnico. Entre em contato com o suporte.",
                Duration = 2000
            });

            await JsRuntime.InvokeVoidAsync("console.log", ex.ToString());
            throw;
        }
    }

    private void CancelarEdicao(ObservacaoDto observacaoDto)
    {
        _observacaoGrid.CancelEditRow(observacaoDto);
    }
}