using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tests;
using xingyi.common;
using xingyi.job.Models;
using xingyi.job.Repository;
using xingyi.microservices.repository;
using xingyi.test;

namespace xingyi.tests
{
    public interface IFixture<T, Id, R, C> where T : class where C : DbContext where R : IRepository<T, Id>
    {
        public C dbContext { get; }
        public Func<C, R> repoFn { get; }
        public Func<T, Id> getId { get; }
        public T noIdItem1 { get; }
        public Id id1 { get; }

        public T item1 { get; }
        public T item2 { get; }
        public Action<T> mutate(string seed);
        public T eagerItem1 { get; }
        public T eagerItem2 { get; }
        public List<T> items() { return new List<T> { item1, item2 }; }
        public List<T> eagerItems() { return new List<T> { item1, item2 }; }

        public Task populate(R repo);


    }

    abstract public class GenericRepoTest<T, Id, R, C> where T : class where C : DbContext where R : IRepository<T, Id>
    {
        private IFixture<T, Id, R, C> fixture;
        private C _context;
        private R _repository;

        protected GenericRepoTest(IFixture<T, Id, R, C> fixture)
        {
            this.fixture = fixture;
        }

        [SetUp]
        public void Setup()
        {
            _context = fixture.dbContext;
            _repository = fixture.repoFn(_context);
        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
        [Test]
        async public Task Ensure_Can_Get_All_NotEager()
        {
            await fixture.populate(_repository);
            var all = await _repository.GetAllAsync();
            Assertions.ListsEqual(new List<T> { fixture.item1, fixture.item2 }, all);

        }
        [Test]
        async public Task Ensure_Can_Get_All_Eager()
        {
            await fixture.populate(_repository);
            var all = await _repository.GetAllAsync(true);
            Assertions.ListsEqual(new List<T> { fixture.eagerItem1, fixture.eagerItem2 }, all);

        }

        [Test]
        async public Task Ensure_Can_get_eager()
        {
            await fixture.populate(_repository);
            Assert.AreEqual(fixture.eagerItem1, await _repository.GetByIdAsync(fixture.id1));
        }
        [Test]
        async public Task Ensure_Can_get_Noteager()
        {
            await fixture.populate(_repository);
            Assert.AreEqual(fixture.item1, await _repository.GetByIdAsync(fixture.id1, false));
        }
        [Test]
        async public Task update()
        {
            await fixture.populate(_repository);
            var id = fixture.id1;
            T mutated = await _repository.GetByIdAsync(id);
            fixture.mutate("newTitle")(mutated);
            await _repository.UpdateAsync(mutated);
            Assert.AreEqual(mutated, await _repository.GetByIdAsync(fixture.id1));
        }
    }
}
