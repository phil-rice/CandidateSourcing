using xingyi.application;
using xingyi.job.Models;

namespace xingyi.gui
{
    public class JobAndApplications
    {
        public Job Job;
        public List<Application> Applications;

        public static List<JobAndApplications> make(List<Application> apps)
        {
            return apps.GroupBy(a => a.JobId).Select(g => new JobAndApplications { Job = g.First().Job, Applications = g.ToList() }).ToList();
        }
        public static List<JobAndApplications> make(List<Job> jobs)
        {
            return jobs.Select(j => new JobAndApplications { Job = j, Applications = j.Applications.ToList() }).ToList();
        }
    }
}
