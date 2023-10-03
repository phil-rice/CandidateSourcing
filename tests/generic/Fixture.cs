using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xingyi.microservices.repository;

namespace xingyi.tests.generic
{
    public interface IGenericFixture<T, Id, R, C> where T : class where C : DbContext where R : IRepository<T, Id>
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

}
