using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.gui;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.Manager
{
    public class IndexModel : PageModel
    {
        private readonly IManagedByRepository manRepo;

        public List<JobAndApplications> Items { get; set; } = new List<JobAndApplications>();

        public IndexModel(IManagedByRepository manRepo)
        {
            this.manRepo = manRepo;
        }

        async public Task<IActionResult> OnGetAsync()
        {
            var mbs = await manRepo.GetAllAsync(new ManagedByWhere { ManagedBy = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value }, true);
            foreach (var mb in mbs)
            {
                var job = mb.Job;
                var apps = job.Applications;
                Items.Add(new JobAndApplications { Job = job, Applications = apps });
            }
            return Page();
        }
    }
}
