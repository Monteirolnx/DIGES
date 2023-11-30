namespace Controle.Atividades.Application.Utils;

public static class Constantes
{
    #region Api
    public const string BaseUrlAta = "api/ata/";
    public const string GeraAta = "v1/gera";

    public const string BaseUrlAtividade = "api/atividade/";
    public const string AdicionaAtividade = "v1/adiciona";
    public const string EditaAtividade = "v1/edita";
    public const string ConsultaTodasAtividades = "v1/consulta-todas";
    public const string ConsultaPorCodigoAtividade = "v1/consulta-codigo/";

    public const string BaseUrlObservacao = "api/observacao/";
    public const string EditaObservacao = "v1/edita";
    public const string ExcluiObservacao = "v1/exclui";

    public const string BaseUrlProfissional = "api/profissional/";
    public const string ConsultaTodosAnalistas = "v1/consulta-todos-analistas";
    public const string ConsultaTodosLideres = "v1/consulta-todos-lideres";

    public const string PaginaLogin = "/login";
    public const string PaginaAtividadeConsultaTodas = "/atividade-consulta-todas";

    #endregion
}