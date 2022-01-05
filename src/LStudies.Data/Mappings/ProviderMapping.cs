using LStudies.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/* EF FluentAPI configuration */
namespace LStudies.Data.Mappings
{
    public class ProviderMapping : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Document)
               .IsRequired()
               .HasColumnType("varchar(14)");

            // 1 : 1 => Provider : Address (provider has one address)
            builder.HasOne(p => p.Adress)
                .WithOne(a => a.Provider);

            // 1 : N => Provider : Products (provider has N products)
            builder.HasMany(p => p.Products)
                .WithOne(prd => prd.Provider)
                .HasForeignKey(prd => prd.ProviderId);

            builder.ToTable("Providers");
        }
    }
}
