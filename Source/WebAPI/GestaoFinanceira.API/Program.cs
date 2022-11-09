using ElmahCore.Mvc;
using GestaoFinanceira.API.Configurations.Authorization;
using GestaoFinanceira.API.Configurations.Swagger;
using GestaoFinanceira.API.Middleware;
using GestaoFinanceira.Domain.Core.Data;
using GestaoFinanceira.Infra.CrossCutting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
#region Injecao de Dependencia
var settings = builder.Configuration.GetSection("Settings").Get<Settings>();
builder.Services.AddSingleton(settings);
builder.Services.AddServices();
#endregion

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerSetup(settings.ApplicationName);
builder.Services.AddElmah();

builder.Services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
builder.Services.AddResponseCompression(options => { options.Providers.Add<GzipCompressionProvider>(); });

builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseResponseCompression();
app.UsePathBase($"/{settings.ApplicationBasePath}");
app.UseSwaggerSetup();

if (builder.Configuration.GetValue("LogEnvVariables", false))
    Console.WriteLine((builder.Configuration).GetDebugView());

app.UseLoggerMiddleware();

app.UseHttpsRedirection();

app.UseElmah();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();