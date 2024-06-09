var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<RiskScoreDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("RiskScoreDb")));
// builder.Services.AddStackExchangeRedisCache(options =>
//     options.Configuration = builder.Configuration.GetConnectionString("ClientsCache"));

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

app.Run();