﻿namespace Controle.Atividades.Server.Servicos;

public class AtividadeServico (Contexto contexto, IMapper mapper) : IAtividadeServico
{
    public async Task<bool> Adicionar(AtividadeDto atividadeDto)
    {
        var atividade = mapper.Map<Atividade>(atividadeDto);

        if (atividadeDto.CodigoAnalista != Guid.Empty)
        {
            var analista = contexto.Analistas.FirstOrDefault(a => a.Codigo == atividadeDto.CodigoAnalista);
            if (analista != null)
            {
                atividade.Analista = analista;
            }
        }

        if (atividadeDto.CodigoLider != Guid.Empty)
        {
            var lider = contexto.Lideres.FirstOrDefault(a => a.Codigo == atividadeDto.CodigoLider);
            if (lider != null)
            {
                atividade.Lider = lider;
            }
        }

        await contexto.Atividades.AddAsync(atividade);

       var resultado = await contexto.SaveChangesAsync();

       return resultado > 0;
    }

    public async Task<IEnumerable<AtividadeDto>?> ConsultaAtividades()
    {
        var atividades = await contexto.Atividades
            .AsNoTracking()
            .Include(a=> a.Analista)
            .Include(a=> a.Lider)
            .Include(a=> a.Historico)
            .OrderBy(a=> a.Analista!.Nome)
            .ToListAsync();

        var resultado = mapper.Map<IEnumerable<AtividadeDto>>(atividades);

        return resultado;
    }
}