using gui.GenericPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.gui;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.SectionTemplates
{
    public class SectionTemplateCreateModel : GenericCreateModel<SectionTemplate, Guid, ISectionTemplateRepository, SectionTemplateWhere>
    {
        public string QuestionFields { get; } = SectionTemplateCommon.QuestionFields;
        public SectionTemplateCreateModel(ISectionTemplateRepository client) : base(client)
        {
        }
        public override async Task modifyItemOnCreate(SectionTemplate Item)
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

            Item.Owner = email;
        }
    }

}
