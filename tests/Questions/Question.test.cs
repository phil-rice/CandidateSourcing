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

namespace xingyi.tests.questions
{
    public class QuestionRepositoryTest : GenericRepoTest<Question, Guid, QuestionRepository, JobDbContext>
    {
        public QuestionRepositoryTest() : base(new QuestionsFixture())
        {
        }
    }
}
