namespace Controle.Atividades.Server.Servicos;

public class ProfissionalServico(Contexto contexto, IMapper mapper) : IProfissionalServico
{
    public async Task<List<AnalistaDto>?> ConsultaTodosAnalistas()
    {
        var analistas = await contexto.Analistas.OrderBy(a=> a.Nome).ToListAsync();

        var resultado = mapper.Map<List<AnalistaDto>>(analistas);

        return resultado;
    }

    public async Task<List<LiderDto>?> ConsultaTodosLideres()
    {
        var lideres = await contexto.Lideres.OrderBy(a => a.Nome).ToListAsync();

        var resultado = mapper.Map<List<LiderDto>>(lideres);

        return resultado;
    }
}