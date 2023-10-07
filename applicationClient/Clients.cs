using Microsoft.Extensions.Options;
using xingyi.application;
using xingyi.application.Repository;
using xingyi.microservices.Client;
using static xingyi.application.Repository.ApplicationRepository;

namespace applicationClient
{
    public interface IApplicationClient : IApplicationRepository
    {
    }
    public class ApplicationSettings : HttpClientSettings { }

    public class ApplicationClient : GenericClient<Application, Guid>, IApplicationClient
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

    public class SectionClient : GenericClient<Section, Guid>, ISectionClient
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

    public class AnswerClient : GenericClient<Answer, Guid>, IAnswerClient
    {
        public AnswerClient(HttpClient httpClient, IOptions<AnswerSettings> settings)
            : base(httpClient, settings.Value.BaseUrl + "Answer")
        {
        }

    }
}