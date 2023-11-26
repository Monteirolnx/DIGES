namespace Controle.Atividades.Application.Servicos;

public class AutenticacaoServico : IAutenticacaoServico
{
    public bool UsuarioEstaLogado { get; private set; }

    public void Login(string user, string password)
    {
        // Implementar lógica de login
        if (user == "a" && password == "1")
        {
            UsuarioEstaLogado = true;
        }
        else
        {
            UsuarioEstaLogado = false;
        }
    }

    public void Logout()
    {
        UsuarioEstaLogado = false;
    }
}
