using InventoryHold.Domain.Interfaces;
using InventoryHold.Domain.Services;
using InventoryHold.Infrastructure.Messaging;
using InventoryHold.Infrastructure.Mongo;
using InventoryHold.Infrastructure.Redis;

namespace InventoryHold.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<HoldService>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IHoldRepository, HoldRepository>();
            services.AddScoped<ICacheService, RedisCacheService>();
            services.AddScoped<IMessagePublisher, RabbitMqPublisher>();
        }
    }
}
