using InventoryHold.Domain.Interfaces;
using InventoryHold.Domain.Services;
using InventoryHold.Infrastructure.Messaging;
using InventoryHold.Infrastructure.Mongo;
using InventoryHold.Infrastructure.Redis;
using InventoryHold.WebApi.BackgroundServices;
using InventoryHold.WebApi.Extensions;
using InventoryHold.WebApi.Middleware;
using Serilog;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console()
      .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day));

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddScoped<HoldService>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IHoldRepository, HoldRepository>();
builder.Services.AddScoped<ICacheService, RedisCacheService>();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    try
    {
        return ConnectionMultiplexer.Connect("localhost:6379,abortConnect=false");
    }
    catch
    {
        Console.WriteLine("Redis not available - running without cache");
        return null;
    }
});

builder.Services.AddScoped<IMessagePublisher, RabbitMqPublisher>();
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddHostedService<HoldExpiryService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MongoDbContext>();
    await SeedData.InitializeAsync(context);
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("AllowFrontend");

app.MapControllers();

app.Run();