var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddRadzenComponents();
builder.Services.AddBlazoredSessionStorage();

builder.Services.AddScoped<IAutenticacaoServico, AutenticacaoServico>();

builder.Services.AddTransient(sp => new HttpClient
    { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<IAtaServico, AtaClientServico>();
builder.Services.AddTransient<IAtividadeServico, AtividadeClientServico>();
builder.Services.AddTransient<IObservacaoServico, ObservacaoClientServico>();
builder.Services.AddTransient<IProfissionalServico, ProfissionalClientServico>();

builder.Configuration.AddUserSecrets<Program>();

var username = builder.Configuration["Credentials:Username"];
var password = builder.Configuration["Credentials:Password"];

var host = builder.Build();
await host.RunAsync();