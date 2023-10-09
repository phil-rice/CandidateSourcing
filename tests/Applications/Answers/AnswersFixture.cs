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
using xingyi.tests.generic;
using xingyi.tests.job;

using static xingyi.job.Repository.ApplicationRepository;

namespace xingyi.tests.answers
{
    public class AnswersFixture : IGenericFixture<Answer, Guid, AnswersRepository, JobDbContext, AnswerWhere>
    {

        public JobDbContext dbContext => 
            new JobDbContext(new DbContextOptionsBuilder<JobDbContext>()
                        .UseInMemoryDatabase(databaseName: "AnswersTest")
                        .Options);

        public Func<JobDbContext, AnswersRepository> repoFn => c => new AnswersRepository(c);

        public Func<Answer, Guid> getId => j => j.Id;
        public Action<Answer> mutate(string seed)
        {
            return j => j.Title = seed;
        }
        public Answer noIdItem1
        {
            get
            {
                var item = item1;
                var propertyInfo = item.GetType().GetProperty("Id");
                propertyInfo.SetValue(item, null, null);
                return item1;
            }
        }

        public Guid id1 => IdFixture.aId1;
        public Guid id2 => IdFixture.aId2;
        public Answer item1 => eagerItem1;
        public Answer item2 => eagerItem2;
        public Answer eagerItem1 => new Answer
        {
            Id = id1,
            Title = "Title1",
            Description = "Description1",
            SectionId = IdFixture.secId1,

            AnswerText = "answer1"


        };
        public Answer eagerItem2 => new Answer
        {
            Id = id2,
            Title = "Title2",
            Description = "Description2",
            SectionId = IdFixture.secId1,
            AnswerText = "answer2"

        };


        async public Task populate(AnswersRepository repo)
        {
            await repo.AddAsync(eagerItem1);
            await repo.AddAsync(eagerItem2);
            repo.cleanDb();
        }
    }
}
