using gui.Pages.Applications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using xingyi.application;
using xingyi.gui;
using xingyi.job.Repository;

namespace gui.Pages
{

      

    public class FillInDetails 
    {
        public string? Candidate { get; set; }
        public string? JobTitle { get; set; }

        public string? InterviewTitle { get; set; }

        public List<FillInAnswer> Answers { get; set; }

    }

    public class FillInAnswer: IValidatableObject
    {
        public string? Title { get; set; } = null!;
        public string? HelpText { get; set; }
        public bool? ScoreOutOfTen { get; set; }
        public bool? Singleline { get; set; }
        public bool? IsRequired { get; set; }
        public bool? IsNumber { get; set; }
        public int Score { get; set; }
    
        public string? AnswerText { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(AnswerText))
            {
                yield return new ValidationResult($"Please provide an value for [{Title}].", new[] { "AnswerText" });
            }
        }
    }


    [Authorize]
    public class FillInDetailsModel : PageModel
    {
        private readonly ISectionRepository repo;

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
                Answers = sect.Answers.Select(a => new FillInAnswer
                {
                    Title = a.Title,
                    HelpText = a.HelpText,
                    ScoreOutOfTen = a.ScoreOutOfTen,
                    Singleline = a.Singleline,
                    IsNumber = a.IsNumber,
                    IsRequired = a.IsRequired,
                    AnswerText = a.AnswerText ?? ""
                }).ToList()

            };
            Console.WriteLine(Item);
            return Page();
        }

        async public Task<IActionResult> OnPostAsync()
        {
            ModelStateHelper.DumpModelState(ModelState);
            var sect = await repo.GetByIdAsync(Id, true);
            if (ModelState.IsValid)
            {

                sect.Finished = SaveAction == "submit";
                for (var i = 0; i < sect.Answers.Count; i++)
                {
                    sect.Answers[i].AnswerText = Item.Answers[i].AnswerText ?? "";
                    sect.Answers[i].Score = Item.Answers[i].Score;

                }
                await repo.UpdateAsync(sect);
                return RedirectToPage("/Index");
            }
            else
            {
                Item.JobTitle = sect.Application.Job.Title;
                Item.Candidate = sect.Application.Candidate;
                for (var i = 0; i < sect.Answers.Count; i++)
                {
                    Item.Answers[i].Title = sect.Answers[i].Title;
                    Item.Answers[i].HelpText = sect.Answers[i].HelpText;
                    Item.Answers[i].IsNumber = sect.Answers[i].IsNumber;
                    Item.Answers[i].IsRequired = sect.Answers[i].IsRequired;
                    Item.Answers[i].ScoreOutOfTen = sect.Answers[i].ScoreOutOfTen;
                    Item.Answers[i].Singleline = sect.Answers[i].Singleline;
                }
            }
            return Page();
        }
    }
}
