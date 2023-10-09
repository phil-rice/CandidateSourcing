using xingyi.job.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xingyi.microservices.repository;
using xingyi.application;

namespace xingyi.job.Repository
{

    public class JobWhere { }
    public interface IJobAndAppRepository : IRepository<Job, Guid, JobWhere> { }
    public class JobAndAppRepository : Repository<JobDbContext, Job, Guid, JobWhere>, IJobAndAppRepository
    {
        public JobAndAppRepository(JobDbContext context) :
            base(context, context => context.Jobs,
                                 id => j => j.Id == id,
                                set => set,
                                set => set.Include(j => j.Applications).ThenInclude(a => a.Sections)
                                .Include(j => j.JobSectionTemplates).ThenInclude(jst => jst.SectionTemplate))
        {
        }
    }

    public interface IJobRepository : IRepository<Job, Guid, JobWhere> { }
    public class JobRepository : Repository<JobDbContext, Job, Guid, JobWhere>, IJobRepository
    {
        public JobRepository(JobDbContext context) :
            base(context, context => context.Jobs,
                  id => j => j.Id == id,
                  set => set,
                  set => set.Include(j => j.JobSectionTemplates)
                            .ThenInclude(jst => jst.SectionTemplate)
                            .ThenInclude(st => st.Questions))
        {
        }

        public void PrintTrackedEntities(DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries())
            {
                Console.WriteLine($"Entity Type: {entry.Entity.GetType().Name}");
                Console.WriteLine($"Entity State: {entry.State}");

                foreach (var prop in entry.OriginalValues.Properties)
                {
                    var original = entry.OriginalValues[prop];
                    var current = entry.CurrentValues[prop];
                    Console.WriteLine($"Property: {prop.Name} | Original: {original} | Current: {current}");
                }

                Console.WriteLine(new string('-', 50)); // Just a separator for readability

            }
        }

        override protected async Task<Job> populateEntityForUpdate(Job job)
        {
            //This doesn't work if the job has already been read from the database. EntityFramework is incredibly hard to work with.
            var existingJob = await GetByIdAsync(job.Id, true);
            existingJob.Title = job.Title;
            existingJob.Description = job.Description;
            existingJob.Finished = job.Finished;
            //existingJob.Owner = job.Owner; decided not to allow this
            var existingSectionids = existingJob.JobSectionTemplates.Select(jst => jst.SectionTemplateId).ToList();
            var providedSectionIds = job.JobSectionTemplates.Select(jst => jst.SectionTemplateId).ToList();
            var sectionsToDeleteIds = existingSectionids.Except(providedSectionIds).ToList();
            var sectionsToAddIds = providedSectionIds.Except(existingSectionids).ToList();

            var jstsToDelete = existingJob.JobSectionTemplates.Where(jst => sectionsToDeleteIds.Contains(jst.SectionTemplateId)).ToList();
            foreach (var jst in jstsToDelete)
            {
                existingJob.JobSectionTemplates.Remove(jst);
                _context.JobSectionTemplates.Remove(jst);  // Still remove directly from DbSet for clarity
            }
            foreach (var id in sectionsToAddIds)
            {
                var jst = new JobSectionTemplate { JobId = job.Id, Job = existingJob, SectionTemplateId = id };
                existingJob.JobSectionTemplates.Add(jst); // Update the navigational property
            }
            return existingJob;
        }




        public async Task UpdateJobFields(Job job)
        {
            _context.Entry(job).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }

    public class QuestionWhere { }
    public interface IQuestionRepository : IRepository<Question, Guid, QuestionWhere> { }
    public class QuestionRepository : Repository<JobDbContext, Question, Guid, QuestionWhere>, IQuestionRepository
    {

        public QuestionRepository(JobDbContext context) : base(context,
            context => context.Questions,
            id => q => q.Id == id,
            set => set,
            set => set)
        {
        }

    }

    public class SectionTemplateWhere { }

    public interface ISectionTemplateRepository : IRepository<SectionTemplate, Guid, SectionTemplateWhere> { }
    public class SectionTemplateRepository : Repository<JobDbContext, SectionTemplate, Guid, SectionTemplateWhere>, ISectionTemplateRepository
    {
        public SectionTemplateRepository(JobDbContext context) :
    base(context,
        context => context.SectionTemplates,
        id => st => st.Id == id,
        set => set,
        set => set.Include(s => s.Questions))
        {
        }

    }

    public class ApplicationWhere { }
    public interface IApplicationRepository : IRepository<Application, Guid, ApplicationWhere> { }
    public class ApplicationRepository : Repository<JobDbContext, Application, Guid, ApplicationWhere>, IApplicationRepository
    {
        public ApplicationRepository(JobDbContext context) :
            base(context, context => context.Applications,
                  id => j => j.Id == id,
                  set => set.Include(a => a.Id),
                  set => set.Include(a => a.Job)
                             .Include(a => a.Sections)
                            .ThenInclude(s => s.Answers))
        {

        }
    }

    public class SectionWhere { }
    public interface ISectionRepository : IRepository<Section, Guid, SectionWhere> { }
    public class SectionRepository : Repository<JobDbContext, Section, Guid, SectionWhere>, ISectionRepository
    {
        public SectionRepository(JobDbContext context) : base(context,
        context => context.Sections,
        id => st => st.Id == id,
        s => s.Include(s => s.Application).ThenInclude(a => a.Job),
        set => set
            .Include(s => s.Answers)
             .Include(s => s.Application).ThenInclude(a => a.Job)) { }

    }
    public class AnswerWhere { }
    public interface IAnswersRepository : IRepository<Answer, Guid, AnswerWhere> { }
    public class AnswersRepository : Repository<JobDbContext, Answer, Guid, AnswerWhere>, IAnswersRepository
    {
        public AnswersRepository(JobDbContext context) :
    base(context,
        context => context.Answers,
        id => st => st.Id == id,
        s => s,
        set => set)
        {
        }

    }

}
