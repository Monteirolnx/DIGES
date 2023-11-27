var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddRadzenComponents();

builder.Services.AddScoped<IAutenticacaoServico, AutenticacaoServico>();

builder.Services.AddTransient(sp => new HttpClient
    { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<IAtaServico, AtaServicoClient>();
builder.Services.AddTransient<IAtividadeServicoTemp, AtividadeServicoTemp>();
builder.Services.AddTransient<IAtividadeServico, AtividadeServicoClient>();
builder.Services.AddTransient<IProfissionalServico, ProfissionalServicoClient>();

var host = builder.Build();
await host.RunAsync();