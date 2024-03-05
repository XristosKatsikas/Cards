using Cards.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Cards.Infrastructure.SchemaDefinitions
{
    public class CardEntitySchemaConfiguration : IEntityTypeConfiguration<Card>
    { 
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("Cards", ApplicationDbContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
               .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Color)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.DateCreated)
                .IsRequired();

            builder.Property(x => x.Role)
                .IsRequired();
        }
    }
}
