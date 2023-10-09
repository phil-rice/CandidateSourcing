using gui.Pages.Applications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.application;
using xingyi.job.Repository;

namespace gui.Pages
{

    public class FillInDetails
    {
        public string Candidate { get; set; }
        public string JobTitle { get; set; }

        public string InterviewTitle { get; set; }

        public List<Answer> Answers { get; set; }
    }

    [Authorize]
    public class FillInDetailsModel : PageModel
    {
        private readonly  ISectionRepository repo;

        public FillInDetailsModel(ISectionRepository repo)
        {
            this.repo = repo;
        }

        [FromRoute]
        public Guid Id { get; set; }


        [BindProperty]
        public FillInDetails Item { get; set; }
        [BindProperty]
        public string SaveAction { get; set; }
        async public Task<IActionResult> OnGetAsync()
        {
            var sect = await repo.GetByIdAsync(Id, true);
            var app = sect.Application;
            var job = app.Job;
            Item = new FillInDetails
            {
                JobTitle = job.Title,
                Candidate = app.Candidate,
                InterviewTitle = sect.Title,
                Answers = sect.Answers
             
            };
            Console.WriteLine(Item);
            return Page();
        }

        async public Task<IActionResult> OnPostAsync()
        {

            var sect = await repo.GetByIdAsync(Id, true);
            sect.Finished = SaveAction == "submit";
            for(var i = 0; i<sect.Answers.Count; i++)
            {
                sect.Answers[i].AnswerText = Item.Answers[i].AnswerText ?? "";
                sect.Answers[i].Score = Item.Answers[i].Score;
                
            }
            await repo.UpdateAsync(sect);
            return RedirectToPage("/Index");
        }
    }
}
