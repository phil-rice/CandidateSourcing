using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.application;
using xingyi.gui;
using xingyi.job.Repository;

namespace gui.Pages.Applications
{
    [Authorize]
    public class ApplicationEditModel : PageModel
    {
        private readonly IApplicationRepository appRepo;

        [FromRoute]
        public Guid Id { get; set; }
        [BindProperty]
        public ApplicationDetails Item { get; set; }

        public ApplicationEditModel(IApplicationRepository appRepo)
        {
            this.appRepo = appRepo;
        }


        public async Task OnGetAsync()
        {
            var app = await appRepo.GetByIdAsync(Id);
            var job = app.Job;
            var sections = app.Sections.Select(sect => new SectionDetail
            {
                Title = sect.Title,
                Who = sect.Who,
                CanEditWho = sect.CanEditWho != false
            }).ToList();
            Item = new ApplicationDetails
            {
                Candidate = app.Candidate,
                JobName = job.Title,
                sections = sections,
                JobId = job.Id,
                Id = Id
            };

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var app = await appRepo.GetByIdAsync(Id);
            ModelStateHelper.DumpModelState(ModelState);
            if (ModelState.IsValid)
            {
                for (var i = 0; i < app.Sections.Count(); i++)
                {
                    var sect = app.Sections.ToList()[i];
                    sect.Who = Item.sections[i].Who;
                }
                await appRepo.UpdateAsync(app);
                return RedirectToPage("/Applications/View", new { Id = app.Id });
            }
            else
            {
                for (var i = 0; i < app.Sections.Count; i++)
                {
                    var job = app.Job;
                    var sections = app.Sections.Select(sect => new SectionDetail
                    {
                        Title = sect.Title,
                        Who = Item.sections[i].Who,
                        CanEditWho = sect.CanEditWho != false
                    }).ToList();
                    Item = new ApplicationDetails
                    {
                        Candidate = app.Candidate,
                        JobName = job.Title,
                        sections = sections,
                        JobId = job.Id,
                        Id = Id
                    };
                }
                return Page();
            }

        }
    }

}