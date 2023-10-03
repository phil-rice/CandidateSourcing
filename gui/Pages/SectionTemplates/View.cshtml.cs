using gui.GenericPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.SectionTemplates
{
    public class SectionTemplateViewModel : GenericViewModel<SectionTemplate, Guid, ISectionTemplateRepository>
    {

        public SectionTemplateViewModel(ISectionTemplateRepository repo) : base(repo)
        {
        }
    }
}
