namespace Controle.Atividades.Server.Servicos;

public class AtividadeServerServico(Contexto contexto, IMapper mapper, IMemoryCache memoryCache) : IAtividadeServico
{
    public async Task<bool> Adicionar(AtividadeDto atividadeDto)
    {
        memoryCache.Remove(Constantes.MemoryCacheServerTodasAtividades);

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
        memoryCache.Remove(Constantes.MemoryCacheServerTodasAtividades);

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
        memoryCache.Remove(Constantes.MemoryCacheServerTodasAtividades);

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
        if (memoryCache.TryGetValue(Constantes.MemoryCacheServerTodasAtividades, out IEnumerable<AtividadeDto>? resultado))
        {
            return resultado;
        }

        var atividades = await contexto.Atividades
            .AsNoTracking()
            .Include(a => a.Analista)
            .Include(a => a.Lider)
            .Include(a => a.Historico)
            .OrderBy(a => a.Analista!.Nome)
            .ThenBy(a => a.Sistema)
            .ThenBy(a => a.NumeroRedmine)
            .ToListAsync();

        resultado = mapper.Map<IEnumerable<AtividadeDto>>(atividades);

        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(5));
        var consultarTodas = resultado.ToList();
        memoryCache.Set(Constantes.MemoryCacheServerTodasAtividades, consultarTodas, cacheEntryOptions);

        return consultarTodas;
    }

    public async Task<AtividadeDto?> ConsultarPorCodigo(Guid codigoAtividade)
    {
        memoryCache.Remove(Constantes.MemoryCacheServerTodasAtividades);

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
        memoryCache.Remove(Constantes.MemoryCacheServerTodasAtividades);

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
        memoryCache.Remove(Constantes.MemoryCacheServerTodasAtividades);

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

   

    public async Task<IEnumerable<AtividadeDto>?> ConsultarPorData(DateTime dataInicio, DateTime pdataFim)
    {
         var dataFim = pdataFim == DateTime.MinValue? DateTime.Today: pdataFim;

        if (dataInicio.Date == dataFim.Date)
        {
            dataFim = dataFim.AddDays(1);
        }

        var atividades = await contexto.Atividades
            .Where(atividade => atividade.Lider != null &&
                                atividade.Lider.Codigo == Guid.Parse("611f0467-8cc5-4a13-90f1-ae00c3e91e5a") &&
                                ((atividade.DtCriacao >= dataInicio && atividade.DtCriacao < dataFim) ||
                                 (atividade.DtModificacao >= dataInicio && atividade.DtModificacao < dataFim)))
            .AsNoTracking()
            .Include(a => a.Analista)
            .Include(a => a.Lider)
            .Include(a => a.Historico).ToListAsync();

        var resultado = mapper.Map<List<AtividadeDto>>(atividades);

        return resultado;
    }


}