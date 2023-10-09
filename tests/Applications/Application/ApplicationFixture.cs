
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Interfaces;
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

namespace xingyi.tests.application
{
    public class ApplicationFixture : IGenericFixture<Application, Guid, ApplicationRepository, JobDbContext, ApplicationWhere>
    {
        private JobFixture jobFixture = new JobFixture();

        public JobDbContext dbContext => new JobDbContext(new DbContextOptionsBuilder<JobDbContext>()
                        .UseInMemoryDatabase(databaseName: "ApplicationTest")
                        .Options);

        public Func<JobDbContext, ApplicationRepository> repoFn => c => new ApplicationRepository(c);
        public Func<Application, Guid> getId => j => j.Id;
        public Action<Application> mutate(string seed)
        {
            return j => j.DetailedComments = seed;
        }

        public Guid id1 => IdFixture.appId1;
        public Guid id2 => IdFixture.appId2;
        public Application noIdItem1
        {
            get
            {
                var item = item1;
                var propertyInfo = item.GetType().GetProperty("Id");
                propertyInfo.SetValue(item, null, null);
                return item;
            }

        }
        public Application item1 {
            get
            {
                var item = eagerItem1;
                item.Sections = new List<Section>();
                return item;
            }
        }
        public Application item2 {
            get
            {
                var item = eagerItem2;
                item.Sections = new List<Section>();
                return item;
            }
        }
        public Application eagerItem1 => new Application
        {
            Id = id1,
            JobId = jobFixture.id1,
            Candidate = "some.candidate@example.com",
            DetailedComments = "these are the detailed comments"
        };
        public Application eagerItem2 => new Application
        {
            Id = id2,
            JobId = jobFixture.id1,
            Candidate = "some.candidate@example.com",
            DetailedComments = "these are the detailed comments"
        };

        async public Task populate(ApplicationRepository repo)
        {
            await repo.AddAsync(eagerItem1);
            await repo.AddAsync(eagerItem2);
            repo.cleanDb();
        }
    }
}
