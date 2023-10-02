using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using xingyi.microservices.repository;

namespace xingyi.job.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   abstract public class GenericController<TEntity> : ControllerBase where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;

        public GenericController(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            if (entities == null || !entities.Any())
                return NotFound();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, TEntity entity)
        {
            // Assumes that TEntity has a property "Id" of type Guid
            var entityId = (Guid)typeof(TEntity).GetProperty("Id").GetValue(entity);
            if (id != entityId)
                return BadRequest();

            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity entity)
        {
            await _repository.AddAsync(entity);
            return CreatedAtAction("Get", new { id = typeof(TEntity).GetProperty("Id").GetValue(entity) }, entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (deleted)
                return NoContent();
            return NotFound();
        }
    }
}
