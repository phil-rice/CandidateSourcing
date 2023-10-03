using gui.GenericPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.gui;
using xingyi.job.Client;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.Jobs
{
    public class JobEditModel : GenericEditModel<Job, Guid, IJobRepository>
    {
        public JobEditModel(IJobRepository client) : base(client)
        {
        }
    }
}
