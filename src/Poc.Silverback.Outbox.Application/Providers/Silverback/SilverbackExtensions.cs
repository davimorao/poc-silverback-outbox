using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Poc.Silverback.Outbox.Application.Persistance.Contexts;
using Poc.Silverback.Outbox.Application.Providers.Silverback.Outbound;
using Poc.Silverback.Outbox.Application.Subscribers;
using Silverback.Background;
using Silverback.Diagnostics;
using System.Collections.Generic;

namespace Poc.Silverback.Outbox.Application.Providers.Silverback
{
    public static class SilverbackExtensions
    {
        public static IServiceCollection AddSilverbackServices(this IServiceCollection services)
        {
            services.AddSilverback()
                    .UseDbContext<KafkaManagementDbContext>()

                    // Setup the lock manager using the database
                    // to handle the distributed locks.
                    // If this line is omitted the OutboundWorker will still
                    // work without locking. 
                    .AddDbDistributedLockManager()


                    .WithConnectionToMessageBroker(options =>
                        options.AddKafka()
                               .AddOutboxDatabaseTable()
                               .AddOutboxWorker(distributedLockSettings: new DistributedLockSettings(resourceName: "service-sample")))

                    .AddEndpointsConfigurator<InboundEndpointsConfigurator>()
                    .AddEndpointsConfigurator<OutboundEndpointsConfigurator>()
                    .AddSingletonSubscriber<OrderSubscriber>()
                    .AddSingletonSubscriber<OrderSubscriber2>()


                    .WithLogLevels(configurator => configurator.SetLogLevel(CoreLogEvents.FailedToSendDistributedLockHeartbeat.EventId, LogLevel.Debug))
                    ;

            services.AddHealthChecks()
                    .AddConsumersCheck()
                    .AddOutboundEndpointsCheck()
                    .AddOutboxCheck(tags: new List<string>() { "outbox-queue" })
                    ;

            return services;
        }
    }
}
