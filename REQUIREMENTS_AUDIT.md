# Meeting the requirements

## Use .Net 6.0 framework  
Each project is using 
```xml
<PropertyGroup>
   <TargetFramework>net6.0</TargetFramework>
</PropertyGroup>
```

## Use MVC for front end  
* Primarily uses Razor pages
    * The project gui has a Pages folder and the pages are in there
* There are a few controllers in the Controllers folder where the controller made more sense 

## Web API for backend
* The project `jobApì` has a Controllers folder where you can find most of the webApi
* `jobApi` is just the domain specific information about the api, the main part of the api code is in the domain neutral project `microservices`

## Entity Framework (code first approach is preferred) for database connectivity  
* In the `jobCommon` project we have the `JobDbContext` class which is the database context
* Points to note
    * Migrations folder demonstrating the use of 'code first'
    * The mixture of annotations and explicit code in `JobDbContext`. 
      * I prefer annotations and use them where possible
      * but things like unique relationships and composite primary keys require explicit code
    * The `DesignTimeDbContextFactory` which helps enormously with migrations and scaffolding
## SQL Server for database (if do not have SQL server, use Sqlite)  
* Observe the connection strings in the `appsettings.json` files (in `jobApi` and `gui`)
## Implement Authentication/Authorization (Cookies based for MVC and JWT for Web API)  
* I implemented authentication for the gui project using the `Microsoft.AspNetCore.Authentication.Google` package & Cookies
    * This can be seen in the `Startup.cs` file in the `gui` project
    * I use google as the authentication provider
    * I used this instead of the `Identity Framework` as they are about the same difficulty, and in almost every `for real` application today I would be using OAuth2 probably with an LDAP provider
* I had no need to store any personal data: 
   * the approach I took is 'anyone can be any role'. This is consistant with internet scale code rather than enterprise scale code
   * For example a person who is an HR manager might be applying for a job while recruiting and simultaneously be an interviewer
   
I didn't implement JWT for the API due to time constraints. I have done this before and it is not difficult although typically takes me a one full day to do it

# Swagger support in Web API  
* Is present.`https://localhost:7209/swagger/index.html` shows it when run from visual studio

## Validations (client side as well as server side)  
* I have only limited client side validation. I do a little: showing different things to prevent the user being able to do things
* There is full server side validation
    * Including custom validation.
    * Here is an example of a custom validator

```c#
 [ToString, Equals(DoNotAddEqualityOperators = true)]
    public class FillInAnswer : IValidatableObject
    {
      //... fields go here
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ScoreOutOfTen == true && (Score < 1 || Score > 10))
                yield return new ValidationResult("Please set the score", new[] { "AnswerText" });

            if (IsRequired == true && string.IsNullOrEmpty(AnswerText))
            {
                yield return new ValidationResult($"Please provide an value for [{Title}].", new[] { "AnswerText" });
            }
            bool number = int.TryParse(AnswerText, out _);
            if (IsNumber == true && !number)
            {
                yield return new ValidationResult($"[{Title}] must be a number", new[] { "AnswerText" });

            }
          
        }
    }
```

## CORS  
* This is present in the `Startup.cs` file in the `gui` project
* Please note that this application does not need CORS as there is only this site, and there is no moving between sites. It is still good to have though
* It is configured in the `appSettings..json` file

## Dependency Injection   
* Is heavily used. 
* The `Startup.cs` file in the `gui` project shows how it is configured
* Almost all the `controllers`, `clients` and the `repository classes` use it
* I'd like to show some example of custom dependency injection beyond 'just using it'

In the file `Startup.cs` in the `gui` project we have the following code
```c#
public void ConfigureServices(IServiceCollection services)
    {
        //...
        services.Configure<JobSettings>(Configuration.GetSection("ApiSettings"));
        services.AddHttpClient<IJobRepository, JobClient>();
        //...
    }
```
* Ìn this we can see that we configure the `JobClient` class to be populated from the `appSettings.json` file. 
* The use of this pattern means (if we choose) that each client can have its own settings. 
    * Currently we aren't doing this, but it is still a very nice and clean interface


This is made possible by the following code

