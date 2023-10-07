using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using xingyi.application.Repository;
using xingyi.job.Models;
using xingyi.job.Repository;
using xingyi.microservices.Controllers;
using static xingyi.application.Repository.ApplicationRepository;

namespace xingyi.application.Controllers
{
    public class ApplicationController : GenericController<Application,Guid>
    {
        public ApplicationController(IApplicationRepository repository) : base(repository,j=>j.Id) { }
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Application controller is working");
        }

    }
    public class SectionController : GenericController<Section,Guid>
    {
        public SectionController(ISectionRepository repository) : base(repository, st =>st.Id) { }

    }
    public class QuestionController : GenericController<Question,Guid>
    {
        public QuestionController(QuestionRepository repository) : base(repository, q=>q.Id) { }

    }
}
