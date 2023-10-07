using xingyi.job.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using xingyi.job;
using xingyi.microservices.repository;

namespace xingyi.application.Repository
{
    public interface IApplicationRepository : IRepository<Application, Guid> { }
    public class ApplicationRepository : Repository<ApplicationDbContext, Application, Guid>, IApplicationRepository
    {
        public ApplicationRepository(ApplicationDbContext context) :
            base(context, context => context.Applications,
                  id => j => j.Id == id,
                  set => set.Include(a => a.Sections)
                            .ThenInclude(s => s.Answers))
        {

        }


        public interface ISectionRepository : IRepository<Section, Guid> { }
        public class SectionRepository : Repository<ApplicationDbContext, Section, Guid>, ISectionRepository
        {
            public SectionRepository(ApplicationDbContext context) :
        base(context,
            context => context.Sections,
            id => st => st.Id == id,
            set => set.Include(s => s.Answers))
            {
            }

        }
        public interface IAnswersRepository : IRepository<Answer, Guid> { }
        public class AnswersRepository : Repository<ApplicationDbContext, Answer, Guid>,IAnswersRepository
        {
            public AnswersRepository(ApplicationDbContext context) :
        base(context,
            context => context.Answers,
            id => st => st.Id == id,
            set => set)
            {
            }

        }

    }
}