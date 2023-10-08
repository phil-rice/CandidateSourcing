using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.application;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages
{
    public class JobAndApplications
    {
        public Job Job;
        public List<Application> Applications;

        public static List<JobAndApplications> make(List<Application> apps)
        {
            return apps.GroupBy(a => a.JobId).Select(g => new JobAndApplications { Job = g.First().Job, Applications = g.ToList() }).ToList();
        }
        public static List<JobAndApplications> make(List<Job> jobs)
        {
            return jobs.Select(j => new JobAndApplications { Job = j, Applications = j.Applications.ToList() }).ToList();
        }
    }

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IJobAndAppRepository jobAndAppRepo;
        private readonly ISectionRepository sectionRepo;

        public List<JobAndApplications> MyActiveApps { get; set; }
        public List<JobAndApplications> MyCompletedApps { get; set; }
        public List<Section> MyTodoSections { get; set; }
        public List<Section> MyDoneSections { get; set; }

        public bool Nothing
        {
            get
            {
                return MyActiveApps.Count == 0 && MyCompletedApps.Count == 0 && MyTodoSections.Count == 0 && MyDoneSections.Count == 0;
            }
        }


        public IndexModel(ILogger<IndexModel> logger, IJobAndAppRepository jobAndAppRepo, ISectionRepository sectionRepo)
        {
            _logger = logger;
            this.jobAndAppRepo = jobAndAppRepo;
            this.sectionRepo = sectionRepo;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToPage("/Identity/Login");
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var jobAndApps = await jobAndAppRepo.GetAllAsync(true);//will have where
            var sects = await sectionRepo.GetAllAsync();
            MyActiveApps = JobAndApplications.make(jobAndApps.Where(j => !j.Finished).ToList());
            MyCompletedApps = JobAndApplications.make(jobAndApps.Where(j => j.Finished).ToList());
            MyTodoSections = sects.Where(s => !s.Finished).ToList();
            MyDoneSections = sects.Where(s => s.Finished).ToList();
            return Page();
        }
    }
}