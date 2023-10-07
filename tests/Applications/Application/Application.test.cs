﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xingyi.application;
using xingyi.application.Repository;
using xingyi.common;
using xingyi.job.Models;
using xingyi.job.Repository;
using xingyi.test.generic;

namespace xingyi.tests.application
{
    public class ApplicationRepositoryTest : GenericRepoTest<Application, Guid, ApplicationRepository, ApplicationDbContext>
    {
        public ApplicationRepositoryTest() : base(new ApplicationFixture())
        {
        }
    }
}
