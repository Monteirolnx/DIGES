﻿using System.Text;

namespace Controle.Atividades.Client.Pages;

[UsedImplicitly]
public partial class AtividadeConsultaTodas
{
    #region Injects
    [Inject]
    protected IAutenticacaoServico AutenticacaoServico { get; set; } = default!;

    [Inject]
    protected IAtividadeServico AtividadeServico { get; set; } = default!;

    [Inject]
    protected IJSRuntime JsRuntime { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    protected IProfissionalServico ProfissionalServico { get; set; } = default!;
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
            atividadesDto = await AtividadeServico.ConsultaAtividades();
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
        var htmlBuilder = new StringBuilder();

        htmlBuilder.AppendLine("<html>");
        htmlBuilder.AppendLine("<head>");
        htmlBuilder.AppendLine("<style>");
        htmlBuilder.AppendLine("body { font-family: Calibri, sans-serif; font-size: 11pt; color: rgb(0,0,0); }");
        htmlBuilder.AppendLine("table { width: 100%; border-collapse: collapse; }");
        htmlBuilder.AppendLine("th, td { border: 1px solid black; padding: 5px; text-align: left; }");
        htmlBuilder.AppendLine("th { background-color: rgb(231,230,230); }");
        htmlBuilder.AppendLine("td { white-space: pre-wrap; }");
        htmlBuilder.AppendLine("</style>");
        htmlBuilder.AppendLine("</head>");
        htmlBuilder.AppendLine("<body>");
        htmlBuilder.AppendLine("<div dir='ltr'>");
        htmlBuilder.AppendLine($"<p>Segue ata da reunião: {DateTime.Now.ToString("dd/MM/yyyy")}.</p>");

        htmlBuilder.AppendLine("<p><b><u>Atividades criadas:</u></b> ...</p>");
        htmlBuilder.AppendLine("<p><b><u>Atividades atualizadas:</u></b> ...</p>");
        htmlBuilder.AppendLine("<p><b><u>Atividades finalizadas/excluídas:</u></b> ...</p>");

        htmlBuilder.AppendLine("<table>");
        htmlBuilder.AppendLine("<tr><th>Atividade</th><th>Descrição</th><th>Sistema</th><th>Analista</th><th>Histórico</th></tr>");

        htmlBuilder.AppendLine("<tr><td>3618</td><td>Investigação para implementação do IP Restriction</td><td>Todos</td><td>Monteiro</td><td>20/03/2023 – Nesta semana trabalharam: Glaucon, Félix, Cláudio Magno, Alex...</td></tr>");

        htmlBuilder.AppendLine("</table>");
        htmlBuilder.AppendLine("</div>");
        htmlBuilder.AppendLine("</body>");
        htmlBuilder.AppendLine("</html>");

        await JsRuntime.InvokeVoidAsync("openNewTabWithHtml", htmlBuilder.ToString());
    }
    
    private Task EditarAtividade(AtividadeDto atividadedto)
    {
        throw new NotImplementedException();
    }

    private Task ExcluirAtividade(AtividadeDto atividadedto)
    {
        throw new NotImplementedException();
    }

    private Task FecharAtividade(AtividadeDto atividadedto)
    {
        throw new NotImplementedException();
    }

    private async Task AbrirRedmine(int? pNumeroRedmine)
    {
        var url = $"https://redmine.sf.prefeitura.sp.gov.br/issues/{pNumeroRedmine}";
        await JsRuntime.InvokeVoidAsync("open", url, "_blank");
    }
}