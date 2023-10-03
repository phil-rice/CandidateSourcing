using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using xingyi.gui;
using xingyi.job.Client;
using xingyi.job.Models;
using xingyi.gui;
using xingyi.job.Client;
using xingyi.job.Models;

namespace gui
{
    public class GenericPageModel: PageModel
    {
        private readonly IJobClient client;

        [BindProperty]
        public Job NewJob { get; set; }

        public GenericPageModel(IJobClient client)
        {
            this.client = client;
        }

 
        public async Task<IActionResult> CreateAsync()
        {
            ModelStateHelper.DumpModelState(ModelState);
            if (ModelState.IsValid)
            {
                await client.AddAsync(NewJob);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
