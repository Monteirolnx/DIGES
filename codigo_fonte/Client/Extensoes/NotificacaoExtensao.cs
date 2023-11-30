namespace Controle.Atividades.Client.Extensoes;

public static class NotificacaoExtensao
{
    private const int Duracao = 2000;
    public static void Sucesso(this NotificationService notification, string? mensagem = null)
    {
        const string mensagemPadrao = "Operação realizada com sucesso!";

        notification.Notify(new NotificationMessage
        {
            Severity = NotificationSeverity.Success,
            Summary = "Sucesso:", 
            Detail = mensagem ?? mensagemPadrao,
            Duration = Duracao
        });
    }

    public static void Erro(this NotificationService notification, string? mensagem = null)
    {
        const string mensagemPadrao = "Ocorreu um erro durante a operação.";

        notification.Notify(new NotificationMessage
        {
            Severity = NotificationSeverity.Error,
            Summary = "Erro:",
            Detail = mensagem ?? mensagemPadrao,
            Duration = Duracao
        });
    }

    public static void Exception(this NotificationService notification, Exception ex)
    {
        notification.Notify(new NotificationMessage
        {
            Severity = NotificationSeverity.Success,
            Summary = "Exception:",
            Detail = ex.Message,
            Duration = Duracao
        });
    }
}