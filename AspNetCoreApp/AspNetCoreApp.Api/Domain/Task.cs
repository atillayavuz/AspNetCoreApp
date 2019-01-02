using AspNetCoreApp.Api.Domain.Base;
using System.Collections.Generic;

namespace AspNetCoreApp.Api.Domain
{
    public class Task : BaseAuditEntity<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<TaskTag> TaskTags { get; set; }
    }
}
