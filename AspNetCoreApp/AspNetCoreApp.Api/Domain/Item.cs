using AspNetCoreApp.Api.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApp.Api.Domain
{
    public class Item : IEntity<int>, IAuditEntity
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
