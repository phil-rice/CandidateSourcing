
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xingyi.common;
using xingyi.job;
using xingyi.job.Models;
using xingyi.job.Repository;
using xingyi.test.generic;
using xingyi.tests.generic;
using xingyi.tests.job;
using xingyi.tests.questions;

namespace xingyi.tests.sectionTemplate
{
    public class SectionTemplateFixture : IGenericFixture<SectionTemplate, Guid, SectionTemplateRepository, JobDbContext, SectionTemplateWhere>
    {
        private QuestionsFixture questionFixture;

        public SectionTemplateFixture()
        {
            questionFixture = new QuestionsFixture();
        }

        public JobDbContext dbContext => new JobDbContext(new DbContextOptionsBuilder<JobDbContext>()
                        .UseInMemoryDatabase(databaseName: "SectionTemplateTest")
                        .Options);

        public Func<JobDbContext, SectionTemplateRepository> repoFn => c => new SectionTemplateRepository(c);
        public Func<SectionTemplate, Guid> getId => j => j.Id;
        public Action<SectionTemplate> mutate(string seed)
        {
            return j => j.Title = seed;
        }
        public SectionTemplate noIdItem1 => new SectionTemplate
        {
            Title = "Title1",
            HelpText = "HelpText1",
        };
        public Guid id1 => IdFixture.stId1;
        public Guid id2 => IdFixture.stId2;
        public SectionTemplate item1 => new SectionTemplate
        {
            Id = id1,
            Title = "Title1",
            HelpText = "HelpText1",
            Owner="me",
            Who="who"

        };
        public SectionTemplate item2 => new SectionTemplate
        {
            Id = id2,
            Title = "Title2",
            HelpText = "HelpText2",
            Owner = "me",
            Who = "who"

        };
        public SectionTemplate eagerItem1 => new SectionTemplate
        {
            Id = id1,
            Title = "Title1",
            HelpText = "HelpText1",
            Owner = "me",
            Who = "who",
            Questions = new List<Question> { questionFixture.item1, questionFixture.item2 }

        };
        public SectionTemplate eagerItem2 => new SectionTemplate
        {
            Id = id2,
            Title = "Title2",
            HelpText = "HelpText2",
            Owner = "me",
            Who = "who"
        };

        async public Task populate(SectionTemplateRepository repo)
        {
            await repo.AddAsync(eagerItem1);
            await repo.AddAsync(eagerItem2);
            repo.cleanDb();
        }
        public SectionTemplateWhere emptyWhere => new SectionTemplateWhere();
    }


}
