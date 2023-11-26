namespace Controle.Atividades.Application.Servicos;

public class AtividadeServicoTemp : IAtividadeServicoTemp
{
    public List<Atividade> RetornarAtividades()
    {
        var resultado = new List<Atividade>
        {
            new()
            {
                Codigo = Guid.NewGuid(),
                NumeroRedmine = 3618,
                Descricao = "Investigação para implementação do IP Restriction",
                Sistema = "Todos",
                Analista = new Analista
                    {
                        Codigo = Guid.Parse("6576ead3-4323-4a44-a716-bd9e49066181"),
                        Nome = "Monteiro",
                        Email = "x435599"
                    },
                Historico = new List<Observacao>
                {
                    new()
                    {
                        Codigo = Guid.NewGuid(),
                        Data = new DateTime(2023, 03,20),
                        Registro = "Nesta semana trabalharam: Glaucon, Félix, Cláudio Magno, Alex. Está tendo um consenso entre a equipe de arquitetura que será implementado apenas em Produção."
                    },
                    new()
                    {
                        Codigo = Guid.NewGuid(),
                        Data = new DateTime(2023, 04,10),
                        Registro = "Pioli informou que já foram obtidas as informações dos sistemas com os analistas. Foi convencionado com a equipe de arquitetura que será tratado apenas produção."
                    },
                    new()
                    {
                        Codigo = Guid.NewGuid(),
                        Data = new DateTime(2023, 07,03),
                        Registro = "Esta atividade perdeu a prioridade. Na volta das férias do Pioli obteremos mais detalhes com ele."
                    },
                    new()
                    {
                        Codigo = Guid.NewGuid(),
                        Data = new DateTime(2023, 07,17),
                        Registro = "Pioli informou que de acordo com o grupo de arquitetura será feito apenas em produção. Como haverá migração das aplicações para SF não daremos continuidade na atividade."
                    }
                }

            } ,
            new()
            {
            Codigo = Guid.NewGuid(),
            NumeroRedmine = 5463698,
            Descricao = "POC - Aplicação .NET Framework 4.X com docker / infra SF.",
            Sistema = "Todos",
            Analista = new Analista
            {
                Codigo = Guid.Parse("6576ead3-4323-4a44-a716-bd9e49066181"),
                    Nome = "Monteiro",
                    Email = "x435599"
                },
            Historico = new List<Observacao>
            {
                new()
                {
                    Codigo = Guid.NewGuid(),
                    Data = new DateTime(2023, 03, 27),
                    Registro = "Problema com certificado no módulo internet..."
                    // Continuação do histórico...
                },
                // Outros registros do histórico...
            },
            Status = TipoAbertaFechada.Aberta
        },

        // Terceira atividade (823)
        new()
        {
            Codigo = Guid.NewGuid(),
            NumeroRedmine = 823,
            Descricao = "Anexo não encontrado",
            Sistema = "SAV",
            Analista = new Analista
            {
                    Codigo = Guid.Empty,
                    Nome = "Alex",
                    Email = "x435500"
                },
            Historico = new List<Observacao>
            {
                new()
                {
                    Codigo = Guid.NewGuid(),
                    Data = new DateTime(2022, 12, 12),
                    Registro = "Pausado devido a outras prioridades."
                }
            },
            Status = TipoAbertaFechada.Aberta
        },

        new()
        {
            Codigo = Guid.NewGuid(),
            NumeroRedmine = 2787,
            Descricao = "Investigação sigla faltando na pasta",
            Sistema = "SAV",
            Analista = new Analista
                {
                    Codigo = Guid.Empty,
                    Nome = "Alex",
                    Email = "x435500"
                },
            Historico = new List<Observacao>
            {
                new()
                {
                    Codigo = Guid.NewGuid(),
                    Data = new DateTime(2022, 10, 25),
                    Registro = "Erro identificado pelo Eduardo..."
                },
            },
            Status = TipoAbertaFechada.Fechada

        }
        };

        return resultado;
    }
}