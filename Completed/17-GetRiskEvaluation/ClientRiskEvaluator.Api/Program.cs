using ClientRiskEvaluator;
using ClientRiskEvaluator.Api;
using ClientRiskEvaluator.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IClientRiskEvaluator, ClientRiskEvaluator.ClientRiskEvaluator>();
builder.Services.AddScoped<IBlockList, RedisBlockList>();
builder.Services.AddScoped< IClientRiskEvaluationStore, PostgresClientRiskEvaluationStore>();
builder.Services.AddDbContext<RiskScoreDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("RiskScoreDb")));
builder.Services.AddStackExchangeRedisCache(options =>
    options.Configuration = builder.Configuration.GetConnectionString("ClientsCache"));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add endpoint...
app.MapPost("/risk-evaluation", RiskEvaluationEndpoint.HandleAsync);
app.MapGet("/risk-evaluation/{id}", GetEvaluationEndpoint.HandleAsync);

app.Run();