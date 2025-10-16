using TaskListApp.Service.Api;
using TaskListApp.Service.ServiceAuthenticator;
using TaskListWeb;
using TaskListWeb.Pages.Usuario;

var builder = WebApplication.CreateBuilder(args);

// =======================================
// CONFIGURAÇÃO DE SERVIÇOS
// =======================================

// Adiciona suporte ao Blazor Server/Interativo (.NET 8+)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Serviços de injeção de dependência da sua aplicação
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<IUserServiceApi, UserServiceApi>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<TaskListApp.Service.ServiceAuthenticator.IHttpClientFactory, HttpClientFactory>();

// Se você precisar de requisições HTTP para APIs externas:
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7004/") // ajuste conforme sua API
});

var app = builder.Build();

// =======================================
// CONFIGURAÇÃO DO PIPELINE HTTP
// =======================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// =======================================
// CONFIGURAÇÃO DO BLAZOR SERVER / UNIFICADO
// =======================================
// Este é o ponto de entrada do Blazor no .NET 8+
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
