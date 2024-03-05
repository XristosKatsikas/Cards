using Cards.Domain.Entities;
using Cards.Domain.Repositories.Abstractions;
using Cards.Infrastructure.SchemaDefinitions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure
{
    /// <summary>
    /// Declare the identity data context by extending the IdentityDbContext class. 
    /// IdentityDbContext is used by EF Core to locate and access the data source used as the persistent user store.
    /// </summary>
    sealed public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "CardsDatabase";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Card> Cards { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(cancellationToken);
            return true;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CardEntitySchemaConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
