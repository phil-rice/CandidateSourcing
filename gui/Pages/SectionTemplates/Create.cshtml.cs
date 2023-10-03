using gui.GenericPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.gui;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.SectionTemplates
{
    public class SectionTemplateCreateModel : GenericCreateModel<SectionTemplate, Guid, ISectionTemplateRepository>
    {
        public SectionTemplateCreateModel(ISectionTemplateRepository client) : base(client)
        {
        }
    }

}
