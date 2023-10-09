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
using xingyi.tests.questions;
using xingyi.job;

namespace xingyi.tests.job
{
    public class JobClientConsumerPactTest : GenericClientConsumerPactTest<Job, Guid, JobRepository, JobDbContext, JobClient ,JobWhere>
    {

        public JobClientConsumerPactTest() : base(

            port =>
            {
                var httpClient = new HttpClient();
                var jobSettings = new JobSettings { BaseUrl = $"http://localhost:{port}/api/" };
                return new JobClient(httpClient, new OptionsWrapper<JobSettings>(jobSettings));

            },
            new JobFixture(),
            "Job"
            )
        {
        }

    }
}
