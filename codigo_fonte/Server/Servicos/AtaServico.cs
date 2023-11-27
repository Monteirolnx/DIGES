namespace Controle.Atividades.Server.Servicos;

public class AtaServico(Contexto contexto, IMapper mapper) : IAtaServico
{
    public async Task<AtaDto?> Gerar()
    {
        var atividades = await contexto.Atividades
            .AsNoTracking()
            .Include(a => a.Analista)
            .Include(a => a.Lider)
            .OrderBy(a => a.Analista!.Nome)
            .ToListAsync();

        foreach (var atividade in atividades)
        {
            var historicoMaisRecente = await contexto.Historico
                .Where(h => h.CodigoAtividade == atividade.Codigo)
                .OrderByDescending(h => h.Data)
                .FirstOrDefaultAsync();

            atividade.Historico = new List<Observacao>();
            if (historicoMaisRecente != null)
            {
                atividade.Historico.Add(historicoMaisRecente);
            }
        }

        var resultado = new AtaDto();

        var atividadesCriadas = atividades
            .Where(a => a.DtCriacao.Date == DateTime.Today && a.NumeroRedmine.HasValue);

        var atividadesAtualizadas = atividades
            .Where(a => a.DtModificacao.Date == DateTime.Today && a.DtCriacao.Date != DateTime.Today && a.NumeroRedmine.HasValue);

        var atividadesFinalizadas = atividades
            .Where(a => a.DtModificacao.Date == DateTime.Today && a is { Status: TipoAbertaFechada.Fechada, NumeroRedmine: not null });

        var atividadesCriadasList = atividadesCriadas.ToList();
        var atividadesAtualizadasList = atividadesAtualizadas.ToList();
        var atividadesFinalizadasList = atividadesFinalizadas.ToList();

        resultado.AtividadesCriadas = string.Join(", ", atividadesCriadasList.Select(a => a.NumeroRedmine!.Value));
        resultado.AtividadesAtualizadas = string.Join(", ", atividadesAtualizadasList.Select(a => a.NumeroRedmine!.Value));
        resultado.AtividadesFinalizadas = string.Join(", ", atividadesFinalizadasList.Select(a => a.NumeroRedmine!.Value));

        resultado.AtividadesDto = atividadesCriadasList
            .Union(atividadesAtualizadasList)
            .Union(atividadesFinalizadasList)
            .Select(mapper.Map<AtividadeDto>);

        return resultado;
    }
}