using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.gui;
using xingyi.job.Client;
using xingyi.job.Models;

namespace gui.Pages.Jobs
{
    public class JobEditModel : PageModel
    {
        private readonly IJobClient client;
        [FromRoute]
        public Guid Id { get; set; }
        [BindProperty]
        public Job Job { get; set; }

        public JobEditModel(IJobClient client)
        {
            this.client = client;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            ModelStateHelper.DumpModelState(ModelState);
            if (ModelState.IsValid)
            {
                await client.UpdateAsync(Job);
                return RedirectToPage("Index");
            }
            return Page();
        }

        public async Task OnGetAsync()
        {
            System.Diagnostics.Debug.WriteLine($"ID: {Id}");
            Console.WriteLine($"ID {Id}");
            Job = await client.GetByIdAsync(Id);
            Console.WriteLine($"Job {Job}");

        }

    }
}
