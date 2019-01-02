using AspNetCoreApp.Api.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApp.Api.Domain
{
    public class Task : BaseEntity<int>,IAuditEntity
    { 
        public string Title { get; set; }

        public string Description { get; set; }
         
        public ICollection<TaskTag> TaskTags { get; set; }
    }
}
