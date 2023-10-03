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

namespace xingyi.tests.job
{
    public class JobClientConsumerPactTest : GenericClientConsumerPactTest<Job, Guid, IJobRepository, JobDbContext, JobClient>
    {


        public JobClientConsumerPactTest() : base(
            port: 9022,
            port =>
            {
                var httpClient = new HttpClient();
                var jobSettings = new JobSettings { BaseUrl = $"http://localhost:{port}/api/" };
                return new JobClient(httpClient, new OptionsWrapper<JobSettings>(jobSettings));

            },
            new QuestionsFixture(),
            "Job"
            )
        {
        }

    }
}
