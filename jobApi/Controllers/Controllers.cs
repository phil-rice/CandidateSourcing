using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using xingyi.application;
using xingyi.job.Models;
using xingyi.job.Repository;
using xingyi.microservices.Controllers;


namespace xingyi.job.Controllers
{
    public class JobAndAppController : GenericController<Job, Guid, JobAndAppWhere>
    {
        public JobAndAppController(IJobAndAppRepository repository) : base(repository, j => j.Id)
        {

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetAll([FromQuery] string? owner, [FromQuery] Boolean eagerLoad)
        {
            var entities = await _repository.GetAllAsync(new JobAndAppWhere { Owner = owner }, eagerLoad);
            return entities == null ? NotFound() : Ok(entities);
        }

    }
    public class JobController : GenericController<Job, Guid, JobWhere>
    {
        public JobController(IJobRepository repository) : base(repository, j => j.Id) { }
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Job controller is working");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetAll([FromQuery] string? owner, [FromQuery] Boolean eagerLoad)
        {
            var entities = await _repository.GetAllAsync(new JobWhere { Owner = owner }, eagerLoad);
            return entities == null ? NotFound() : Ok(entities);
        }


    }
    public class SectionTemplateController : GenericController<SectionTemplate, Guid, SectionTemplateWhere>
    {
        public SectionTemplateController(ISectionTemplateRepository repository) : base(repository, st => st.Id) { }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SectionTemplate>>> GetAll([FromQuery] string? owner, [FromQuery] Boolean eagerLoad)
        {
            var entities = await _repository.GetAllAsync(new SectionTemplateWhere { Owner = owner }, eagerLoad);
            return entities == null ? NotFound() : Ok(entities);
        }
    }
    public class QuestionController : GenericEmptyWhereController<Question, Guid, QuestionWhere>
    {
        public QuestionController(QuestionRepository repository) : base(repository, q => q.Id, new QuestionWhere()) { }

    }

    public class ApplicationController : GenericEmptyWhereController<Application, Guid, ApplicationWhere>
    {
        public ApplicationController(IApplicationRepository repository) : base(repository, j => j.Id, new ApplicationWhere()) { }
        
    }
    public class SectionController : GenericController<Section, Guid, SectionWhere>
    {
        public SectionController(ISectionRepository repository) : base(repository, st => st.Id) { }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SectionTemplate>>> GetAll([FromQuery] string? email, [FromQuery] Boolean eagerLoad)
        {
            var entities = await _repository.GetAllAsync(new SectionWhere { Email = email }, eagerLoad);
            return entities == null ? NotFound() : Ok(entities);
        }

    }
    public class AnswerController : GenericEmptyWhereController<Answer, Guid, AnswerWhere>
    {
        public AnswerController(IAnswersRepository repository) : base(repository, q => q.Id, new AnswerWhere()) { }

    }
}
