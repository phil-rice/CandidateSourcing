using gui.GenericPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.gui;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.SectionTemplates
{
    public class SectionTemplateEditModel : GenericEditModel<SectionTemplate, Guid, ISectionTemplateRepository>
    {
        public SectionTemplateEditModel(ISectionTemplateRepository client) : base(client)
        {
        }
    }
}
