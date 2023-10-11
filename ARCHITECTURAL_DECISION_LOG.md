# Why codefirst  database 
* The 'database first' approach automatic scaffolding is not up to the task of many/many relatonships. It only generates Job, SectionTemplate and Question
* I will learn more about how things work if I have to do it this way

# Why an API?
* For this application an API is contra-indicated. 
* The application is is the only user of the database, and is itself effectively an api
* Everything is made harder: the Entity Framework is not designed for this, and the scaffolding is not designed for this.

However it was a requirement

# Why have customisable jobs

In all the interviewing I have done the questions and processes have varied
* The questions are especially different
* The process is sometimes different (I have had three or four different stages)
* I didn't want to hard code this for a single job: I wanted to allow different jobs to be created

This forced me to learn more about C#, Dotnet and Entity Framework than I would have done otherwise so that was a very positive outcome

# Why Use Razor Pages
Again this is contra-indicated. 
* Typescript frameworks such as React are enormously easier to use, far more composible, and would have taken a fraction of the time as well as giving a better user experience
* The use of JWT would be much easier and allow access to APIs directly from the browser removing whole layers of complexity without impacting security

However it was a requirement

# Why the Repository Pattern

This pattern can be summarised as follows:
* There is an interface IRepository
* It is implemented by the database accessor
* It is implemented by the HttpClient that calls it
* There is a generic controller that implements the calls the Http client needs to use

```csharp
    public interface IRepository<T, Id, Where> where T : class where Where : IRepositoryWhere<T>
    {
        Task<List<T>> GetAllAsync(Where where, Boolean eagerLoad = false);
        Task<T> GetByIdAsync(Id id, Boolean eagerLoad = true);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(Id id);
    }
```

There are a number of generics
* T is the entity (Job/Application/Question/Answer...)
* Id is the type of the primary key (Usually a primary key, but can be a composite key)
* Where is the type of the where clause (IRepositoryWhere<T>)

So why?
* The generic code was quite simple
* The code was very easy to test
* The code was very easy to implement for most of the entities (everything except `Job`  which turned out to be painful)

Here is all the code for the Answer entity. This creates a client that calls a controller that gets it's data using EntityFramework
```
 public class AnswerWhere : EmptyRepositoryWhere<Answer>
    {
    }
    public interface IAnswersRepository : IRepository<Answer, Guid, AnswerWhere> { }
    public class AnswersRepository : Repository<JobDbContext, Answer, Guid, AnswerWhere>, IAnswersRepository
    {
        public AnswersRepository(JobDbContext context) :
    base(context,
        context => context.Answers,
        id => st => st.Id == id,
        s => s,
        set => set,
        orderFn: set => set.OrderBy(s => s.Title),
          postGetMutate: s => { })        { }
    }

    public interface IAnswerClient : IAnswersRepository    {    }
    
    public class AnswerSettings : HttpClientSettings { }

    public class AnswerClient : GenericClient<Answer, Guid, AnswerWhere>, IAnswerClient
    {
        public AnswerClient(HttpClient httpClient, IOptions<AnswerSettings> settings)
            : base(httpClient, settings.Value.BaseUrl + "Answer")        {
        }
    }

    public class AnswerController : GenericEmptyWhereController<Answer, Guid, AnswerWhere>
    {
        public AnswerController(IAnswersRepository repository) : base(repository, q => q.Id, new AnswerWhere()) { }
    }
}
```





