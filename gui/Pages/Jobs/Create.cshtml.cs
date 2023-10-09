using gui.GenericPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.common;
using xingyi.gui;
using xingyi.job.Client;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.Jobs
{

   
    public class JobCreateModel : AbstractJobPageModel
    {

        public JobCreateModel(IJobRepository jobRepo, ISectionTemplateRepository stRepo): base(jobRepo, stRepo)
        {
        }

        public async Task OnGetAsync()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var Job = new Job { Owner = email == null ? "No email" : email };
            await populateItem(Job, email);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            addTemplatesToJob();

            ModelStateHelper.DumpModelState(ModelState);
            if (ModelState.IsValid)
            {
                await jobRepo.AddAsync(Item.Job);
                return RedirectToPage("Index");
            }
            return Page();
        }

    
    }

}
