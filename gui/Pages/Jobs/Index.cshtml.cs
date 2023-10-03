using gui.GenericPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.job.Client;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.Jobs
{
    public class JobIndexModel : GenericIndexModel<Job, Guid, IJobRepository>
    {

        public JobIndexModel(IJobRepository repo) : base(repo)
        {

        }
    }
}
