﻿using AspNetCoreApp.Api.Application.Mapping;
using AspNetCoreApp.Api.Dto;
using AspNetCoreApp.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TodoContext _context; 
        public TaskController(TodoContext context )
        {
            _context = context; 
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<IEnumerable<TaskDto>> Get()
        {
            var result = _context.Tasks.ToList();

            if (result == null)
            {
                return NotFound();
            }

            return new ObjectResult(result.Select(s => s.MapToTaskDto()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Get(int id)
        {
            var result = _context.Tasks.FirstOrDefault(t => t.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return new ObjectResult(result.MapToTaskDto());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Post([FromBody] TaskDto model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var task = model.MapToTask();

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public IActionResult Put([FromBody] TaskDto model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var taskEntity = _context.Tasks.FirstOrDefault(x => x.Id == model.Id);
            if (taskEntity == null)
            {
                return NotFound();
            }
              
            taskEntity.Title = model.Title;
            taskEntity.Description = model.Description;
            _context.Tasks.Update(taskEntity);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Delete(int id)
        {
            var taskEntity = _context.Tasks.FirstOrDefault(x => x.Id == id);
            if (taskEntity == null)
            {
                return NotFound();
            }
            _context.Tasks.Remove(taskEntity);
            _context.SaveChanges();

            return Ok();
        }
    }
}