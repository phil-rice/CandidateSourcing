using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xingyi.common;
using xingyi.job.Models;
using xingyi.job.Repository;
using xingyi.test.generic;
using xingyi.tests.generic;
using xingyi.tests.job;

namespace xingyi.tests.questions
{
    public class QuestionsFixture : IGenericFixture<Question, Guid, IQuestionRepository, JobDbContext>
    {

        public JobDbContext dbContext => new JobDbContext(new DbContextOptionsBuilder<JobDbContext>()
                        .UseInMemoryDatabase(databaseName: "JobTest")
                        .Options);

        public Func<JobDbContext, IQuestionRepository> repoFn => c => new QuestionRepository(c);
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
        public Guid id1 => Guids.from("Q1");
        public Question item1 => new Question
        {
            Id = id1,
            Title = "Title1",
            Description = "Description1"
          
        };
        public Question item2 => new Question
        {
            Id = Guids.from("Q2"),
            Title = "Title2",
            Description = "Description2"
          
        };
        public Question eagerItem1 => new Question
        {
            Id = id1,
            Title = "Title1",
            Description = "Description1"
          
        };
        public Question eagerItem2 => new Question
        {
            Id = Guids.from("Q2"),
            Title = "Title2",
            Description = "Description2"
          
        };

        async public Task populate(IQuestionRepository repo)
        {
            await repo.AddAsync(item1);
            await repo.AddAsync(item2);
        }
    }
}
