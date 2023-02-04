using Poc.Silverback.Outbox.Application.Events;
using Silverback.Messaging.Configuration;

namespace Poc.Silverback.Outbox.Application.Providers.Silverback.Outbound
{
    public class OutboundEndpointsConfigurator : IEndpointsConfigurator
    {
        public void Configure(IEndpointsConfigurationBuilder builder) =>
            builder
                .AddKafkaEndpoints(endpoints => endpoints
                    .Configure(config =>
                    {
                        config.BootstrapServers = "PLAINTEXT://localhost:9092";
                    })
                    .AddOutbound<OrderCommand>(endpoint => endpoint.ProduceTo("order-events")
                                                                   .ProduceToOutbox())
                    .AddOutbound<OrderCommand2>(endpoint => endpoint.ProduceTo("order-events-2")
                                                                   .ProduceToOutbox()));
    }
}
