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

using xingyi.tests.answers;
using xingyi.application;
using static xingyi.application.Repository.ApplicationRepository;
using applicationClient;

namespace xingyi.tests.answer
{
    public class AnswerClientConsumerPactTest : GenericClientConsumerPactTest<Answer, Guid, AnswersRepository, ApplicationDbContext, AnswerClient>
    {
        public AnswerClientConsumerPactTest() : base(
    port =>
    {
        var httpClient = new HttpClient();
        var settings = new AnswerSettings { BaseUrl = $"http://localhost:{port}/api/" };
        return new AnswerClient(httpClient, new OptionsWrapper<AnswerSettings>(settings));

    },
    new AnswersFixture(),
    "Answer"
    )
        {
        }

    }
}
