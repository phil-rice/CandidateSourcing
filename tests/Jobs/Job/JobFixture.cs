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

namespace xingyi.tests.job
{
    public class JobFixture : IGenericFixture<Job, Guid, JobRepository, JobDbContext, JobWhere>
    {

        public JobDbContext dbContext => new JobDbContext(new DbContextOptionsBuilder<JobDbContext>()
                        .UseInMemoryDatabase(databaseName: "JobTest")
                        .Options);

        public Func<JobDbContext, JobRepository> repoFn => c => new JobRepository(c);
        public Func<Job, Guid> getId => j => j.Id;
        public Action<Job> mutate(string seed)
        {
            return j => j.Title = seed;
        }
        public Job noIdItem1 => new Job
        {
            Title = "Title1",
            Description = "Description1",
            Owner = "Owner1"
        };
        public Guid id1 => IdFixture.jobId1;
        public Guid id2 => IdFixture.jobId2;
        public Job item1 => new Job
        {
            Id = id1,
            Title = "Title1",
            Description = "Description1",
            Owner = "Owner1"
        };
        public Job item2 => new Job
        {
            Id = id2,
            Title = "Title2",
            Description = "Description2",
            Owner = "Owner2"
        };
        public Job eagerItem1 => new Job
        {
            Id = id1,
            Title = "Title1",
            Description = "Description1",
            Owner = "Owner1"
        };
        public Job eagerItem2 => new Job
        {
            Id = id2,
            Title = "Title2",
            Description = "Description2",
            Owner = "Owner2"
        };

        async public Task populate(JobRepository repo)
        {
            await repo.AddAsync(eagerItem1);
            await repo.AddAsync(eagerItem2);
            repo.cleanDb();
        }
        public JobWhere emptyWhere => new JobWhere();
    }

}
