using AspNetCoreApp.Api.Domain.Base;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApp.Api.Domain
{
    public class Tag : BaseEntity<int>, IAuditEntity
    { 
        public string Name { get; set; } 

        public ICollection<TaskTag> TaskTags { get; set; }
    }
}
