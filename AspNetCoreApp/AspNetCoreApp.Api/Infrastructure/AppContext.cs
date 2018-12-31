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

        public DbSet<Task> Items { get; set; }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Configuration

            modelBuilder.Entity<Task>().Property(p => p.Title).IsRequired();
            modelBuilder.Entity<Task>().Property(p => p.Description).IsRequired();
            modelBuilder.Entity<Task>().Property(p => p.TagId).IsRequired();

            modelBuilder.Entity<Tag>().Property(p => p.Name).IsRequired();
            
            #endregion

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

            #region QueryFilter

            modelBuilder.Entity<IAuditEntity>()
                .HasQueryFilter(post => EF.Property<bool>(post, "IsDeleted") == false);

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
                {
                    entity.Property("CreateDate").CurrentValue = DateTime.UtcNow;
                    entity.Property("IsDeleted").CurrentValue = false;
                }
                else if (entity.State == EntityState.Modified)
                    entity.Property("ModifiedDate").CurrentValue = DateTime.UtcNow;
                else //EntityState.Deleted
                    entity.Property("IsDeleted").CurrentValue = true;
            }
        }
    }
}
