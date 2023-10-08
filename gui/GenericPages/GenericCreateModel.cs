using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.gui;
using xingyi.job.Client;
using xingyi.job.Models;
using xingyi.microservices.repository;

namespace gui.GenericPages
{
    [Authorize]
    public class GenericCreateModel<T, ID, C> : PageModel where T : class, new() where C : IRepository<T, ID>
    {
        private readonly C repo;

        [BindProperty]
        public T Item { get; set; }

        public GenericCreateModel(C repo)
        {
            this.repo = repo;
        }

        virtual async public Task modifyItemOnCreate(T Item)
        {

        }
        virtual async public Task modifyItemOnPost(T Item)
        {

        }

        virtual public async Task OnGetAsync()
        {
            T i = new T();
            await modifyItemOnCreate(i);
            Item = i;
        }
       virtual public async Task<IActionResult> OnPostAsync()
        {
            await modifyItemOnPost(Item);
            TryValidateModel(Item);
            ModelStateHelper.DumpModelState(ModelState);
            if (ModelState.IsValid)
            {
                await repo.AddAsync(Item);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
