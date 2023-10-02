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



namespace xingyi.job.Client
{
    public interface IJobClient : IJobRepository
    {
    }


    public class JobClient : GenericClient<Job>, IJobClient
    {
        //TODO how to do this...
        private const string BaseUrl = "http://localhost/api/Jobs"; // Replace with your API's address

        public JobClient(HttpClient httpClient) : base(httpClient, BaseUrl)
        {
        }
    }
    public interface ISectionTemplateClient : ISectionTemplateRepository
    {
    }

    public class SectionTemplateClient : GenericClient<SectionTemplate>, ISectionTemplateClient
    {
        //TODO how to do this...
        private const string BaseUrl = "http://localhost/api/SessionTemplate"; // Replace with your API's address

        public SectionTemplateClient(HttpClient httpClient) : base(httpClient, BaseUrl)
        {
        }
    }
}
