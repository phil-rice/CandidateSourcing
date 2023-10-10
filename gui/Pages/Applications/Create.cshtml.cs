using gui.GenericPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using xingyi.application;
using xingyi.gui;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.Applications
{
  
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
            var job = await jobRepo.GetByIdAsync(JobId);
            ModelStateHelper.DumpModelState(ModelState);
            if (ModelState.IsValid)
            {
                var id = Guid.NewGuid();
                job.PostGet();

                var sections = new List<Section>();

                var sumOfWeightings = 0;
                var maxScore = 0;
                for (var i = 0; i < job.JobSectionTemplates.Count(); i++)
                {
                    var st = job.JobSectionTemplates.ToList()[i].SectionTemplate;
                    sumOfWeightings += st.Weighting;
                    var sect = st.asSection(id, Guid.NewGuid());
                    if (sect.Who == "The Candidate")
                        sect.Who = Item.Candidate;
                    else
                        sect.Who = Item.sections[i].Who;

                    sections.Add(sect);
                }
                var app = new Application
                {
                    Id = id,
                    JobId = JobId,
                    Sections = sections,
                    SumOfWeightings = sumOfWeightings,
                    Candidate = Item.Candidate
                };
                Console.WriteLine(app);
                await appRepo.AddAsync(app);
                return RedirectToPage("/Index", new { id = JobId });

            }
            else
            {
                for (var i = 0; i < Item.sections.Count; i++)
                {
                    var st = job.JobSectionTemplates.ToList()[i].SectionTemplate;
                    Item.sections[i].Title = st.Title;
                    Item.sections[i].CanEditWho = st.CanEditWho != false;
                    if (st.Who == "The Candidate")
                    {
                        Item.sections[i].Who = "The Candidate";
                    }
                }
            }

            return Page();
        }

    }
}
