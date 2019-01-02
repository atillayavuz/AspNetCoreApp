using AspNetCoreApp.Api.Domain;
using AspNetCoreApp.Api.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AspNetCoreApp.Api.Infrastructure
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<TaskTag> TaskTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            #region Configuration

            modelBuilder.Entity<Task>().HasMany(c => c.TaskTags)
                .WithOne(cd => cd.Task)
                .HasForeignKey(cd => cd.TaskId);

            modelBuilder.Entity<Tag>().HasMany(c => c.TaskTags)
               .WithOne(cd => cd.Tag)
               .HasForeignKey(cd => cd.TagId);

            modelBuilder.Entity<TaskTag>().HasOne(cd => cd.Task)
               .WithMany(c => c.TaskTags)
               .HasForeignKey(cd => cd.TaskId);

            modelBuilder.Entity<TaskTag>().HasOne(cd => cd.Tag)
              .WithMany(d => d.TaskTags)
              .HasForeignKey(cd => cd.TagId);

            modelBuilder.Entity<TaskTag>().HasKey(cd => new { cd.TagId, cd.TaskId });

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

            modelBuilder.Entity<Task>().HasQueryFilter(item => !EF.Property<bool>(item, "IsDeleted"));
            modelBuilder.Entity<Tag>().HasQueryFilter(item => !EF.Property<bool>(item, "IsDeleted"));

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
                {
                    entity.State = EntityState.Modified;
                    entity.Property("IsDeleted").CurrentValue = true;
                }
            }
        }
    }
}
