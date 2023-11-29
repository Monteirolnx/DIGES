namespace Controle.Atividades.Client.Pages;

public partial class  AtividadeHistorico
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
    private AtividadeDto? atividadeAtual;

    private bool carregando = true;
    #endregion

    private RadzenDataGrid<ObservacaoDto> observacaoGrid = default!;

    private ObservacaoDto? observacaoAtualizar;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (Guid.TryParse(CodigoAtividade, out var codigoAtividade))
            {
                atividadeAtual = await AtividadeServico.ConsultarPorCodigo(codigoAtividade);
            }
            carregando = false;

            StateHasChanged();
        }
    }

    private async Task EditarLinha(ObservacaoDto observacaoDto)
    {
        observacaoAtualizar = observacaoDto;
        await observacaoGrid.EditRow(observacaoDto);
    }

    private async Task OnEditarLinha(ObservacaoDto observacaoDto)
    {
        try
        {
            Reset();
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
        await observacaoGrid.UpdateRow(observacaoDto);
    }
    

    private void Reset()
    {
        observacaoAtualizar = null;
    }





    private void CancelEdit(ObservacaoDto observacaoDto)
    {
        Reset();
        observacaoGrid.CancelEditRow(observacaoDto);
    }

    private async Task DeleteRow(ObservacaoDto observacaoDto)
    {
        Reset();

        // if (orders.Contains(observacaoDto))
        // {
        //     dbContext.Remove<Order>(observacaoDto);

        //     dbContext.SaveChanges();

        //     await observacaoGrid.Reload();
        // }
        // else
        // {
        //     observacaoGrid.CancelEditRow(observacaoDto);
        //     await observacaoGrid.Reload();
        // }
    }
}
