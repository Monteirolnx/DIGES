namespace Controle.Atividades.Application.Servicos;

public class AutenticacaoServico : IAutenticacaoServico
{
    public bool UsuarioEstaLogado { get; private set; }

    public void Logar(string usuario, string password)
    {
        if (usuario == "controle" && password == "atividade1234")
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