using Radzen.Blazor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
      .AddInteractiveServerComponents()
      .AddInteractiveWebAssemblyComponents();

builder.Services.AddDbContextFactory<Contexto>(opcoes => opcoes.UseNpgsql());
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddScoped<IAutenticacaoServico, AutenticacaoServico>();

builder.Services.AddTransient<IAtaServico, AtaServico>();
builder.Services.AddTransient<IAtividadeServico, AtividadeServico>();
builder.Services.AddTransient<IProfissionalServico, ProfissionalServico>();

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddControllers();
builder.Services.AddRadzenComponents();
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode()
   .AddInteractiveWebAssemblyRenderMode()
   .AddAdditionalAssemblies(typeof(Controle.Atividades.Client._Imports).Assembly);

app.Run();