```c#
    public interface IJobClient : IJobRepository
    {
    }
    public class JobSettings : HttpClientSettings { }

    public class JobClient : GenericClient<Job, Guid, JobWhere>, IJobClient
    {
        public JobClient(HttpClient httpClient, IOptions<JobSettings> jobSettings)
            : base(httpClient, jobSettings.Value.BaseUrl + "Job") {}
    }
```


## Repository pattern  
Has been heavily used
* There is a IxxxRepository for each entity
  * This is implemented to get the data from the data base
  * Exactly the same interface is used to get the data from the api via a 'client' pattern, making it seamless to switch between api and repository
* There is a generic repository, generic client and generic controller
  * These are customised for each entity
  * Making the code very DRY, easy to maintain, easy to extend and all the other good things we like (including easy testing)
   
# Good to haves

## Multilayer architecture  
For example Getting/Putting data to the database
* A razor page backing code (a controller effectively) 
* calls a `client` which wraps a `HttpClient` giving it a `IRepository` interface
* This calls a `controller` in the `jobApi` project
* The `controller` calls a `repository` which implements the `IRepository` interface
* This repository wraps the `JobDbContext` which is the database context for Entity Framework


Because of the enforced used of interfaces, and separating the different code bases into different projects this is all extremely clean. 
There are clean extension points for all the things that need to be extended 

## Exception Handling
* I currently use the default exception handling
* I catch exceptions in a few places but at this stage in the project I want to see the exceptions and not hide them!



## Response Caching  
A famous quote `There are only two hard problems in IT: Naming things and stale cache strategies`. It is trivial to add response caching, but
doing it in a way that doesn't introduce bugs will require significant analysis and work. For example when I add something to the database all
the pages that show the data need their caches to be invalidated. 

Summary: I decided not to do it

## Filters
I'm using filters quite a lot of course. I did implement my own to learn how it is done: A request/response logger. I found this incredibly useful.

Here we see it being used. It took a while to make this as clean and idiomatic as this
```c#
            app.UseRequestResponseLogging(options => { options.Enabled = false; options.Prefix= "Some prefix" });
```

The code for this is in the `microservices` project in the `Middleware` folder. Here we see how the configuration was enabled

```c#

    public class RequestResponseLoggingOptions
    {
        public string Prefix { get; set; } = string.Empty; // default is empty string
        public bool Enabled { get; set; } = true; // default is true
    }
    public static class RequestResponseLoggingExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder app, Action<RequestResponseLoggingOptions> configureOptions = null)
        {
            if (configureOptions != null)
            {
                var options = new RequestResponseLoggingOptions();
                configureOptions(options);

                // Use this overloaded method to pass the custom options directly
                return app.UseMiddleware<RequestResponseLoggingMiddleware>(options);
            }
            return app.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
```
The actual code is too long to show here. It is complicated because it has to manipulate the request/response streams: resetting them back to markers. 


<hr />

# Functional Requirements

## Summary
> For Large Organisation, it is not practical to maintain the records of the interviewing process.
  Candidate Sourcing portal enables maintenance of the interview records and score the candidates based on the discussion. Managers will login to the system and 
  will be able to review the candidature  

This has been met by the use of the `gui` project. It is a web application that allows the user to do all the things required.
It requires the `jobApi` project to be running. 


## Interviews/Rounds
> Each interview would have rounds panellist 1, panellist 2 ,HR, Manager, Recruiter, Candidate  
I was unhappy with this requirement. I felt that it was too specific and not general enough. 
I decided to make it more general.

  * I have a `Job` entity with `sectionTemplates`
  * Each `Job` (which represents a job being advertised) has a number of `sectionTemplates`
  * Each `sectionTemplate` corresponds to recording results. Such as `CandidateDetails`  or `Interviewer` results
    * Once a SectionTemplate has been created it can be used in any number of jobs. 
    * So we can set up `Candidate Details`  and `HR Interview` which might be universal
    * But we can set up `Dot net full stack interview` or `Room Cleaner` or `Scrum Master interview 1` or `Architect interview 2` which are customised to the job

To make it 'quick' to test, on page `https://localhost:7000/Info/PostingVacancies` there is a `click here to create a test job and test sections`. This can only be clicked once but creates the samples needed for the requirements

### Candidate Login
>Candidate Login : Candidate updates his details, Passport Number, Name, address, DOB. He should not be able to view panellist forms  

