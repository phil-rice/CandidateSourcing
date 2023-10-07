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

namespace xingyi.tests.questions
{
    public class QuestionsFixture : IGenericFixture<Question, Guid, QuestionRepository, JobDbContext>
    {

        public JobDbContext dbContext => new JobDbContext(new DbContextOptionsBuilder<JobDbContext>()
                        .UseInMemoryDatabase(databaseName: "QuestionsTest")
                        .Options);

        public Func<JobDbContext, QuestionRepository> repoFn => c => new QuestionRepository(c);
        public Func<Question, Guid> getId => j => j.Id;
        public Action<Question> mutate(string seed)
        {
            return j => j.Title = seed;
        }
        public Question noIdItem1 => new Question
        {
            Title = "Title1",
            Description = "Description1"
        };
        public Guid id1 => IdFixture.qId1;
        public Guid id2 => IdFixture.qId2;
        public Question item1 => new Question
        {
            Id = id1,
            Title = "Title1",
            Description = "Description1",
            SectionTemplateId = IdFixture.stId1

        };
        public Question item2 => new Question
        {
            Id = id2,
            Title = "Title2",
            Description = "Description2",
            SectionTemplateId = IdFixture.stId1

        };
        public Question eagerItem1 => new Question
        {
            Id = id1,
            Title = "Title1",
            Description = "Description1",
            SectionTemplateId = IdFixture.stId1


        };
        public Question eagerItem2 => new Question
        {
            Id = id2,
            Title = "Title2",
            Description = "Description2",
            SectionTemplateId = IdFixture.stId1

        };

        async public Task populate(QuestionRepository repo)
        {
            await repo.AddAsync(eagerItem1);
            await repo.AddAsync(eagerItem2);
            repo.cleanDb();

        }
    }
}
