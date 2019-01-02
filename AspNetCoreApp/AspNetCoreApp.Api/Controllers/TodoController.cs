using AspNetCoreApp.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Task = AspNetCoreApp.Api.Domain.Task;

namespace AspNetCoreApp.Api.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        public TodoController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Task>> Get()
        {
            var result = _context.Tasks.ToList();

            if (result == null)
            {
                return NotFound();
            }

            return new ObjectResult(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _context.Tasks.FirstOrDefault(t => t.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Task task)
        {
            if (task == null)
            {
                return BadRequest();
            }

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Task task)
        {
            if (task == null && task.Id != id)
            {
                return BadRequest();
            }

            var taskEntity = _context.Tasks.FirstOrDefault(x => x.Id == id);
            if (taskEntity == null)
            {
                return NotFound();
            }

            taskEntity.Title = task.Title;
            taskEntity.Description = task.Description;
            _context.Tasks.Update(taskEntity);
            _context.SaveChanges();
             
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var taskEntity =_context.Tasks.FirstOrDefault(x => x.Id == id);
            if (taskEntity == null)
            {
                return NotFound();
            }
            _context.Tasks.Remove(taskEntity);
            _context.SaveChanges();

            return new NoContentResult(); 
        }
    }
}