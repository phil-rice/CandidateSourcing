using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using xingyi.common;
using xingyi.job.Models;
using xingyi.job.Repository;
using static gui.Controllers.TestDataMaker;
namespace gui.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
    public class TestDataController : Controller
    {
        private readonly IJobRepository jobRepo;
        private readonly ISectionTemplateRepository stRepo;

        public TestDataController(IJobRepository jobRepo, ISectionTemplateRepository stRepo)
        {
            this.jobRepo = jobRepo;
            this.stRepo = stRepo;
        }
        [HttpPost]
        async public Task<IActionResult> Create()
        {
            try
            {
                var owner = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
                var sts = new List<SectionTemplate> {
                candidateSt(owner),
                interview1(owner),
                interview2(owner),
                hrInterview(owner)
            };
                var job = testJob(owner, sts);
                foreach (var st in sts)
                {
                    await stRepo.AddAsync(st);
                }
                //Wow so complicated with Entityframework! 
                var jsts = job.JobSectionTemplates;
                job.JobSectionTemplates = new List<JobSectionTemplate>();
                await jobRepo.AddAsync(job);
                job.JobSectionTemplates = jsts;
                await jobRepo.UpdateAsync(job);
            }catch (Exception ex)
            {
                   System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return RedirectToPage("/Index");
        }
    }
}
