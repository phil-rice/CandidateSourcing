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

using xingyi.application;

using xingyi.job;
using static xingyi.job.Repository.ApplicationRepository;


namespace xingyi.tests.section
{
    public class SectionConsumerPactTest : GenericClientConsumerPactTest<Section, Guid, SectionRepository, JobDbContext, SectionClient>
    {


        public SectionConsumerPactTest() : base(
            port =>
            {
                var httpClient = new HttpClient();
                var settings = new SectionSettings { BaseUrl = $"http://localhost:{port}/api/" };
                return new SectionClient(httpClient, new OptionsWrapper<SectionSettings>(settings));

            },
            new SectionFixture(),
            "SectionTemplate"
            )
        {
        }

    }
}
