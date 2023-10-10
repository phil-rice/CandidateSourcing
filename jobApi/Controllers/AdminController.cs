using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xingyi.job;
using xingyi.job.Repository;

namespace jobApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly JobDbContext context;

        public AdminController(JobDbContext context)
        {
            this.context = context;
        }

        [HttpPost("purge")]
        async public Task<IActionResult> Purge()
        {
            var date = DateTime.Now.AddMonths(-6);
            context.Database.ExecuteSqlInterpolated(
                $"DELETE FROM Applications  WHERE DateCreated < {date}");
            return NoContent();
        }
    }
}
