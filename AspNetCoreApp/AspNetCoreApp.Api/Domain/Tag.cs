using AspNetCoreApp.Api.Domain.Base;
using System.Collections.Generic;

namespace AspNetCoreApp.Api.Domain
{
    public class Tag : BaseAuditEntity<int>
    { 
        public string Name { get; set; } 

        public ICollection<TaskTag> TaskTags { get; set; }
    }
}
