using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApp.Api.Domain.Base
{
    public abstract class BaseEntity<T> where T : struct
    {
        [Key]
        T Id { get; set; }
    }
}
