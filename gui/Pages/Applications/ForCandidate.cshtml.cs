using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.application;
using xingyi.job.Repository;

namespace gui.Pages.Applications
{
    public class ForCandidateModel : PageModel
    {
        private readonly IApplicationRepository appRepo;

        [BindProperty(SupportsGet = true)]
        public string Candidate { get; set; }

        [BindProperty]
        public List<Application> Items { get; set; }   
        public ForCandidateModel(IApplicationRepository appRepo)
        {
            this.appRepo = appRepo;
        }

        async public Task<IActionResult> OnGetAsync()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            Items = await appRepo.GetAllAsync(new ApplicationWhere { Candidate= Candidate, Owner=email}, true);
            return Page();

        }
    }
}
