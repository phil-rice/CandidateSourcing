using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.application;
using xingyi.gui;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.Applications
{
    public class ForCandidateModel : PageModel
    {
        private readonly IApplicationRepository appRepo;
        private readonly IManagedByRepository manRepo;

        [BindProperty(SupportsGet = true)]
        public string Candidate { get; set; }

        [BindProperty]
        public List<JobAndApplications> Items { get; set; } = new List<JobAndApplications>();

        [BindProperty]
        public List<JobAndApplications> ManagedByItems { get; set; } = new List<JobAndApplications>();
        public ForCandidateModel(IApplicationRepository appRepo, IManagedByRepository manRepo)
        {
            this.appRepo = appRepo;
            this.manRepo = manRepo;
        }

        async public Task<IActionResult> OnGetAsync()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            if (Candidate != null)
            {
                Items = JobAndApplications.make(await appRepo.GetAllAsync(new ApplicationWhere { Candidate = Candidate, Owner = email }, true));
                ManagedByItems = JobAndApplications.make(await manRepo.GetAllAsync(new ManagedByWhere { Candidate = Candidate, ManagedBy = email }, true));
            }
            return Page();
        }
    }
}
