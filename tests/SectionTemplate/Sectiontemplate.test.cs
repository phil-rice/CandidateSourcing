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
using xingyi.tests.sectionTemplate;

namespace xingyi.tests.sectionTemplate
{
  
    public class SectionTemplateRepositoryTest : GenericRepoTest<SectionTemplate, Guid, SectionTemplateRepository, JobDbContext>
    {
        public SectionTemplateRepositoryTest() : base(new SectionTemplateFixture())
        {
        }
    }
}
