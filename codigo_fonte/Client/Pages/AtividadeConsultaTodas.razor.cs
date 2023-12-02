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
    protected IProfissionalServico ProfissionalServico { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    protected NotificationService NotificationService { get; set; } = default!;

    [Inject]
    protected TooltipService TooltipService { get; set; } = default!;
    #endregion

    #region Fields
    private RadzenDataGrid<AtividadeDto> atividadeDtoGrid = default!;

    private IEnumerable<AtividadeDto>? atividadesDto;

    private bool carregando = true;
    #endregion

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            VerificarAutenticacao();

            await MontarMemoria(firstRender);
        }
        catch (Exception ex)
        {
            NotificationService.Exception(ex);
            await JsRuntime.LogarErroConsole(ex);
        }
    }

    private void VerificarAutenticacao()
    {
        if (!AutenticacaoServico.UsuarioEstaLogado)
        {
            NavigationManager.NavigateTo(Constantes.PaginaLogin);
        }
    }

    private async Task MontarMemoria(bool firstRender)
    {
        try
        {
            if (firstRender)
            {
                atividadesDto = await AtividadeServico.ConsultarTodas();
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

    private void NavegarAdicionaAtividade()
    {
        NavigationManager.NavigateTo(Constantes.PaginaAtividadeAdiciona);
    }

    private void NavedarEditaAtividade(AtividadeDto atividadedto)
    {
        NavigationManager.NavigateTo($"{Constantes.PaginaAtividadeEdita}{atividadedto.Codigo}");
    }

    private async Task GerarAta()
    {
        try
        {
            var ata = await AtaServico.Gerar();

            if (ata != null)
            {
                await JsRuntime.InvokeVoidAsync("openNewTabWithHtml", HtmlUtil.CriarAta(ata));
            }
        }
        catch (Exception ex)
        {
            NotificationService.Exception(ex);
            await JsRuntime.LogarErroConsole(ex);
        }
    }

    private async Task ExcluirAtividade(AtividadeDto atividadedto)
    {
        try
        {
            var questao = await DialogService.Confirm("Tem certeza que deseja excluir esta atividade?",
                "Confirmar Exclusão", new ConfirmOptions { OkButtonText = "Excluir", CancelButtonText = "Cancelar" });

            if (questao == true)
            {
                if (await AtividadeServico.Excluir(atividadedto))
                {
                    NotificationService.Sucesso("Atividade excluída com sucesso!");
                }
                else
                {
                    NotificationService.Erro("Não foi possível excluir a atividade!");
                }

                atividadesDto = await AtividadeServico.ConsultarTodas();
                StateHasChanged();
                await atividadeDtoGrid.Reload();
            }
        }
        catch (Exception ex)
        {
            NotificationService.Exception(ex);
            await JsRuntime.LogarErroConsole(ex);
        }
    }

    private async Task FinalizarAtividade(AtividadeDto atividadeDto)
    {
        try
        {
            var questao = await DialogService.Confirm("Tem certeza que deseja finalizar esta atividade?",
                "Confirmar fechamento", new ConfirmOptions { OkButtonText = "Finalizar", CancelButtonText = "Cancelar" });

            if (questao == true)
            {
                if (await AtividadeServico.Finalizar(atividadeDto))
                {
                    NotificationService.Sucesso("Atividade finalizada com sucesso!");
                }
                else
                {
                    NotificationService.Erro("Não foi possível finalizar a atividade!");
                }

                atividadesDto = await AtividadeServico.ConsultarTodas();
                StateHasChanged();
                await atividadeDtoGrid.Reload();
            }
        }
        catch (Exception ex)
        {
            NotificationService.Exception(ex);
            await JsRuntime.LogarErroConsole(ex);
        }
    }

    private async Task ReabrirAtividade(AtividadeDto atividadeDto)
    {
        try
        {
            var questao = await DialogService.Confirm("Tem certeza que deseja reabrir esta atividade?",
                "Confirmar re-abertura", new ConfirmOptions { OkButtonText = "Reabrir", CancelButtonText = "Cancelar" });

            if (questao == true)
            {
                if (await AtividadeServico.Reabrir(atividadeDto))
                {
                    NotificationService.Sucesso("Atividade reaberta com sucesso!");
                }
                else
                {
                    NotificationService.Erro("Não foi possível reaberta a atividade!");
                }

                atividadesDto = await AtividadeServico.ConsultarTodas();
                StateHasChanged();
                await atividadeDtoGrid.Reload();
            }
        }
        catch (Exception ex)
        {
            NotificationService.Exception(ex);
            await JsRuntime.LogarErroConsole(ex);
        }
    }
}