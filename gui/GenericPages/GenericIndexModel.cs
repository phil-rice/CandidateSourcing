using Microservices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.job.Client;
using xingyi.job.Models;
using xingyi.microservices.repository;

namespace gui.GenericPages
{
    [Authorize]
    public abstract class GenericIndexModel<T, ID, C, Where> : PageModel 
        where T : class
        where C : IRepository<T, ID, Where>
        where Where : IRepositoryWhere<T>
    {
        private IRepository<T, ID, Where> repo { get; }
        public GenericIndexModel(IRepository<T, ID, Where> repo)
        {
            this.repo = repo;

        }

        public List<T> Items { get; private set; }

        abstract protected Where where();
        virtual public async Task OnGetAsync()
        {
            Items = await repo.GetAllAsync(where());
        }
    }
}
