using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.common;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.Jobs
{
    [ToString, Equals(DoNotAddEqualityOperators = true)]
    public class SectionTemplateAndChecked
    {
        public String? Title { get; set; }
        public String? HelpText { get; set; }   
        public Guid Id { get; set; }
        public bool IsChecked { get; set; }
    }
    [ToString, Equals(DoNotAddEqualityOperators = true)]
    public class JobAndSectionTemplates
    {
        public Job Job { get; set; }
        public List<SectionTemplateAndChecked> SectionTemplates { get; set; } = new List<SectionTemplateAndChecked>();

        public List<JobSectionTemplate> templates(Job job)
        {
            return SafeHelpers.safeEnumerable(SectionTemplates).Where(s => s.IsChecked)
                .Select(S => new JobSectionTemplate { JobId = job.Id, SectionTemplateId = S.Id })
                .ToList();
        }
    }

    abstract public class AbstractJobPageModel : PageModel
    {
        protected readonly IJobRepository jobRepo;
        protected readonly ISectionTemplateRepository stRepo;

        [BindProperty]
        public JobAndSectionTemplates Item { get; set; }
        public AbstractJobPageModel(IJobRepository jobRepo, ISectionTemplateRepository stRepo)
        {
            this.jobRepo = jobRepo;
            this.stRepo = stRepo;
        }

        async protected Task populateItem(Job job, string email)
        {
            var sts = await stRepo.GetAllAsync(new SectionTemplateWhere { Owner = email});
            var stAndChecked = sts
                .Select(s => new SectionTemplateAndChecked
                {
                    Id = s.Id,
                    Title = s.Title,
                    HelpText = s.HelpText,
                    IsChecked = job.contains(s)
                }).ToList();
            Item = new JobAndSectionTemplates
            {
                Job = job,
                SectionTemplates = SafeHelpers.safeList(stAndChecked)
            };
        }

        protected void addTemplatesToJob()
        {
            var templates = Item.templates(Item.Job);
            Item.Job.JobSectionTemplates = templates;
        }
    }
}
