using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.job.Client;
using xingyi.job.Models;
using xingyi.microservices.repository;

namespace gui.GenericPages
{
    public class GenericIndexModel<T, ID, C> : PageModel where T : class where C : IRepository<T, ID>
    {
        private IRepository<T, ID> repo { get; }
        public GenericIndexModel(IRepository<T, ID> repo)
        {
            this.repo = repo;

        }

        public List<T> Items { get; private set; }

        public async Task OnGetAsync()
        {
            Items = await repo.GetAllAsync();
        }
    }
}
