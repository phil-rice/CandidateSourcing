using gui.GenericPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.gui;
using xingyi.job.Client;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.Jobs
{

    [ToString, Equals(DoNotAddEqualityOperators = true)]
    public class SectionTemplateAndChecked
    {
        public String? Title { get; set; }
        public Guid Id { get; set; }
        public bool IsChecked { get; set; }
    }
    public class JobAndSectionTemplates
    {
        public Job Job { get; set; }
        public List<SectionTemplateAndChecked> SectionTemplates { get; set; }

        public List<JobSectionTemplate> templates(Job job)
        {
            return SectionTemplates.Where(s => s.IsChecked)
                .Select(S => new JobSectionTemplate { JobId = job.Id, SectionTemplateId = S.Id })
                .ToList();
        }
    }
    public class JobCreateModel : PageModel
    {
        private readonly IJobRepository jobRepo;
        private readonly ISectionTemplateRepository stRepo;

        [BindProperty]
        public JobAndSectionTemplates Item { get; set; }
        public JobCreateModel(IJobRepository jobRepo, ISectionTemplateRepository stRepo)
        {
            this.jobRepo = jobRepo;
            this.stRepo = stRepo;
        }

        public async Task OnGetAsync()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var Job = new Job { Owner = email == null ? "No email" : email };
            Item = new JobAndSectionTemplates
            {
                Job = Job,
                SectionTemplates = (await stRepo.GetAllAsync())
                .Select(s => new SectionTemplateAndChecked { Id = s.Id, Title = s.Title, IsChecked=Job.contains(s)
                }).ToList()
            };
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var templates = Item.templates(Item.Job);
            foreach (var st in templates)
                Console.WriteLine(st);
            Item.Job.JobSectionTemplates = templates;
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
