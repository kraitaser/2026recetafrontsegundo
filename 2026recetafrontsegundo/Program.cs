using _2026recetafrontsegundo.Auth;
using _2026recetafrontsegundo.Components;
using _2026recetafrontsegundo.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Components.Authorization;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

var redisConnection = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING");

if (string.IsNullOrWhiteSpace(redisConnection))
{
    throw new InvalidOperationException(
        "No se ha configurado REDIS_CONNECTION_STRING.");
}

var redis = ConnectionMultiplexer.Connect(redisConnection);

builder.Services.AddDataProtection()
    .SetApplicationName("2026recetafrontsegundo")
    .PersistKeysToStackExchangeRedis(
        redis,
        "recetario:data-protection-keys");

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = ".Recetario.Antiforgery.v2";
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthorizationCore();

builder.Services.AddHttpClient<ApiClient>(client =>
{
    var baseUrl = Environment.GetEnvironmentVariable("API_URL")
        ?? builder.Configuration["Api:BaseUrl"]
        ?? throw new InvalidOperationException("No se ha configurado Api:BaseUrl.");

    baseUrl = baseUrl.TrimEnd('/');
    if (baseUrl.EndsWith("/api", StringComparison.OrdinalIgnoreCase))
    {
        baseUrl = baseUrl[..^4];
    }

    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(
    sp => sp.GetRequiredService<AuthStateProvider>());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute(
    "/not-found",
    createScopeForStatusCodePages: true);

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
