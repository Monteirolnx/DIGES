namespace Controle.Atividades.Application.Servicos.Interfaces;

public interface IAutenticacaoServico
{
    bool UsuarioEstaLogado { get; }

    void Login(string user, string password);

    void Logout();
}