using AspNetCoreApp.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly TodoContext _context;
        public TagController(TodoContext context)
        {
            _context = context;
        }
    }
}
