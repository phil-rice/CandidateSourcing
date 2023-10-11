using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices
{
    public interface IRepositoryWhere<T>
    {
        IQueryable<T> Apply(IQueryable<T> queryable);
        string queryString();
    }
    public class EmptyRepositoryWhere<T> : IRepositoryWhere<T>
    {
        public bool whereDoesEverything => false;

        public IQueryable<T> Apply(IQueryable<T> queryable)
        {
            return queryable;
        }

        public string queryString()
        {
            return "";
        }
    }
}
