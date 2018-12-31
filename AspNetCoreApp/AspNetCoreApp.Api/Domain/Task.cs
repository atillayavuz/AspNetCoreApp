using AspNetCoreApp.Api.Domain.Base;

namespace AspNetCoreApp.Api.Domain
{
    public class Task : BaseEntity<int>, IAuditEntity
    {  
        public string Title { get; set; }

        public string Description { get; set; }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
