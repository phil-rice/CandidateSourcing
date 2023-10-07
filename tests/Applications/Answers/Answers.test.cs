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
using static xingyi.job.Repository.ApplicationRepository;

namespace xingyi.tests.answers
{
    public class AnswerRepositoryTest : GenericRepoTest<Answer, Guid, AnswersRepository, JobDbContext>
    {
        public AnswerRepositoryTest() : base(new AnswersFixture())
        {
        }
    }
}
