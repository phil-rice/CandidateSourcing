using gui.GenericPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.gui;
using xingyi.job.Client;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.Jobs
{
    [Authorize]
    public class JobEditModel : AbstractJobPageModel

    {
        [FromRoute]
        public Guid Id { get; set; }
        public JobEditModel(IJobRepository client, ISectionTemplateRepository stRepo) : base(client, stRepo)
        {
        }

        public async Task OnGetAsync()
        {
            System.Diagnostics.Debug.WriteLine($"ID: {Id}");
            Console.WriteLine($"ID {Id}");
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

            var job = await jobRepo.GetByIdAsync(Id);
            await populateItem(job, email);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            addTemplatesToJob();
            ModelStateHelper.DumpModelState(ModelState);
            if (ModelState.IsValid)
            {
                await jobRepo.UpdateAsync(Item.Job);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
