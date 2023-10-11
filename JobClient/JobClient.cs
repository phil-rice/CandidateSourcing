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
using xingyi.microservices.Client;
using static xingyi.job.Repository.ApplicationRepository;
using xingyi.application;
using Microservices;
using System.Net;
using Microsoft.CodeAnalysis.CodeFixes;

namespace xingyi.job.Client
{
    public interface IJobAndAppClient : IJobAndAppRepository
    {
    }
    public class JobAndAppSettings : HttpClientSettings { }

    public class JobAndAppClient : GenericClient<Job, Guid, JobAndAppWhere>, IJobAndAppClient
    {
        public JobAndAppClient(HttpClient httpClient, IOptions<JobAndAppSettings> jobSettings)
            : base(httpClient, jobSettings.Value.BaseUrl + "JobAndApp")
        {
        }

    }
    public interface IJobClient : IJobRepository
    {
    }
    public class JobSettings : HttpClientSettings { }

    public class JobClient : GenericClient<Job, Guid, JobWhere>, IJobClient
    {
        public JobClient(HttpClient httpClient, IOptions<JobSettings> jobSettings)
            : base(httpClient, jobSettings.Value.BaseUrl + "Job")
        {
        }

    }

    public interface IManagedByClient : IManagedByRepository
    {
    }
    public class ManagedBySettings : HttpClientSettings { }

    public class ManagedByClient : GenericClient<ManagedBy, GuidAndEmail, ManagedByWhere>, IManagedByClient
    {
        public ManagedByClient(HttpClient httpClient, IOptions<ManagedBySettings> settings) :
            base(httpClient, settings.Value.BaseUrl + "ManagedBy", id => $"{id.Id}/{WebUtility.UrlEncode(id.Email??"")}")
        {
        }

        
    }
    public interface ISectionTemplateClient : ISectionTemplateRepository
    {
    }
    public class SectionTemplateSettings : HttpClientSettings { }

    public class SectionTemplateClient : GenericClient<SectionTemplate, Guid, SectionTemplateWhere>, ISectionTemplateClient
    {
        public SectionTemplateClient(HttpClient httpClient, IOptions<SectionTemplateSettings> settings) : base(httpClient, settings.Value.BaseUrl + "SectionTemplate")
        {
        }
    }
    public interface IQuestionClient : IQuestionRepository
    {
    }
    public class QuestionSettings : HttpClientSettings { }

    public class QuestionClient : GenericClient<Question, Guid, QuestionWhere>, IQuestionClient
    {
        public QuestionClient(HttpClient httpClient, IOptions<QuestionSettings> settings) : base(httpClient, settings.Value.BaseUrl + "Question")
        {
        }
    }
    public interface IApplicationClient : IApplicationRepository
    {
    }
    public class ApplicationSettings : HttpClientSettings { }

    public class ApplicationClient : GenericClient<Application, Guid, ApplicationWhere>, IApplicationClient
    {
        public ApplicationClient(HttpClient httpClient, IOptions<ApplicationSettings> settings)
            : base(httpClient, settings.Value.BaseUrl + "Application")
        {
        }

    }



    public interface ISectionClient : ISectionRepository
    {
    }
    public class SectionSettings : HttpClientSettings { }

    public class SectionClient : GenericClient<Section, Guid, SectionWhere>, ISectionClient
    {
        public SectionClient(HttpClient httpClient, IOptions<SectionSettings> settings)
            : base(httpClient, settings.Value.BaseUrl + "Section")
        {
        }

    }

    public interface IAnswerClient : IAnswersRepository
    {
    }
    public class AnswerSettings : HttpClientSettings { }

    public class AnswerClient : GenericClient<Answer, Guid, AnswerWhere>, IAnswerClient
    {
        public AnswerClient(HttpClient httpClient, IOptions<AnswerSettings> settings)
            : base(httpClient, settings.Value.BaseUrl + "Answer")
        {
        }

    }
}
