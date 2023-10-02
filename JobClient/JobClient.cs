using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xingyi.job.Models;
using xingyi.job.Repository;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace xingyi.job.Client
{
    public interface IJobClient : IJobRepository
    {
    }
    public class JobSettings : HttpClientSettings { }

    public class JobClient : GenericClient<Job>, IJobClient
    {
        public JobClient(HttpClient httpClient, IOptions<JobSettings> jobSettings) : base(httpClient, jobSettings.Value.BaseUrl+ "Job")
        {
        }
    }
    public interface ISectionTemplateClient : ISectionTemplateRepository
    {
    }
    public class SectionTemplateSettings: HttpClientSettings {}

    public class SectionTemplateClient : GenericClient<SectionTemplate>, ISectionTemplateClient
    {
        public SectionTemplateClient(HttpClient httpClient, SectionTemplateSettings settings): base(httpClient, settings.BaseUrl+"SectionTemplate")
        {
        }
    }
}
