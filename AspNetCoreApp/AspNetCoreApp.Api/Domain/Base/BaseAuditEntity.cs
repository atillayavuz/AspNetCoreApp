using System;

namespace AspNetCoreApp.Api.Domain.Base
{
    public class BaseAuditEntity<T> : BaseEntity<T>, IAuditEntity where T : struct
    {
        public DateTime CreateDate { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
