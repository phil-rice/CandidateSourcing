using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xingyi.application;
using xingyi.gui;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IJobAndAppRepository jobAndAppRepo;
        private readonly ISectionRepository sectionRepo;

        public List<Section> MyTodoSections { get; set; }
        public List<Section> MyDoneSections { get; set; }

        public bool Nothing
        {
            get
            {
                return MyDoneSections.Count == 0 && MyTodoSections.Count == 0;
            }
        }



        public IndexModel(ILogger<IndexModel> logger, IJobAndAppRepository jobAndAppRepo, ISectionRepository sectionRepo)
        {
            _logger = logger;

            this.sectionRepo = sectionRepo;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //if (!User.Identity.IsAuthenticated) return RedirectToPage("/Identity/Login");
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var sects = await sectionRepo.GetAllAsync(new SectionWhere { Email = email });
            MyTodoSections = sects.Where(s => !s.Finished).ToList();
            MyDoneSections = sects.Where(s => s.Finished).ToList();
            return Page();
        }
    }
}