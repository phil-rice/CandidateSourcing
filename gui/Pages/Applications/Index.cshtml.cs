using gui.GenericPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.application;
using xingyi.application.Repository;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.Applications
{
    public class ApplicationsIndexModel : GenericIndexModel<Application, Guid, IApplicationRepository>
    {


        public ApplicationsIndexModel(IApplicationRepository repo) : base(repo)
        {
        }


    }
}
