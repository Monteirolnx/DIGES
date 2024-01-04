namespace Controle.Atividades.Application.Servicos;

public class AutenticacaoServico(IConfiguration configuration) : IAutenticacaoServico
{
    public bool UsuarioEstaLogado { get; private set; }
    
    public void Logar(string usuario, string password)
    {
        var confUsername = configuration["Credentials:Username"];
        var confPassword = configuration["Credentials:Password"];
        if (usuario == confUsername && password == confPassword)
        {
            UsuarioEstaLogado = true;
        }
        else
        {
            UsuarioEstaLogado = false;
        }
    }

    public void Deslogar()
    {
        UsuarioEstaLogado = false;
    }
}