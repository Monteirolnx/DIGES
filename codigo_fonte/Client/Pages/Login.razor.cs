﻿namespace Controle.Atividades.Client.Pages;

[UsedImplicitly]
public partial class Login
{
    #region Injects
    [Inject]
    protected IAutenticacaoServico AutenticacaoServico { get; set; } = default!;
    
    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;
    #endregion

    private bool _carregandoPagina;

    private Task Logar(LoginArgs args)
    {
        try
        {
            _carregandoPagina = true;
            AutenticacaoServico.Logar(args.Username, args.Password);
            NavigationManager.NavigateTo(AutenticacaoServico.UsuarioEstaLogado
                ? "/atividade-consulta-todas"
                : "/Logar");

            return Task.CompletedTask;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            _carregandoPagina = false;
        }
        
    }
}