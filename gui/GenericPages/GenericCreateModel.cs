using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.gui;
using xingyi.job.Client;
using xingyi.job.Models;
using xingyi.microservices.repository;

namespace gui.GenericPages
{
    public class GenericCreateModel<T, ID ,C> : PageModel where T: class where C : IRepository<T, ID>
    {
        private readonly C repo;

        [BindProperty]
        public T NewItem { get; set; }

        public GenericCreateModel(C repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelStateHelper.DumpModelState(ModelState);
            if (ModelState.IsValid)
            {
                await repo.AddAsync(NewItem);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
