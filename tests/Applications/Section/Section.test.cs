using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xingyi.application;
using xingyi.common;
using xingyi.job.Models;
using xingyi.job.Repository;
using xingyi.test.generic;
using xingyi.tests.sectionTemplate;
using static xingyi.application.Repository.ApplicationRepository;

namespace xingyi.tests.section
{

    public class SectionRepositoryTest : GenericRepoTest<Section, Guid, SectionRepository, ApplicationDbContext>
    {
        public SectionRepositoryTest() : base(new SectionFixture())
        {
        }
    }
}
