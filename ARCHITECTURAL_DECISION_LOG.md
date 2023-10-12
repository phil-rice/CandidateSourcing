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






# Why generic tests

I have some tests that make it easy to test the repository pattern. They are generic and can be used for any entity. Here is the code for the Job entity

```csharp
	public class JobTests : GenericRepositoryTests<Job, Guid, JobWhere>
	{
		public JobTests() : base(new JobWhere()) { }
	}
```

They also have a test fixture what is populated with data of various types

## So Why?

It made it trivialy easy to test the repository pattern. All the major entities are tested, and I know the code works and will keep working

# Contract testing
This is for testing between the client and controller

There is a defect in the DotNet Pact tests that means only the first contract test run passes. I 
have notified them and provided sample code. However I can manually run them one at a time and they all pass

## So Why?
Contract testing checks the http communications between the client and the 
controller. It is a very good way of testing that the client and controller 
are working together correctly. It ensures that urls / encoding / decoding 
/ headers/status codes etc are as expected.

# Why no Gui testing
Actually I made a mistake. I thought I didn't have time. But I think it 
would have been quicker if I had some reliable selenium tests. As ever it
was a false economy to skip testing...

# Why are the Help pages Razor Page?

When I started I expected them to have a little dynamic content. In practice they have
stayed 'pure html'. It was no extra work to make them this way, and
the effort of moving them is none zero, so I left them like this

I do expect them to become dynamic in the future if the project continued.

# Why is the API not versioned

I have a rabid dislike of versioned APIs. When making
a public API perhaps there is a case for it.

The way to view it is 'what is the fan out of the API'. 
This API has only one client (fan out of one), the client is tightly coupled
to it. The cost of versioning is very high and there
is literally no value for a fanout of one.

# Why are all the questions in alphabetical order?
It's consistant and easy, and fine to get going

It's fine for a 'minimum viable product' but 
I would add an 'ordering' to the questions in the future





