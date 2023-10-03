using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.gui;
using xingyi.job.Client;
using xingyi.job.Models;

namespace gui.Pages.Jobs
{
    public class JobCreateModel : PageModel
    {
        private readonly IJobClient client;

        [BindProperty]
        public Job NewJob { get; set; }

        public JobCreateModel(IJobClient client)
        {
            this.client = client;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
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
