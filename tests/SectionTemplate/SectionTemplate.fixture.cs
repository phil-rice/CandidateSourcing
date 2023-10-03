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

namespace xingyi.tests.sectionTemplate
{
    public class SectionTemplateFixture : IGenericFixture<SectionTemplate, Guid, ISectionTemplateRepository, JobDbContext>
    {

        public JobDbContext dbContext => new JobDbContext(new DbContextOptionsBuilder<JobDbContext>()
                        .UseInMemoryDatabase(databaseName: "SectionTemplateTest")
                        .Options);

        public Func<JobDbContext, ISectionTemplateRepository> repoFn => c => new SectionTemplateRespository(c);
        public Func<SectionTemplate, Guid> getId => j => j.Id;
        public Action<SectionTemplate> mutate(string seed)
        {
            return j => j.Title = seed;
        }
        public SectionTemplate noIdItem1 => new SectionTemplate
        {
            Title = "Title1",
            Description = "Description1"
        };
        public Guid id1 => Guids.from("st1");
        public SectionTemplate item1 => new SectionTemplate
        {
            Id = id1,
            Title = "Title1",
            Description = "Description1",

        };
        public SectionTemplate item2 => new SectionTemplate
        {
            Id = Guids.from("st2"),
            Title = "Title2",
            Description = "Description2",

        };
        public SectionTemplate eagerItem1 => new SectionTemplate
        {
            Id = id1,
            Title = "Title1",
            Description = "Description1",

        };
        public SectionTemplate eagerItem2 => new SectionTemplate
        {
            Id = Guids.from("st2"),
            Title = "Title2",
            Description = "Description2",

        };

        async public Task populate(ISectionTemplateRepository repo)
        {
            await repo.AddAsync(item1);
            await repo.AddAsync(item2);
        }
    }


}
