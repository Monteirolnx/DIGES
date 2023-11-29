namespace Controle.Atividades.Client.Pages;

public partial class  AtividadeHistorico
{
    [Parameter]
    public string? CodigoAtividade { get; set; }

    [Inject]
    protected IAtividadeServico AtividadeServico { get; set; } = default!;

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

    private async Task SalvarLinha(ObservacaoDto observacaoDto)
    {
        await observacaoGrid.UpdateRow(observacaoDto);
    }
    

    private void Reset()
    {
        observacaoAtualizar = null;
    }


    private void OnUpdateRow(ObservacaoDto observacaoDto)
    {
        Reset();




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
