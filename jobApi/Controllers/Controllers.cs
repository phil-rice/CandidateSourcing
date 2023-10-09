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
    public class JobAndAppController : GenericController<Job,Guid, JobWhere>
    {
        public JobAndAppController(IJobAndAppRepository repository) : base(repository,j=>j.Id) { }

    }
    public class JobController : GenericController<Job,Guid, JobWhere>
    {
        public JobController(IJobRepository repository) : base(repository,j=>j.Id) { }
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Job controller is working");
        }

    }
    public class SectionTemplateController : GenericController<SectionTemplate,Guid, SectionTemplateWhere>
    {
        public SectionTemplateController(ISectionTemplateRepository repository) : base(repository, st =>st.Id) { }

    }
    public class QuestionController : GenericController<Question,Guid, QuestionWhere>
    {
        public QuestionController(QuestionRepository repository) : base(repository, q=>q.Id) { }

    }

    public class ApplicationController : GenericController<Application, Guid, ApplicationWhere>
    {
        public ApplicationController(IApplicationRepository repository) : base(repository, j => j.Id) { }


    }
    public class SectionController : GenericController<Section, Guid, SectionWhere>
    {
        public SectionController(ISectionRepository repository) : base(repository, st => st.Id) { }

    }
    public class AnswerController : GenericController<Answer, Guid, AnswerWhere>
    {
        public AnswerController(IAnswersRepository repository) : base(repository, q => q.Id) { }

    }
}
