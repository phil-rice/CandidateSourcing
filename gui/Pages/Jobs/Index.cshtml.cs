using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.job.Client;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages.Jobs
{
    public class IndexModel : PageModel
    {

        public IndexModel(IJobClient client)
        {
            this.client = client;

        }


        public List<Job> Jobs { get; private set; }

        public async Task OnGetAsync()
        {
            Jobs = await client.GetAllAsync();
        }



        private IJobClient client { get; }


    }
}
