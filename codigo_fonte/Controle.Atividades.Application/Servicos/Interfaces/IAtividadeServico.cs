﻿namespace Controle.Atividades.Application.Servicos.Interfaces;

public interface IAtividadeServico
{
    Task<bool> Adicionar(AtividadeDto atividadeDto);

    Task<bool> Editar(AtividadeDto atividadeDto);

    Task<bool> Excluir(AtividadeDto atividadeDto);

    Task<IEnumerable<AtividadeDto>?> ConsultarTodas();

    Task<AtividadeDto?> ConsultarPorCodigo(Guid codigoAtividade);
    
    Task<bool> Finalizar(Guid atividadedtoCodigo);

    Task<bool> Reabrir(Guid atividadedtoCodigo);
}