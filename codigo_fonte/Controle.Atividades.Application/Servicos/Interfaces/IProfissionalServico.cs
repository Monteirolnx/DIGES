namespace Controle.Atividades.Application.Servicos.Interfaces;

public interface IProfissionalServico
{
    Task<List<AnalistaDto>?> ConsultaTodosAnalistas();

    Task<List<LiderDto>?> ConsultaTodosLideres();
}