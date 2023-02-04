using Microsoft.Extensions.Logging;
using Poc.Silverback.Outbox.Application.Events;

namespace Poc.Silverback.Outbox.Application.Subscribers
{
    public class OrderSubscriber2
    {
        private readonly ILogger<OrderSubscriber2> _logger;
        public OrderSubscriber2(ILogger<OrderSubscriber2> logger)
        {
            _logger = logger;
        }

        public void OnMessageReceived(OrderCommand2 message)
        {
            _logger.LogInformation($"Received Id: {message.Id}, CreatedAt: {message.CreatedAt:dd/MM/yyyy - HH:mm:ss}");
        }

    }
}
