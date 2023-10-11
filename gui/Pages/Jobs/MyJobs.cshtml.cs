using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.gui;
using xingyi.job.Repository;

namespace gui.Pages.Jobs
{
    [Authorize]
    public class MyJobsModel : PageModel
    {
        public List<JobAndApplications> MyActiveApps { get; set; }
        public List<JobAndApplications> MyCompletedApps { get; set; }
        private readonly IJobAndAppRepository jobAndAppRepo;

        public bool Nothing
        {
            get
            {
                return MyActiveApps.Count == 0 && MyCompletedApps.Count == 0;
            }
        }
        public MyJobsModel(IJobAndAppRepository jobAndAppRepo)
        {
            this.jobAndAppRepo = jobAndAppRepo;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var jobAndApps = await jobAndAppRepo.GetAllAsync(new JobAndAppWhere { Owner = email }, true);//will have where
            MyActiveApps = JobAndApplications.make(jobAndApps.Where(j => !j.Finished).ToList());
            MyCompletedApps = JobAndApplications.make(jobAndApps.Where(j => j.Finished).ToList());
            return Page();
        }
    }
}
