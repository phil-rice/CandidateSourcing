using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.job.Client;
using xingyi.job.Models;

namespace gui.Pages.Jobs
{
    public class JobViewModel : PageModel
    {
        private readonly IJobClient client;
        [FromRoute]
        public Guid Id { get; set; }
        [BindProperty]
        public Job Job { get; private set; }

        public JobViewModel(IJobClient client)
        {
            this.client = client;
        }


        public async Task OnGetAsync()
        {
            System.Diagnostics.Debug.WriteLine($"ID: {Id}");
            Job = await client.GetByIdAsync(Id);
        }

    }
}
