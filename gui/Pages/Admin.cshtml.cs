using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using xingyi.job.Client;
using xingyi.job.Repository;
using xingyi.jobClient.admin;
using xingyi.microservices.Client;

namespace gui.Pages
{

    [Authorize]
    public class AdminModel : PageModel
    {


        public AdminModel(AdminHttpClient client)
        {
            Client = client;
        }

        public AdminHttpClient Client { get; }

        async public Task<IActionResult> OnPostAsync()
        {
            await Client.purge();
            return RedirectToPage("/Index");
        }
    }
}
