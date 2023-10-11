using Microservices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using xingyi.microservices.repository;

namespace xingyi.microservices.Controllers
{
    /// <summary>
    /// Please note that for this to work properly you need to 
    /// add Newtonsoft JSON as the serialiser and use JsonSettings
    /// 
    /// services.AddControllers()
    /// .AddNewtonsoftJson(options =>
    ///    {
    ///    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    ///    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    ///});
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    [Route("api/[controller]")]
    [ApiController]
    abstract public class GenericControllerWithNoIdMethods<TEntity, Id, Where> : ControllerBase
        where TEntity : class
        where Where : IRepositoryWhere<TEntity>
    {
        protected readonly IRepository<TEntity, Id, Where> _repository;
        private readonly Func<TEntity, Id> idFn;

        public GenericControllerWithNoIdMethods(IRepository<TEntity, Id, Where> repository, Func<TEntity, Id> idFn)
        {
            _repository = repository;
            this.idFn = idFn;
        }

        [HttpPut()]
        public async Task<IActionResult> Put(TEntity entity)
        {
            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity entity)
        {
            var entityWithId = await _repository.AddAsync(entity);
            Id id = idFn(entityWithId);
            return CreatedAtAction("Get", new { id = id }, entity);
        }

    }
    [Route("api/[controller]")]
    [ApiController]
    abstract public class GenericController<TEntity, Id, Where> : GenericControllerWithNoIdMethods<TEntity, Id, Where>
        where TEntity : class
        where Where : IRepositoryWhere<TEntity>
    {

        public GenericController(IRepository<TEntity, Id, Where> repository, Func<TEntity, Id> idFn) : base(repository, idFn)
        {
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(Id id, [FromQuery] Boolean eagerLoad)
        {
            var entity = await _repository.GetByIdAsync(id, eagerLoad);
            return entity == null ? NotFound() : Ok(entity);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Id id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    abstract public class GenericEmptyWhereController<TEntity, Id, Where> : GenericController<TEntity, Id, Where>
        where TEntity : class
        where Where : IRepositoryWhere<TEntity>
    {
        private readonly Where emptyWhere;

        public GenericEmptyWhereController(IRepository<TEntity, Id, Where> repository, Func<TEntity, Id> idFn, Where emptyWhere)
            : base(repository, idFn)
        {
            this.emptyWhere = emptyWhere;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> GetAll([FromQuery] Boolean eagerLoad)
        {
            var entities = await _repository.GetAllAsync(emptyWhere, eagerLoad);
            return entities == null ? NotFound() : Ok(entities);
        }
    }
}

