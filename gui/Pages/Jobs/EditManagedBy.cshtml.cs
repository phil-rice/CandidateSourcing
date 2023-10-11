using Microservices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.Jobs
{
    public class EditManagedByModel : PageModel
    {
        private readonly IJobRepository jobRepo;
        private readonly IManagedByRepository managedByRepo;

        [FromRoute]
        public Guid JobId { get; set; }


        [BindProperty]
        public string? Action { get; set; }

        [BindProperty]
        public Job Item { get; set; }
        [BindProperty]
        [EmailAddress]
        public string? Email { get; set; } = "";

        public EditManagedByModel(IJobRepository jobRepo, IManagedByRepository managedByRepo)
        {
            this.jobRepo = jobRepo;
            this.managedByRepo = managedByRepo;
        }

        async public Task<IActionResult> OnGetAsync()
        {
            Item = await jobRepo.GetByIdAsync(JobId, true);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Item = await jobRepo.GetByIdAsync(JobId, true);
            if (Action == "Remove")
            {
                if (Email != null)
                {
                    await managedByRepo.DeleteAsync(new GuidAndEmail(JobId, Email));
                    Item = await jobRepo.GetByIdAsync(JobId, true);
                }
                return Page();
            }
            if (Action == "Add")
            {
                if (Email != null && Email.Length > 0 && !Item.ManagedBy.Any(mb => mb.Email == Email))
                {
                    await managedByRepo.AddAsync(new ManagedBy { Email = Email, JobId = JobId });
                    Item = await jobRepo.GetByIdAsync(JobId, true);
                }
                return Page();
            }
            return BadRequest();
        }
    }
}
