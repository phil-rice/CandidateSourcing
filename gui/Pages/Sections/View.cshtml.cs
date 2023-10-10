using gui.Pages.Applications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.application;
using xingyi.job.Repository;

namespace gui.Pages.Sections
{
    public class ViewModel : PageModel
    {
        [FromRoute]
        public Guid Id { get; set; }
        [BindProperty]
        public Section Item { get; set; }
        public ISectionRepository SecRepo { get; }

        public ViewModel(ISectionRepository secRepo)
        {
            SecRepo = secRepo;
        }

        async public Task<IActionResult> OnGetAsync()
        {
            Item = await SecRepo.GetByIdAsync(Id, true);
            return Page();
        }
    }
}
