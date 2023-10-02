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

namespace xingyi.job.Controllers
{
    public class JobController : GenericController<Job>
    {
        public JobController(IJobRepository repository) : base(repository) { }
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Job controller is working");
        }

    }
    public class SectionTemplateController : GenericController<SectionTemplate>
    {
        public SectionTemplateController(ISectionTemplateRepository repository) : base(repository) { }

    }
    public class QuestionController : GenericController<Question>
    {
        public QuestionController(QuestionRepository repository) : base(repository) { }

    }
}
