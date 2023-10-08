using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.application;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IJobRepository jobRepo;
        private readonly ISectionRepository sectionRepo;

        public List<Job> MyActiveJobs { get; set; }
        public List<Job> MyCompletedJobs { get; set; }
        public List<Section> MyTodoSections { get; set; }
        public List<Section> MyDoneSections { get; set; }

        public bool Nothing
        {
            get
            {
                return MyActiveJobs.Count == 0 && MyCompletedJobs.Count == 0 && MyTodoSections.Count == 0 && MyDoneSections.Count == 0;
            }
        }


        public IndexModel(ILogger<IndexModel> logger, IJobRepository jobRepo, ISectionRepository sectionRepo)
        {
            _logger = logger;
            this.jobRepo = jobRepo;
            this.sectionRepo = sectionRepo;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToPage("/Identity/Login");
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            MyActiveJobs = (await jobRepo.GetAllAsync()).Where(j=>!j.Finished).ToList(); //will have where.
            MyCompletedJobs = (await jobRepo.GetAllAsync()).Where(j => j.Finished).ToList(); //will have where.
            MyTodoSections = await sectionRepo.GetAllAsync(); //will have where.    
            MyDoneSections = await sectionRepo.GetAllAsync(); //will have where.    
            return Page();
        }
    }
}