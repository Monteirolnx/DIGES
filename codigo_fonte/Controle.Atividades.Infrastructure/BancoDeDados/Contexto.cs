namespace Controle.Atividades.Infrastructure.BancoDeDados;

public class Contexto(IConfiguration configuration) : DbContext
{
    public virtual DbSet<Analista> Analistas => Set<Analista>();

    public virtual DbSet<Atividade> Atividades => Set<Atividade>();

    public virtual DbSet<Lider> Lideres => Set<Lider>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var databaseUrl = configuration["DATABASE_URL"];

        var databaseUri = new Uri(databaseUrl ?? throw new InvalidOperationException());
        var userInfo = databaseUri.UserInfo.Split(':');

        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = databaseUri.Host,
            Port = databaseUri.Port,
            Username = userInfo[0],
            Password = userInfo[1],
            Database = databaseUri.LocalPath.TrimStart('/')
        };

        optionsBuilder.UseNpgsql(builder.ToString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Analista>().HasData(
            new Analista { Codigo = Guid.NewGuid(), Nome = "Alex Kaam", Email = "askaam@sf.prefeitura.sp.gov.br", Status = TipoAtivoInativo.Ativo },
            new Analista { Codigo = Guid.NewGuid(), Nome = "Arthur Vieira", Email = "avieira@sf.prefeitura.sp.gov.br", Status = TipoAtivoInativo.Ativo },
            new Analista { Codigo = Guid.NewGuid(), Nome = "Cláudio Almeida", Email = "caandrade@sf.prefeitura.sp.gov.br", Status = TipoAtivoInativo.Ativo },
            new Analista { Codigo = Guid.NewGuid(), Nome = "Cláudio Magno", Email = "csmagno@sf.prefeitura.sp.gov.br", Status = TipoAtivoInativo.Ativo },
            new Analista { Codigo = Guid.NewGuid(), Nome = "Félix Silva", Email = "fmsilva@sf.prefeitura.sp.gov.br", Status = TipoAtivoInativo.Ativo },
            new Analista { Codigo = Guid.NewGuid(), Nome = "Gabriel Capetini", Email = "gcsantanna@sf.prefeitura.sp.gov.br", Status = TipoAtivoInativo.Ativo },
            new Analista { Codigo = Guid.NewGuid(), Nome = "Glaucon Marrafon", Email = "gmarrafon@sf.prefeitura.sp.gov.br", Status = TipoAtivoInativo.Ativo },
            new Analista { Codigo = Guid.NewGuid(), Nome = "Luis Barreto", Email = "lcebarreto@sf.prefeitura.sp.gov.br", Status = TipoAtivoInativo.Ativo },
            new Analista { Codigo = Guid.NewGuid(), Nome = "Luis Monteiro", Email = "lfmleitao@sf.prefeitura.sp.gov.br", Status = TipoAtivoInativo.Ativo },
            new Analista { Codigo = Guid.NewGuid(), Nome = "Rafael Miranda", Email = "rsmiranda@sf.prefeitura.sp.gov.br", Status = TipoAtivoInativo.Ativo },
            new Analista { Codigo = Guid.NewGuid(), Nome = "Renan Guedes", Email = "rgsouza@sf.prefeitura.sp.gov.br", Status = TipoAtivoInativo.Ativo },
            new Analista { Codigo = Guid.NewGuid(), Nome = "Thiago Maximiliano", Email = "tsmachado@sf.prefeitura.sp.gov.br", Status = TipoAtivoInativo.Ativo }
        );

        modelBuilder.Entity<Lider>().HasData(
            new Lider { Codigo = Guid.NewGuid(), Nome = "Calemino Mendes", Email = "camendes@sf.prefeitura.sp.gov.br", Status = TipoAtivoInativo.Ativo },
            new Lider { Codigo = Guid.NewGuid(), Nome = "Fernando Brambilla", Email = "fbrambilla@sf.prefeitura.sp.gov.br", Status = TipoAtivoInativo.Ativo },
            new Lider { Codigo = Guid.NewGuid(), Nome = "Rafael Pioli", Email = "rpioli@sf.prefeitura.sp.gov.br", Status = TipoAtivoInativo.Ativo },
            new Lider { Codigo = Guid.NewGuid(), Nome = "Rodrigo Guerra", Email = "rodrigomallmann@sf.prefeitura.sp.gov.br", Status = TipoAtivoInativo.Ativo }
        );

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Contexto).Assembly);
    }
}