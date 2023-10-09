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

namespace xingyi.tests.application
{
    public class ApplicationRepositoryTest : GenericRepoTest<Application, Guid, ApplicationRepository, JobDbContext, ApplicationWhere>
    {
        public ApplicationRepositoryTest() : base(new ApplicationFixture())
        {
        }
    }
}
