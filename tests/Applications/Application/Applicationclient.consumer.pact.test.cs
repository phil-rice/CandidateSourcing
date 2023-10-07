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
using xingyi.application;
using xingyi.job;

namespace xingyi.tests.application
{
    public class ApplicationClientConsumerPactTest : GenericClientConsumerPactTest<Application, Guid, ApplicationRepository, JobDbContext, ApplicationClient>
    {


        public ApplicationClientConsumerPactTest() : base(
             port =>
            {
                var httpClient = new HttpClient();
                var settings = new ApplicationSettings { BaseUrl = $"http://localhost:{port}/api/" };
                return new ApplicationClient(httpClient, new OptionsWrapper<ApplicationSettings>(settings));

            },
            new ApplicationFixture(),
            "Application"
            )
        {
        }

    }
}
