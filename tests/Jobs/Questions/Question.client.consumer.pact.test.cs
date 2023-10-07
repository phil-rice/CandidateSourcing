using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PactNet.Infrastructure.Outputters;
using PactNet;
using xingyi.job.Client;
using Microsoft.Extensions.Options;
using System.Net.Http;
using xingyi.common;
using xingyi.job.Models;
using xingyi.test;
using xingyi.job.Repository;
using xingyi.tests.generic;
using xingyi.job;

namespace xingyi.tests.questions
{
    public class QuestionClientConsumerPactTest : GenericClientConsumerPactTest<Question, Guid, QuestionRepository, JobDbContext, QuestionClient>
    {
        public QuestionClientConsumerPactTest() : base(
    port =>
    {
        var httpClient = new HttpClient();
        var settings = new QuestionSettings { BaseUrl = $"http://localhost:{port}/api/" };
        return new QuestionClient(httpClient, new OptionsWrapper<QuestionSettings>(settings));

    },
    new QuestionsFixture(),
    "Question"
    )
        {
        }

    }
}
