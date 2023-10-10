using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.application;
using xingyi.job.Repository;

namespace gui.Pages.Applications
{
    public class ViewModel : PageModel
    {
        private readonly IApplicationRepository appRepo;

        [FromRoute]
        public Guid Id { get; set; }
        [BindProperty]
        public Application Item { get; set; }

        public ViewModel(IApplicationRepository appRepo)
        {
            this.appRepo = appRepo;
        }

        public async Task OnGetAsync()
        {
            Item = await appRepo.GetByIdAsync(Id, true);
        }
    }
}
