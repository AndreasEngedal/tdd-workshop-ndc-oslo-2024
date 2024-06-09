using ClientRiskEvaluator.AcceptanceTests.Extensions;
using ClientRiskEvaluator.Api;
using ClientRiskEvaluator.Infrastructure;
using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace ClientRiskEvaluator.AcceptanceTests;

public class ApiFactory : WebApplicationFactory<IApiAssemblyMarker>,
    IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithWaitStrategy(Wait.ForUnixContainer().UntilCommandIsCompleted("pg_isready"))
        .Build();

    private readonly RedisContainer _redisContainer = new RedisBuilder()
        .Build();
    
    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
        await _redisContainer.StartAsync();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureServices(
            services =>
            {
                services.RemoveDbContext<RiskScoreDbContext>();
                services.AddDbContext<RiskScoreDbContext>(options =>
                    options.UseNpgsql(_postgreSqlContainer.GetConnectionString()));
                services.EnsureDbCreated<RiskScoreDbContext>();
                
                services.Remove<IDistributedCache>();
                services.AddStackExchangeRedisCache(options =>
                    options.Configuration = _redisContainer.GetConnectionString());
            });
    }

    public async Task DisposeAsync()
    {
        await _postgreSqlContainer.DisposeAsync();
        await _postgreSqlContainer.DisposeAsync();
    }
}