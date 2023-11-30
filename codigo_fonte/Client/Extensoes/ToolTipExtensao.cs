namespace Controle.Atividades.Client.Extensoes;

public static class ToolTipExtensao
{
    public static void MostrarAjuda(this TooltipService tooltipService, ElementReference elementReference, string tooltipText)
    {
        var tooltipOptions = new TooltipOptions { Delay = 100, Duration = 1000 };
        tooltipService.Open(elementReference, tooltipText, tooltipOptions);
    }
}