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
    public class QuestionReopsitoryTest : GenericRepoTest<Question, Guid, IQuestionRepository, JobDbContext>
    {
        public QuestionReopsitoryTest() : base(new QuestionsFixture())
        {
        }
    }
}
