using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Poc.Silverback.Outbox.Application.Persistance.Contexts;

namespace Poc.Silverback.Outbox.Application.Persistance
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddDbContext<KafkaManagementDbContext>(options =>
                     options.UseSqlServer("Initial Catalog=cd-kafka-management; Data Source=localhost; User Id=sa; Password=Password@123;"));

            return services;
        }
    }
}
