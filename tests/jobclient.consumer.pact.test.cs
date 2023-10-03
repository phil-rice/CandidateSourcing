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

namespace tests
{
    public class JobClientConsumerPactTest
    {

        private IPactBuilderV2 pact;
        private readonly int port = 9222;
        private JobClient client;

        [OneTimeSetUp]
        public void Init()
        {
            var httpClient = new System.Net.Http.HttpClient();
            var jobSettings = new JobSettings { BaseUrl = $"http://localhost:{port}/api/" };
            this.client = new JobClient(httpClient, new OptionsWrapper<JobSettings>(jobSettings));

            var config = new PactConfig
            {
                PactDir = @"..\..\..\..\artifacts\pacts"
            };

            IPactV2 pact = Pact.V2("JobClient", "JobApi", config);

            this.pact = pact.WithHttpInteractions(port);
        }

        [Test]
        async public Task EnsureCanReadEmptyJobs()
        {
            pact
              .UponReceiving("A request for jobs")
              .Given("No items")
              .WithRequest(HttpMethod.Get, "/api/Job")
              .WithQuery("eagerLoad", "False")
              .WillRespond()
              .WithStatus(200)
              .WithBody("[]", "application/json");

            await pact.VerifyAsync(async ctx =>
            {
                var result = await client.GetAllAsync();
                Assert.AreEqual(0, result.Count());

            });

        }
        [Test]
        async public Task EnsureCanReadOneJob()
        {
            var guid = Guids.from("1");
            pact
              .UponReceiving("A request for a single job")
              .Given("Items exist")
              .WithRequest(HttpMethod.Get, $"/api/Job/{guid}")
              .WillRespond()
              .WithStatus(200)
              .WithBody("[]", "application/json");

            await pact.VerifyAsync(async ctx =>
            {
                var result = await client.GetByIdAsync(guid);
                Assert.AreEqual(guid, result.Id);

            });
        }
    }
}
