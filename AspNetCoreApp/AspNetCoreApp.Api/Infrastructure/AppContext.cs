using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApp.Api.Infrastructure
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            // Todo : Adds shadow properties
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            ApplyAuditInformation();
            return base.SaveChanges();
        }
        private void ApplyAuditInformation()
        {

        }
    }
}
