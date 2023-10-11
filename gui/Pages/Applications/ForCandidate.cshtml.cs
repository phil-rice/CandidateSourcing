using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.application;
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
        public List<Application> Items { get; set; } = new List<Application>();

        [BindProperty]
        public List<ManagedBy> ManagedByItems { get; set; } = new List<ManagedBy>();
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
                Items = await appRepo.GetAllAsync(new ApplicationWhere { Candidate = Candidate, Owner = email }, true);
                ManagedByItems= await manRepo.GetAllAsync(new ManagedByWhere { Candidate = Candidate, ManagedBy = email }, true);
            }
            return Page();
        }
    }
}
