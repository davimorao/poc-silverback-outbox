using Microsoft.Extensions.Logging;
using Poc.Silverback.Outbox.Application.Events;

namespace Poc.Silverback.Outbox.Application.Subscribers
{
    public class OrderSubscriber
    {
        private readonly ILogger<OrderSubscriber> _logger;
        public OrderSubscriber(ILogger<OrderSubscriber> logger)
        {
            _logger = logger;
        }

        public void OnMessageReceived(OrderCommand message)
        {
            _logger.LogInformation($"Received Id: {message.Id}, CreatedAt: {message.CreatedAt:dd/MM/yyyy - HH:mm:ss}");
        }

    }
}
