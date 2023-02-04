using Silverback.Messaging.Messages;
using System;

namespace Poc.Silverback.Outbox.Application.Events
{
    public class OrderCommand2 : IIntegrationCommand
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
