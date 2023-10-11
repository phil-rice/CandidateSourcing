using gui.GenericPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.job.Client;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.Jobs
{

    public class JobIndexModel : GenericIndexModel<Job, Guid, IJobRepository, JobWhere>
    {

        public JobIndexModel(IJobRepository repo) : base(repo)
        {

        }

        override public async Task OnGetAsync()
        {
            Items = await repo.GetAllAsync(where(), true);
            foreach (var item in Items)
                Console.WriteLine("Item: " + item);
        }
        protected override JobWhere where()
        {
            var Owner = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            return new JobWhere { Owner = Owner };
        }
    }
}
