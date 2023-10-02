using xingyi.job.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xingyi.microservices.repository;

namespace xingyi.job.Repository
{
    public interface IJobRepository : IRepository<Job> { }
    public class JobRepository : Repository<JobDbContext, Job>, IJobRepository
    {
        private readonly JobDbContext _context;

        public JobRepository(JobDbContext context) : base(context, context => context.Jobs)
        {
        }

    }

    public interface IQuestionRepository : IRepository<Question> { }
    public class QuestionRepository : Repository<JobDbContext, Question>, IQuestionRepository
    {
        private readonly JobDbContext _context;

        public QuestionRepository(JobDbContext context) : base(context, context => context.Questions)
        {
        }

    }

    public interface ISectionTemplateRepository : IRepository<SectionTemplate> { }
    public class SectionTemplateRespository : Repository<JobDbContext, SectionTemplate>, ISectionTemplateRepository
    {
        private readonly JobDbContext _context;

        public SectionTemplateRespository(JobDbContext context) : base(context, context => context.SectionTemplates)
        {
        }

    }

}
