namespace Controle.Atividades.Client.Layout;

public partial class MainLayout
{
    #region Injects
    [Inject]
    protected IAutenticacaoServico AutenticacaoServico { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;
    #endregion

    private bool _sidebarExpandido = true;

    private void SidebarToggleClick()
    {
        _sidebarExpandido = !_sidebarExpandido;
    }

    private void Logout()
    {
        AutenticacaoServico.Deslogar();
        NavigationManager.NavigateTo(Constantes.PaginaLogin);
    }
}