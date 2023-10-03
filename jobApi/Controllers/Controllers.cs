using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using xingyi.job.Models;
using xingyi.job.Repository;
using xingyi.microservices.Controllers;

namespace xingyi.job.Controllers
{
    public class JobController : GenericController<Job,Guid>
    {
        public JobController(IJobRepository repository) : base(repository,j=>j.Id) { }
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Job controller is working");
        }

    }
    public class SectionTemplateController : GenericController<SectionTemplate,Guid>
    {
        public SectionTemplateController(ISectionTemplateRepository repository) : base(repository, st =>st.Id) { }

    }
    public class QuestionController : GenericController<Question,Guid>
    {
        public QuestionController(QuestionRepository repository) : base(repository, q=>q.Id) { }

    }
}