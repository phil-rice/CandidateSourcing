
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xingyi.application;
using xingyi.common;
using xingyi.job;
using xingyi.job.Models;
using xingyi.job.Repository;
using xingyi.test.generic;
using xingyi.tests.answers;
using xingyi.tests.generic;
using xingyi.tests.job;
using xingyi.tests.questions;
using static xingyi.job.Repository.ApplicationRepository;

namespace xingyi.tests.section
{
    public class SectionFixture : IGenericFixture<Section, Guid, SectionRepository, JobDbContext, SectionWhere>
    {
        private AnswersFixture answersFixture;

        public SectionFixture()
        {
            answersFixture = new AnswersFixture();
        }

        public JobDbContext dbContext => new JobDbContext(new DbContextOptionsBuilder<JobDbContext>()
                        .UseInMemoryDatabase(databaseName: "SectionTest")
                         .EnableSensitiveDataLogging(true)
                        .Options);

        public Func<JobDbContext, SectionRepository> repoFn => c => new SectionRepository(c);
        public Func<Section, Guid> getId => j => j.Id;
        public Action<Section> mutate(string seed)
        {
            return j => j.Comments = seed;
        }
        public Section noIdItem1
        {
            get
            {
                var item = item1;
                var propertyInfo = item.GetType().GetProperty("Id");
                propertyInfo.SetValue(item, null, null);
                return item1;
            }
        }
        public Guid id1 => IdFixture.secId1;
        public Guid id2 => IdFixture.secId2;
        public Section item1 => new Section
        {
            Id = id1,
            ApplicationId = IdFixture.appId1,
            Who = "Who1",
            Comments = "Comments1",
            Finished = false,
            Answers = new List<Answer>
            {
            }

        };
        public Section item2 => new Section
        {
            Id = id2,
            ApplicationId = IdFixture.appId2,
            Who = "Who2",
            Comments = "Comments2",
            Finished = true,
            Answers = new List<Answer>()
        };
        public Section eagerItem1 => new Section
        {
            Id = id1,
            ApplicationId = IdFixture.appId1,
            Who = "Who1",
            Comments = "Comments1",
            Finished = false,
            Answers = new List<Answer> { answersFixture.eagerItem1, answersFixture.eagerItem2 }

        };
        public Section eagerItem2 => new Section
        {
            Id = id2,
            ApplicationId = IdFixture.appId2,
            Who = "Who2",
            Comments = "Comments2",
            Finished = true,
            Answers = new List<Answer>()
        };

        async public Task populate(SectionRepository repo)
        {
            var e1 = eagerItem1;
            var e2 = eagerItem2;

            await repo.AddAsync(e1);
            await repo.AddAsync(e2);
            repo.cleanDb();
        }
    }


}
