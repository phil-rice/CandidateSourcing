using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace gui.Pages.Identity
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        async public Task<IActionResult> OnGetAsync()
        {
            Name = User.Identity.Name;
            Email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            return Page();
        }
    }
}
