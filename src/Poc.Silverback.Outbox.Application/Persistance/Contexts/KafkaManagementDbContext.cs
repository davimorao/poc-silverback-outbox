using Microsoft.EntityFrameworkCore;
using Silverback.Database.Model;

namespace Poc.Silverback.Outbox.Application.Persistance.Contexts
{
    public class KafkaManagementDbContext : DbContext
    {
        public KafkaManagementDbContext(DbContextOptions options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<OutboxMessage> Outbox { get; set; } = null!;
        public DbSet<InboundLogEntry> InboundMessages { get; set; } = null!;
        public DbSet<StoredOffset> StoredOffsets { get; set; } = null!;
        public DbSet<Lock> Locks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InboundLogEntry>()
                        .HasKey(t => new { t.MessageId, t.ConsumerGroupName });
        }
    }
}
