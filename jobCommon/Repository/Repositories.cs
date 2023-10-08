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
    public interface IJobAndAppRepository : IRepository<Job, Guid> { }
    public class JobAndAppRepository : Repository<JobDbContext, Job, Guid>, IJobAndAppRepository
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

    public interface IJobRepository : IRepository<Job, Guid> { }
    public class JobRepository : Repository<JobDbContext, Job, Guid>, IJobRepository
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

        override protected async Task populateEntityForUpdate(Job job)
        {
            var sectionTemplateIds = job.JobSectionTemplates.Select(jst => jst.SectionTemplateId).ToList();
            var sectionTemplates = await _context.SectionTemplates
                                                 .Where(st => sectionTemplateIds.Contains(st.Id))
                                                 .ToListAsync();
            foreach (var jst in job.JobSectionTemplates)
            {
                jst.SectionTemplate = sectionTemplates.FirstOrDefault(st => st.Id == jst.SectionTemplateId);
                jst.Job = job;
                try
                {
                    _context.JobSectionTemplates.Add(jst); //if the job isn't already in the database then this won't be and the relationships won't be updated
                }
                catch (InvalidOperationException ex)
                {
                    //We are ok with this. Perhaps it came from the database already instead of from an api
                }

            }


            // Get all JobSectionTemplate IDs associated with this job from the database
            var existingSectionIds = await _context.JobSectionTemplates
                                                   .Where(jst => jst.JobId == job.Id)
                                                   .Select(jst => jst.SectionTemplateId)
                                                   .ToListAsync();

            // Get the SectionTemplate IDs from the provided job object
            var providedSectionIds = job.JobSectionTemplates.Select(jst => jst.SectionTemplateId).ToList();

            // Identify the JobSectionTemplates that are in the database but not in the provided job object
            var sectionsToDeleteIds = existingSectionIds.Except(providedSectionIds).ToList();

            if (sectionsToDeleteIds.Any())
            {
                // Delete the identified JobSectionTemplates
                var sectionsToDelete = _context.JobSectionTemplates
                                               .Where(jst => jst.JobId == job.Id && sectionsToDeleteIds.Contains(jst.SectionTemplateId));
                _context.JobSectionTemplates.RemoveRange(sectionsToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateJobFields(Job job)
        {
            _context.Entry(job).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }

    public interface IQuestionRepository : IRepository<Question, Guid> { }
    public class QuestionRepository : Repository<JobDbContext, Question, Guid>, IQuestionRepository
    {

        public QuestionRepository(JobDbContext context) : base(context,
            context => context.Questions,
            id => q => q.Id == id,
            set => set,
            set => set)
        {
        }

    }

    public interface ISectionTemplateRepository : IRepository<SectionTemplate, Guid> { }
    public class SectionTemplateRepository : Repository<JobDbContext, SectionTemplate, Guid>, ISectionTemplateRepository
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
    public interface IApplicationRepository : IRepository<Application, Guid> { }
    public class ApplicationRepository : Repository<JobDbContext, Application, Guid>, IApplicationRepository
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

    public interface ISectionRepository : IRepository<Section, Guid> { }
    public class SectionRepository : Repository<JobDbContext, Section, Guid>, ISectionRepository
    {
        public SectionRepository(JobDbContext context) :
    base(context,
        context => context.Sections,
        id => st => st.Id == id,
        s => s,
        set => set.Include(s => s.Answers))
        {
        }

    }
    public interface IAnswersRepository : IRepository<Answer, Guid> { }
    public class AnswersRepository : Repository<JobDbContext, Answer, Guid>, IAnswersRepository
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
