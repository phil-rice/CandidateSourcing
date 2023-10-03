using gui.GenericPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.job.Client;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.Jobs
{
    public class JobViewModel : GenericViewModel<Job, Guid, IJobRepository>
    {

        public JobViewModel(IJobRepository repo) : base(repo)
        {
        }
    }
}
