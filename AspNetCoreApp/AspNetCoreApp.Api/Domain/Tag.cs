using AspNetCoreApp.Api.Domain.Base;

namespace AspNetCoreApp.Api.Domain
{
    public class Tag : BaseEntity<int>
    {  
        public string Name { get; set; }
    }
}
