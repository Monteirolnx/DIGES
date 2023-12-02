namespace Controle.Atividades.Server.Servicos;

public class AtividadeServerServico(Contexto contexto, IMapper mapper) : IAtividadeServico
{
    public async Task<bool> Adicionar(AtividadeDto atividadeDto)
    {
        var atividadeExistente = mapper.Map<Atividade>(atividadeDto);

        atividadeExistente.Criar();

        if (atividadeDto.CodigoAnalista != Guid.Empty)
        {
            var analista = contexto.Analistas.FirstOrDefault(a => a.Codigo == atividadeDto.CodigoAnalista);
            if (analista != null)
            {
                atividadeExistente.Analista = analista;
            }
        }

        if (atividadeDto.CodigoLider != Guid.Empty)
        {
            var lider = contexto.Lideres.FirstOrDefault(a => a.Codigo == atividadeDto.CodigoLider);
            if (lider != null)
            {
                atividadeExistente.Lider = lider;
            }
        }

        await contexto.Atividades.AddAsync(atividadeExistente);

        var resultado = await contexto.SaveChangesAsync();

        return resultado > 0;
    }

    public async Task<bool> Editar(AtividadeDto atividadeDto)
    {
        var atividadeExistente = await contexto.Atividades.FirstOrDefaultAsync(o => o.Codigo == atividadeDto.Codigo);

        if (atividadeExistente == null)
        {
            return false;
        }
        
        mapper.Map(atividadeDto, atividadeExistente);

        atividadeExistente.Editar();

        contexto.Atividades.Update(atividadeExistente);

        var resultado = await contexto.SaveChangesAsync();

        return resultado > 0;
    }

    public async Task<bool> Excluir(AtividadeDto atividadeDto)
    {
        var atividadeExistente = await contexto.Atividades.Include(a => a.Historico)
            .FirstOrDefaultAsync(a => a.Codigo == atividadeDto.Codigo);

        if (atividadeExistente == null)
        {
            return false;
        }

        if (atividadeExistente.Historico != null)
        {
            foreach (var historico in atividadeExistente.Historico)
            {
                contexto.Historico.Remove(historico);
            }
        }
        
        contexto.Atividades.Remove(atividadeExistente);

        var resultado = await contexto.SaveChangesAsync();

        return resultado > 0;
    }

    public async Task<IEnumerable<AtividadeDto>?> ConsultarTodas()
    {
        var atividades = await contexto.Atividades
            .AsNoTracking()
            .Include(a => a.Analista)
            .Include(a => a.Lider)
            .Include(a => a.Historico)
            .OrderBy(a => a.Analista!.Nome)
            .ThenBy(a => a.Sistema)
            .ThenBy(a => a.NumeroRedmine)
            .ToListAsync();

        var resultado = mapper.Map<IEnumerable<AtividadeDto>>(atividades);

        return resultado;
    }

    public async Task<AtividadeDto?> ConsultarPorCodigo(Guid codigoAtividade)
    {
        var atividades = await contexto.Atividades
            .AsNoTracking()
            .Where(a => a.Codigo == codigoAtividade)
            .Include(a => a.Analista)
            .Include(a => a.Lider)
            .Include(a => a.Historico)
            .FirstOrDefaultAsync();

        var resultado = mapper.Map<AtividadeDto>(atividades);

        return resultado;
    }

    public async Task<bool> Finalizar(AtividadeDto atividadeDto)
    {
        var atividade = await contexto.Atividades.FirstOrDefaultAsync(a => a.Codigo == atividadeDto.Codigo);

        if (atividade == null)
        {
            return false;
        }

        atividade.Finalizar();

        contexto.Atividades.Update(atividade);

        var resultado = await contexto.SaveChangesAsync();

        return resultado > 0;
    }

    public async Task<bool> Reabrir(AtividadeDto atividadeDto)
    {
        var atividade = await contexto.Atividades.FirstOrDefaultAsync(a => a.Codigo == atividadeDto.Codigo);

        if (atividade == null)
        {
            return false;
        }

        atividade.Reabrir();

        contexto.Atividades.Update(atividade);

        var resultado = await contexto.SaveChangesAsync();

        return resultado > 0;
    }
}