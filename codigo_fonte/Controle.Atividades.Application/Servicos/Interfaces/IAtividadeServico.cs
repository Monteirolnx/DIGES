namespace Controle.Atividades.Application.Servicos.Interfaces;

public interface IAtividadeServico
{
    Task<bool> Adicionar(AtividadeDto atividadeDto);

    Task<bool> Editar(AtividadeDto atividadeDto);

    Task<IEnumerable<AtividadeDto>?> ConsultarTodas();

    Task<AtividadeDto?> ConsultarPorCodigo(Guid codigoAtividade);
}