﻿namespace Controle.Atividades.Application.Servicos.Interfaces;

public interface IObservacaoServico
{
    Task<bool> Editar(ObservacaoDto observacaoDto);
}