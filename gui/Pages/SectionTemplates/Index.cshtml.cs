using gui.GenericPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.SectionTemplates
{
    public class SectionTemplateIndexModel : GenericIndexModel<SectionTemplate, Guid, ISectionTemplateRepository, SectionTemplateWhere>
    {

        public SectionTemplateIndexModel(ISectionTemplateRepository repo) : base(repo)
        {

        }

        protected override SectionTemplateWhere where()
        {
            var Owner = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

            return new SectionTemplateWhere { Owner = Owner };
        }
    }
}
