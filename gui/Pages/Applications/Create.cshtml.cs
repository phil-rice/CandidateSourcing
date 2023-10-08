using gui.GenericPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.application;
using xingyi.gui;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.Applications
{
    public class ApplicationDetails
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public string JobName { get; set; }
        public string Candidate { get; set; }
        public List<SectionDetail> sections { get; set; }
    }
    public class SectionDetail
    {
        public string? Title { get; set; }
        public string Who { get; set; }
        public bool? CanEditWho { get; set; }

    }

    public class ApplicationCreateModel : PageModel
    {
        private readonly IJobRepository jobRepo;
        private readonly IApplicationRepository appRepo;


        [FromRoute]
        public Guid JobId { get; set; }

        [BindProperty]
        public ApplicationDetails Item { get; set; }
        public ApplicationCreateModel(IApplicationRepository appRepo, IJobRepository jobRepo) : base()
        {
            this.appRepo = appRepo;
            this.jobRepo = jobRepo;
        }

        public async Task OnGetAsync()
        {
            var job = await jobRepo.GetByIdAsync(JobId);

            var sections = job.JobSectionTemplates.Select(jst =>
                 jst.SectionTemplate).Select(st => new SectionDetail
                 {
                     Title = st.Title,
                     Who = st.Who,
                     CanEditWho = st.CanEditWho != false
                 }).ToList();
            Item = new ApplicationDetails
            {
                JobName = job.Title,
                sections = sections,
                JobId = JobId,
                Id = Guid.Empty
            };

        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelStateHelper.DumpModelState(ModelState);
            if (ModelState.IsValid)
            {



                var id = Guid.NewGuid();
                var job = await jobRepo.GetByIdAsync(JobId);
                var sections = job.JobSectionTemplates.Select(jst =>
                                jst.SectionTemplate).Select(st =>
                                st.asSection(id, Guid.NewGuid())).ToList();
                var app = new Application
                {
                    Id = id,
                    JobId = JobId,
                    Sections = sections,
                    Candidate = Item.Candidate
                };
                Console.WriteLine(app);
                await appRepo.AddAsync(app);
                return RedirectToPage("/Jobs/View", new { id = JobId });

            };

            return Page();

        }

    }
}