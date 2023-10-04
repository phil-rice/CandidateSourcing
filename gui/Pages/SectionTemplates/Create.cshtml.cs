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
        public string QuestionFields { get; } = "Title:text,Description:text,ScoreOutOfTen:checkbox,Singleline:checkbox";
        public SectionTemplateCreateModel(ISectionTemplateRepository client) : base(client)
        {
        }
    }

}
