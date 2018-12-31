using AspNetCoreApp.Api.Domain;
using AspNetCoreApp.Api.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AspNetCoreApp.Api.Infrastructure
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        { 
        }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Shadow properties

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                  .Where(e => typeof(IAuditEntity).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime>("CreateDate");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime?>("ModifiedDate");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<int>("CreatedBy");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<int?>("ModifiedBy");

                modelBuilder.Entity(entityType.ClrType)
                   .Property<bool?>("IsDeleted");
            }

            #endregion

            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            ApplyAuditInformation();
            return base.SaveChanges();
        }
        private void ApplyAuditInformation()
        {
            var modifiedEntities = ChangeTracker.Entries<IAuditEntity>()
                    .Where(e => e.State == EntityState.Added 
                                || e.State == EntityState.Modified 
                                        || e.State == EntityState.Deleted);

            foreach (var entity in modifiedEntities)
            { 
                if (entity.State == EntityState.Added) 
                    entity.Property("CreateDate").CurrentValue = DateTime.UtcNow; 
                else if (entity.State == EntityState.Modified) 
                    entity.Property("ModifiedDate").CurrentValue = DateTime.UtcNow; 
                else 
                    entity.Property("IsDeleted").CurrentValue = true; 
            }
        }
    }
}
