namespace Controle.Atividades.Client.Layout;

public partial class MainLayout
{
    private bool sidebarExpandido = true;

    private void SidebarToggleClick()
    {
        sidebarExpandido = !sidebarExpandido;
    }
}