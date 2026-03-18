using InventoryHold.Domain.Entities;
using InventoryHold.Domain.Interfaces;
using InventoryHold.Domain.Services;
using InventoryHold.Domain.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryHold.UnitTests.Services
{
    public class HoldServiceTests
    {
        private readonly Mock<IInventoryRepository> _inventoryMock = new();
        private readonly Mock<IHoldRepository> _holdMock = new();
        private readonly Mock<IMessagePublisher> _publisherMock = new();
        private readonly Mock<ICacheService> _cacheMock = new();

        private HoldService CreateService()
        {
            return new HoldService(
                _inventoryMock.Object,
                _holdMock.Object,
                _publisherMock.Object,
                _cacheMock.Object);
        }


        [Fact]
        public async Task CreateHold_Should_Succeed_When_Inventory_Available()
        {
            _inventoryMock.Setup(x => x.DeductStockAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(true);

            var service = CreateService();

            var result = await service.CreateHoldAsync(new List<HoldItem>
             {
                  new HoldItem { ProductId = "1", Quantity = 2 }
              });

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateHold_Should_Fail_When_Inventory_Insufficient()
        {
            _inventoryMock.Setup(x => x.DeductStockAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(false);

            var service = CreateService();

            await Assert.ThrowsAsync<Exception>(() =>
                service.CreateHoldAsync(new List<HoldItem>
                {
            new HoldItem { ProductId = "1", Quantity = 100 }
                }));
        }

        [Fact]
        public async Task GetHold_Should_Throw_When_NotFound()
        {
            _holdMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Hold)null);

            var service = CreateService();

            await Assert.ThrowsAsync<Exception>(() => service.GetHoldAsync("123"));
        }

        [Fact]
        public async Task GetHold_Should_Throw_When_Expired()
        {
            _holdMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new Hold
                {
                    Expiry = DateTime.UtcNow.AddMinutes(-1)
                });

            var service = CreateService();

            await Assert.ThrowsAsync<Exception>(() => service.GetHoldAsync("123"));
        }

        [Fact]
        public async Task ReleaseHold_Should_Restore_Inventory()
        {
            _holdMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new Hold
                {
                    Items = new List<HoldItem>
                    {
                new HoldItem { ProductId = "1", Quantity = 2 }
                    }
                });

            var service = CreateService();

            await service.ReleaseHoldAsync("1");

            _inventoryMock.Verify(x => x.RestoreStockAsync("1", 2), Times.Once);
        }

    }
}
