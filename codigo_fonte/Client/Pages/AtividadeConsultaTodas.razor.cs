namespace Controle.Atividades.Client.Pages;

[UsedImplicitly]
public partial class AtividadeConsultaTodas
{
    #region Injects
    [Inject]
    protected IAutenticacaoServico AutenticacaoServico { get; set; } = default!;

    [Inject]
    protected IAtaServico AtaServico { get; set; } = default!;

    [Inject]
    protected IAtividadeServico AtividadeServico { get; set; } = default!;

    [Inject]
    protected DialogService DialogService { get; set; } = default!;

    [Inject]
    protected IJSRuntime JsRuntime { get; set; } = default!;
  
    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    protected IProfissionalServico ProfissionalServico { get; set; } = default!;

    [Inject]
    protected TooltipService TooltipService { get; set; } = default!;
    #endregion

    #region Fields
    private IEnumerable<AtividadeDto>? atividadesDto;

    private bool carregando = true;
    #endregion

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!AutenticacaoServico.UsuarioEstaLogado)
        {
            NavigationManager.NavigateTo("/login");
        }

        if (firstRender)
        {
            atividadesDto = await AtividadeServico.ConsultarTodas();
            carregando = false;
            StateHasChanged();
        }
    }

    private void IncluirAtividade()
    {
        NavigationManager.NavigateTo("/atividade-adiciona");
    }

    private async Task GerarAta()
    {
        var ata = await AtaServico.Gerar();

        if (ata != null)
        {
            await JsRuntime.InvokeVoidAsync("openNewTabWithHtml", HtmlUtil.CriarAta(ata));
        }
    }

    private void EditarAtividade(AtividadeDto atividadedto)
    {
        NavigationManager.NavigateTo($"/atividade-edita/{atividadedto.Codigo}");
    }

    private async Task ExcluirAtividade(AtividadeDto atividadedto)
    {
        var questao = await DialogService.Confirm("Tem certeza que deseja excluir esta atividade?",
            "Confirmar Exclusão", new ConfirmOptions { OkButtonText = "Excluir", CancelButtonText = "Cancelar" });

        if (questao == true)
        {
            // Código para excluir a atividade
            // Por exemplo: await AtividadeServico.Excluir(atividadedto.Codigo);
        }
    }

    private async Task FinalizarAtividade(AtividadeDto atividadedto)
    {
        var questao = await DialogService.Confirm("Tem certeza que deseja finalizar esta atividade?",
            "Confirmar fechamento", new ConfirmOptions { OkButtonText = "Finalizar", CancelButtonText = "Cancelar" });

        if (questao == true)
        {
            // Código para excluir a atividade
            // Por exemplo: await AtividadeServico.Excluir(atividadedto.Codigo);
        }
    }

    private async Task ReabrirAtividade(AtividadeDto atividadedto)
    {
        var questao = await DialogService.Confirm("Tem certeza que deseja reabrir esta atividade?",
            "Confirmar re-abertura", new ConfirmOptions { OkButtonText = "Reabrir", CancelButtonText = "Cancelar" });

        if (questao == true)
        {
            // Código para excluir a atividade
            // Por exemplo: await AtividadeServico.Excluir(atividadedto.Codigo);
        }
    }

    private async Task AbrirRedmine(int? pNumeroRedmine)
    {
        var url = $"https://redmine.sf.prefeitura.sp.gov.br/issues/{pNumeroRedmine}";
        await JsRuntime.InvokeVoidAsync("open", url, "_blank");
    }

    #region Auxiliares
    private void MostrarAjuda(ElementReference elementReference, string tooltipText, TooltipOptions? options = null)
    {
        var tooltipOptions = options ?? new TooltipOptions { Delay = 100, Duration = 1000 };
        TooltipService.Open(elementReference, tooltipText, tooltipOptions);
    }
    #endregion
}