namespace Controle.Atividades.Application.Servicos.Interfaces;

public interface IAutenticacaoServico
{
    bool UsuarioEstaLogado { get; }

    void Logar(string usuario, string password);

    void Deslogar();
}