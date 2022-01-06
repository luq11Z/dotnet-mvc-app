using LStudies.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

/* EF DbContext configuration*/
namespace LStudies.Data.Context
{
    public class LStudiesDbContext : DbContext
    {
        public LStudiesDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Provider> Providers { get; set; }

        internal Address AsNoTracking()
        {
            throw new NotImplementedException();
        }

        /* This method is called when the models are created. Here we can pass our own configurations context*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Limit varchar column types to size 255*/
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(255)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LStudiesDbContext).Assembly);

            /* Search for relationships within modelBuilder getting the entity types and identify the relations 
             * and update the delete behavior in order to prevent removing its children (cascade delete) */
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
