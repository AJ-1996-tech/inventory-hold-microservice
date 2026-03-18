using InventoryHold.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryHold.Infrastructure.Messaging
{
    public class RabbitMqPublisher : IMessagePublisher
    {
        public Task PublishAsync<T>(T message)
        {
          
            Console.WriteLine($"Event Published: {typeof(T).Name}");
            return Task.CompletedTask;
        }
    }
}
