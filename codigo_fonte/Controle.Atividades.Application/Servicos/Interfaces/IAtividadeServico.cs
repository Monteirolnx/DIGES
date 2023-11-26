﻿namespace Controle.Atividades.Application.Servicos.Interfaces;

public interface IAtividadeServico
{
    Task<bool> Adicionar(AtividadeDto atividadeDto);

    Task<IEnumerable<AtividadeDto>?> ConsultaAtividades();
}