using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xingyi.common;
using xingyi.microservices.repository;

namespace xingyi.tests.generic
{
    public static class IdFixture
    {
        public static Guid jobId1 = Guids.from("job1");
        public static Guid jobId2 = Guids.from("job2");
        public static Guid stId1 = Guids.from("st1");
        public static Guid stId2 = Guids.from("st2");
        public static Guid qId1 = Guids.from("q1");
        public static Guid qId2 = Guids.from("q2");
    }
    public interface IGenericFixture<T, Id, R, C> where T : class where C : DbContext where R : IRepository<T, Id>
    {
  

        public C dbContext { get; }
        public Func<C, R> repoFn { get; }
        public Func<T, Id> getId { get; }
        public T noIdItem1 { get; }
        public Id id1 { get; }
        public Id id2 { get; }


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
