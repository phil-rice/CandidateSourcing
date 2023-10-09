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
using Microsoft.EntityFrameworkCore;
using xingyi.microservices.repository;
using xingyi.microservices.Client;
using xingyi.test.generic;

namespace xingyi.tests.generic
{
    abstract public class GenericClientConsumerPactTest<T, Id, R, C, Cl, Where>
        where T : class
        where C : DbContext
        where R : IRepository<T, Id, Where>
        where Cl : GenericClient<T, Id, Where>
    {

        private Func<int, Cl> clientFn ;
        private IGenericFixture<T, Id, R, C, Where> fixture;
        private string itemClassName = "Job";
        private IPactBuilderV2 pact;

        protected GenericClientConsumerPactTest(Func<int, Cl> clientFn, IGenericFixture<T, Id, R, C, Where> fixture, string itemClassName)
        {
            this.clientFn = clientFn;
            this.fixture = fixture;
            this.itemClassName = itemClassName;
        }

        [OneTimeSetUp]
        public void Init()
        {
            var config = new PactConfig
            {
                PactDir = @"..\..\..\..\artifacts\pacts"
            };
            IPactV2 pact = Pact.V2($"{itemClassName}Client", $"{itemClassName}Api", config);
            this.pact = pact.WithHttpInteractions();
        }

        [Test]
        async public Task EnsureCanReadEmptyItems()
        {
            pact
              .UponReceiving($"A request for {itemClassName}s")
              .Given("No items")
              .WithRequest(HttpMethod.Get, $"/api/{itemClassName}")
              .WithQuery("eagerLoad", "False")
              .WillRespond()
              .WithStatus(200)
              .WithBody("[]", "application/json");

            await pact.VerifyAsync(async ctx =>
            {
                var client = clientFn(ctx.MockServerUri.Port);
                var result = await client.GetAllAsync();
                Assert.AreEqual(0, result.Count());

            });

        }
        [Test]
        async public Task EnsureCanReadItems()
        {
            var expected = new List<T> { fixture.item1, fixture.item2 };
            pact
              .UponReceiving($"A request for {itemClassName}s when they exist")
              .Given("Items exist")
              .WithRequest(HttpMethod.Get, $"/api/{itemClassName}")
              .WithQuery("eagerLoad", "False")
              .WillRespond()
              .WithStatus(200)
              .WithJsonBody(expected);

            await pact.VerifyAsync(async ctx =>
            {
                var client = clientFn(ctx.MockServerUri.Port);
                var result = await client.GetAllAsync();
                Assertions.ListsEqual(expected, result);

            });

        }
        [Test]
        async public Task EnsureCanReadOneItem()
        {
            var id1 = fixture.id1;
            pact
              .UponReceiving($"A request for a single {itemClassName}")
              .Given("Items exist")
              .WithRequest(HttpMethod.Get, $"/api/{itemClassName}/{id1}")
              .WithQuery("eagerLoad", "True")
              .WillRespond()
              .WithStatus(200)
              .WithJsonBody(fixture.item1);

            await pact.VerifyAsync(async ctx =>
            {

                var client = clientFn(ctx.MockServerUri.Port);
                var result = await client.GetByIdAsync(id1);
                Assert.AreEqual(fixture.item1, result);

            });
        }
        [Test]
        async public Task EnsureCanCreateOneItem()
        {
            pact
              .UponReceiving($"Creating a {itemClassName}")
              .Given("No items")
              .WithRequest(HttpMethod.Post, $"/api/{itemClassName}")
              .WithJsonBody(fixture.noIdItem1)
              .WillRespond()
              .WithStatus(201)
              .WithJsonBody(fixture.eagerItem1);

            await pact.VerifyAsync(async ctx =>
            {

                var client = clientFn(ctx.MockServerUri.Port);
                var result = await client.AddAsync(fixture.noIdItem1);
                Assert.AreEqual(fixture.eagerItem1, result);

            });
        }

    }
}
