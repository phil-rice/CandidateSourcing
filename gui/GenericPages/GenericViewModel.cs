using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.job.Client;
using xingyi.job.Models;
using xingyi.microservices.repository;

namespace gui.GenericPages
{
    public class GenericViewModel<T, ID, C> : PageModel where T : class where C : IRepository<T, ID>
    {
        private readonly IRepository<T, ID> repo;
        [FromRoute]
        public ID Id { get; set; }
        [BindProperty]
        public T Item { get; private set; }

        public GenericViewModel(IRepository<T, ID> repo)
        {
            this.repo = repo;
        }


        public async Task OnGetAsync()
        {
            System.Diagnostics.Debug.WriteLine($"ID: {Id}");
            Item = await repo.GetByIdAsync(Id);
        }

    }
}