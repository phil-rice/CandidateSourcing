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

namespace xingyi.tests.sectionTemplate
{
    public class SectionTemplateConsumerPactTest : GenericClientConsumerPactTest<SectionTemplate, Guid, SectionTemplateRepository,JobDbContext, SectionTemplateClient>
    {


        public SectionTemplateConsumerPactTest() : base(
            port =>
            {
                var httpClient = new HttpClient();
                var settings = new SectionTemplateSettings { BaseUrl = $"http://localhost:{port}/api/" };
                return new SectionTemplateClient(httpClient, new OptionsWrapper<SectionTemplateSettings>(settings));

            },
            new SectionTemplateFixture(),
            "SectionTemplate"
            )
        {
        }

    }
}
