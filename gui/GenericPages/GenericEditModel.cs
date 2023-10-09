using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.gui;
using xingyi.job.Client;
using xingyi.job.Models;
using xingyi.microservices.repository;

namespace gui.GenericPages
{
    [Authorize]
    public class GenericEditModel<T, ID, C, Where> : PageModel where T : class where C : IRepository<T, ID, Where>
    {
        private readonly IRepository<T,ID, Where> repo;
        [FromRoute]
        public ID Id { get; set; }
        [BindProperty]
        public T Item { get; set; }

        public GenericEditModel(IRepository<T, ID, Where> repo)
        {
            this.repo = repo;
        }
        public virtual async Task<IActionResult> OnPostAsync()
        {
            ModelStateHelper.DumpModelState(ModelState);
            if (ModelState.IsValid)
            {
                await repo.UpdateAsync(Item);
                return RedirectToPage("Index");
            }
            return Page();
        }

        public async Task OnGetAsync()
        {
            System.Diagnostics.Debug.WriteLine($"ID: {Id}");
            Console.WriteLine($"ID {Id}");
            Item = await repo.GetByIdAsync(Id);
            Console.WriteLine($"Job {Item}");

        }

    }
}