Candidate login is easy. 
* They access the main page and are asked to login via google.
* When logged in they are redirected to the home page
* Once on the home page they can see that they need to fill in their details. 
* They can do this by clicking on the `Candidate Details` link.
* The `sample` test job/sections has the data specified above
* Note that the Date of Birth has a date picker
* And that the validation is working: passport number is a number.



### First Round 
> Panellist 1 would login to the portal and provide feedback in various sections on a score with scale of 1 to 10 (10 being highest). Detail feedback section should allow a maximum of 1000 characters.  

The same process as the candidate: 
* They access the main page and are asked to login via google.
* When logged in they are redirected to the home page
* Once on the home page they can see that they need to fill in the result of interview round 1. 
* They can do this by clicking on the `Interview 1` link.
* They have a number of questions.
     * Some of the questions have a score (fully configurable questions)
     * There is a comments field that is required
* When finished they go back to the home page 
  * and see the work has been completed 
  * and the averaged score is given with a 'badge'
  * They can go and edit their answers if they want to
* The comments field for 'detailed feedback' is limited to 1000 characters

### Second Round 
>  Panellist 2 would login to the portal and provide feedback in various sections on a score with scale of 1 to 10 (10 being highest). Detail feedback section should allow a maximum of 1000 characters.

The same process as the candidate and interviewer 1. 

### HR Round
>  Login to the portal and provide feedback and give a score on a scale of 1 to 10(10 being highest). Detail feedback section should allow a maximum of 1000 characters. The weightage for this score is 10%  

The same process. Note that I give multiple questions, and places to give answers. If you only wanted
one question and one score, that's fully configurable.

### Weighted average
This is fully configurable. Each Section has a weighting. 

The sample test project has
* Candidate Details 0%
* Interview 1 40%
* Interview 2 50%
* Hr Interview 10%
These numbers can be seen on the gui above each section


## Manager
> Manager logs in and checks the candidate rating which is weighted average of the interviews’ score from the three rounds of the candidate   
> Search and view reports of candidate feedback

There is a whole section of the website devoted to the manager.

### What is a manager?

* Each 'job' has a set of managers. 
* This is managed on the `admìn` / `jobs` page. and then `Edit Managed By` link
* Only the job owner can do this
* They can add or remove managers

Note that for one job a person might be a candidate, for another an interviewer, and for another a manager or owner

### Manager page
Note the error that there are two proficients. This has been implemented, but the badges have different colours.

### There is a summary page for the manager on the main menu `Managed Jobs`
>*  Weighted average is > 8 then rating is Expert  
>* Weighted average is > 6 and <=7.99 then rating is Proficient  
>* Weighted average is > 4 and <=5.99 then rating is Proficient  
>* Weighted average is <4 then rating is Reject  

* This summary page shows all the jobs that the manager is managing.
* It shows, for each candidate, how the work flow is going
* If all the sections are filled in then there is a score and a `badge` that aligns with the above

### In addition there is a candidate search
If you type in a candidates email then (as long as you are a job owner, or job manager) you can see a summary of which
jobs they have applied for and how they are doing


## Data retention
> The candidate details are maintained in the organisation for 1 year 

In reality this would be a batch job that runs regularly. I have implemented it as a button on the `admin` page.

## Duplicate candidate

> and candidates rejected cant appear the interview within 6 months of their attempt. Recruiter can login and check if the candidate has appeared for interview in the last 6 months  

* When a candidate is added to a job, the system checks to see if they have been added to this job in the last six months, if so a validation error is shown.
* In addition a recruiter can search for a candidate and see all the jobs they have applied for, that they have access to (are owner or manager of)

<hr />

# Nice bits of code

## Use of parameterised partials
The index pages shows this. `_SectionToDoList` has parameters that control what is shown

## Use of custom TagHelpers
In the `TagHelpers` folder in the `gui` project
* `ScoreBadgeTagHelper` is used to show the badges
* `LabelAndxxxTagHelper` ensures the same formating/layout for textAreas, inputs and dates
* `DisplayTagHelper` was invaluable while debugging. It shows a model object on the screen`

Some of the TagHelpers call built in TagHelpers: especially to show validation code

## Use of custom validation
Described above

## Use of custom dependency injection
Described above

## Use of custom filters
Described above

## The layered architecture: 
Generic Razor Pages, Generic Controllers, Generic Clients, Generic Repositories

## Fody Library
This autogenerates toString and equals methods



