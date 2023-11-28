﻿namespace Controle.Atividades.Client.Pages;

[UsedImplicitly]
public partial class AtividadeEdita
{
    [Parameter]
    public string? CodigoAtividade { get; set; }
    
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
    private AtividadeDto novaAtividade = new();

    private IEnumerable<AnalistaDto>? analistasDto;
    private IEnumerable<LiderDto>? lideresDto;

    private AnalistaDto analistaSelecionado = default!;
    private LiderDto liderSelecionado = default!;

    private bool carregando = true;
    private string? observacao = string.Empty;
    #endregion
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!AutenticacaoServico.UsuarioEstaLogado)
        {
            NavigationManager.NavigateTo("/login");
        }

        if (firstRender)
        {
            if (Guid.TryParse(CodigoAtividade, out var codigoAtividade))
            {
                novaAtividade = await AtividadeServico.ConsultarPorCodigo(codigoAtividade) ?? throw new InvalidOperationException();

                if (novaAtividade.Analista != null)
                {
                    analistaSelecionado = novaAtividade.Analista;
                }

                if (novaAtividade.Lider != null)
                {
                    liderSelecionado = novaAtividade.Lider;
                }

                if (novaAtividade.Historico != null)
                {
                     observacao = novaAtividade.Historico.Select(r=> r.Registro).FirstOrDefault();
                }
            }
            
            analistasDto = await ProfissionalServico.ConsultaTodosAnalistas();
            lideresDto = await ProfissionalServico.ConsultaTodosLideres();

            carregando = false;

            StateHasChanged();
        }
    }

    private async Task AbrirRedmine(int? pNumeroRedmine)
    {
        var url = $"https://redmine.sf.prefeitura.sp.gov.br/issues/{pNumeroRedmine}";
        await JsRuntime.InvokeVoidAsync("open", url, "_blank");
    }

    private void OnAnalistaChanged(AnalistaDto? analistaDto)
    {
        if (analistaDto != null)
        {
            analistaSelecionado = analistaDto;
        }

        StateHasChanged();
    }

    private void OnLiderChanged(LiderDto? liderDto)
    {
        if (liderDto != null)
        {
            liderSelecionado = liderDto;
        }

        StateHasChanged();
    }

    private async Task Enviar(AtividadeDto atividadeDto)
    {
        try
        {
            if (analistaSelecionado.Codigo != Guid.Empty)
            {
                atividadeDto.CodigoAnalista = analistaSelecionado.Codigo;
                atividadeDto.Analista = analistaSelecionado;
            }
            else
            {
                atividadeDto.Analista = null;
            }

            if (liderSelecionado.Codigo != Guid.Empty)
            {
                atividadeDto.CodigoLider = liderSelecionado.Codigo;
                atividadeDto.Lider = liderSelecionado;
            }
            else
            {
                atividadeDto.Lider = null;
            }

            if (!string.IsNullOrEmpty(observacao))
            {
                atividadeDto.Historico = new List<ObservacaoDto>
                {
                    new()
                    {
                        Data = DateTime.Now,
                        Registro = observacao
                    }
                };
            }
            else
            {
                atividadeDto.Historico = null;
            }

            atividadeDto.DtCriacao = DateTime.Now;
            atividadeDto.Status = TipoAbertaFechada.Aberta;

            if (await AtividadeServico.Adicionar(atividadeDto))
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Atividade adicionada", Duration = 2000 });
                NavigationManager.NavigateTo("/atividade-consulta-todas");
            }
        }
        catch (Exception ex)
        {
            NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Ocorreu um erro técnico. Entre em contato com o suporte.", Duration = 2000 });
            await JsRuntime.InvokeVoidAsync("console.log", ex.ToString());
            throw;
        }
    }

    private void Cancelar()
    {
        NavigationManager.NavigateTo("/atividade-consulta-todas");
    }
}