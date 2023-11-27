namespace Controle.Atividades.Application.Servicos.Interfaces;

public interface IAtaServico
{
    Task<AtaDto?> Gerar();
}