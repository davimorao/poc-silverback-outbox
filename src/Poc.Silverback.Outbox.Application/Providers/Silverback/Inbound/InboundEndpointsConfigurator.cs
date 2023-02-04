using Poc.Silverback.Outbox.Application.Events;
using Silverback.Messaging.Configuration;
using System;

namespace Poc.Silverback.Outbox.Application.Providers.Silverback.Outbound
{
    public class InboundEndpointsConfigurator : IEndpointsConfigurator
    {
        public void Configure(IEndpointsConfigurationBuilder builder) =>
        builder
            .AddKafkaEndpoints(endpoints => endpoints
                .Configure(config =>
                {
                    config.BootstrapServers = "PLAINTEXT://localhost:9092";
                })
                .AddInbound<OrderCommand>(endpoint => endpoint.ConsumeFrom("order-events")
                                                .Configure(config =>
                                                {
                                                    config.GroupId = "order-consumer";
                                                    config.AllowAutoCreateTopics = true;
                                                })
                                                .OnError(policy => policy.Retry(3, TimeSpan.FromSeconds(1))
                                                                         .ThenSkip()))

                .AddInbound<OrderCommand2>(endpoint => endpoint.ConsumeFrom("order-events-2")
                                                .Configure(config =>
                                                {
                                                    config.GroupId = "order-consumer-2";
                                                    config.AllowAutoCreateTopics = true;
                                                })
                                                .OnError(policy => policy.Retry(3, TimeSpan.FromSeconds(1))
                                                                         .ThenSkip()))
            );
    }
}
