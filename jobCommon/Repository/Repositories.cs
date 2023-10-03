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
    public interface IJobRepository : IRepository<Job, Guid> { }
    public class JobRepository : Repository<JobDbContext, Job, Guid>, IJobRepository
    {
        public JobRepository(JobDbContext context) :
            base(context, context => context.Jobs,
                  id => j => j.Id == id,
                  set => set.Include(j => j.JobSectionTemplates)
                            .ThenInclude(jst => jst.SectionTemplate)
                            .ThenInclude(st => st.Questions))
        {
        }

    }

    public interface IQuestionRepository : IRepository<Question, Guid> { }
    public class QuestionRepository : Repository<JobDbContext, Question, Guid>, IQuestionRepository
    {

        public QuestionRepository(JobDbContext context) : base(context,
            context => context.Questions,
            id => q => q.Id == id,
            set => set)
        {
        }

    }

    public interface ISectionTemplateRepository : IRepository<SectionTemplate, Guid> { }
    public class SectionTemplateRespository : Repository<JobDbContext, SectionTemplate, Guid>, ISectionTemplateRepository
    {
        public SectionTemplateRespository(JobDbContext context) :
    base(context,
        context => context.SectionTemplates,
        id => st => st.Id == id,
        set => set)
        {
        }

    }

}
