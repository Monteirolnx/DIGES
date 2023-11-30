namespace Controle.Atividades.Client.Extensoes;

public static class JSRuntimeExtensao
{
    public static async Task AbrirRedmine(this IJSRuntime jsRuntime, int? numeroRedmine)
    {
        var url = $"https://redmine.sf.prefeitura.sp.gov.br/issues/{numeroRedmine}";
        await jsRuntime.InvokeVoidAsync("open", url, "_blank");
    }

    public static async Task LogarErroConsole(this IJSRuntime jsRuntime, Exception ex)
    {
        await jsRuntime.InvokeVoidAsync("console.log", ex.StackTrace);
    }
 
}