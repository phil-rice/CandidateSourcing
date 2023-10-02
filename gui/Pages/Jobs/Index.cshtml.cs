using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.job.Client;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.Jobs
{
    public class IndexModel : PageModel
    {
        private readonly IJobClient client;
        public List<Job> Jobs { get; set; } = new List<Job>();


        public IndexModel(IJobClient client)
        {
            this.client = client;
        }
        async public void OnGet()
        {
            Jobs = await client.GetAllAsync();
        }
    }
}
