using InventoryHold.Contracts.Events;
using InventoryHold.Domain.Interfaces;

namespace InventoryHold.WebApi.BackgroundServices
{
    public class HoldExpiryService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<HoldExpiryService> _logger;

        public HoldExpiryService(IServiceProvider serviceProvider, ILogger<HoldExpiryService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();

                var holdRepo = scope.ServiceProvider.GetRequiredService<IHoldRepository>();
                var inventoryRepo = scope.ServiceProvider.GetRequiredService<IInventoryRepository>();
                var publisher = scope.ServiceProvider.GetRequiredService<IMessagePublisher>();

                var holds = await holdRepo.GetExpiredHoldsAsync();

                foreach (var hold in holds)
                {
                    _logger.LogInformation($"Expiring hold {hold.Id}");

                    foreach (var item in hold.Items)
                    {
                        await inventoryRepo.RestoreStockAsync(item.ProductId, item.Quantity);
                    }

                    hold.IsReleased = true;

                    await holdRepo.UpdateAsync(hold);

                    await publisher.PublishAsync(new HoldExpiredEvent(hold.Id));
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
