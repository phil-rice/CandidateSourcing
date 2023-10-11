using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using xingyi.application;
using xingyi.common;

namespace xingyi.job.Models
{

    [ToString, Equals(DoNotAddEqualityOperators = true)]
    [DebuggerDisplay("Job: (Id: {Id}, Title: {Title})")]
    public partial class Job
    {
        public void PostGet()

        {
            if (JobSectionTemplates != null)
                JobSectionTemplates.Sort((st1, st2) => st1.SectionTemplate.Title.CompareTo(st2.SectionTemplate.Title));
            if (Applications != null)
            {
                Applications.Sort((a1, a2) => a1.Candidate.CompareTo(a2.Candidate));
                foreach (var a in Applications) a.PostGet();
            }
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }


        [Required]
        [StringLength(255)]
        public string Owner { get; set; } = null!;

        public bool Finished { get; set; } = false;

        //Navigation
        [InverseProperty("Job")]
        public virtual List<JobSectionTemplate> JobSectionTemplates { get; set; } = new List<JobSectionTemplate>();

        public string GetManagedByEmails()
        {
            return string.Join(", ", ManagedBy.Select(mb => mb.Email));
        }
        [InverseProperty("Job")]
        public List<ManagedBy> ManagedBy { get; set; } = new List<ManagedBy>();

        [InverseProperty("Job")]
        public List<Application> Applications { get; set; } = new List<Application>();
        public bool contains(SectionTemplate st)
        {
            return JobSectionTemplates.Any(jst => jst.SectionTemplate.Id == st.Id);
        }
    }
    [ToString, Equals(DoNotAddEqualityOperators = true)]
    [DebuggerDisplay("ManagedBy: (Job: {JobId}, Email: {Email})")]
    public class ManagedBy
    {
        public Guid JobId { get; set; }
        public string Email { get; set; }

        [ForeignKey("JobId")]
        public Job? Job { get; set; }  // Added this navigation property


        public void PostGet()
        {
            if (Job != null) Job.PostGet();
        }
    }



    [ToString, Equals(DoNotAddEqualityOperators = true)]
    [DebuggerDisplay("JobSectionTemplate: (Job: {JobId}, SectionTemplateId: {SectionTemplateId})")]
    public class JobSectionTemplate
    {
        [Required]
        [Key]
        public Guid JobId { get; set; }

        [Required]
        public Guid SectionTemplateId { get; set; }

        //Navigation
        [ForeignKey("JobId")]
        [JsonIgnore]
        [IgnoreDuringToString]
        public Job? Job { get; set; }



        [ForeignKey("SectionTemplateId")]
        [IgnoreDuringToString]
        public SectionTemplate? SectionTemplate { get; set; }


    }
    [ToString, Equals(DoNotAddEqualityOperators = true)]
    [DebuggerDisplay("SectionTemplate: (Id: {Id}, Owner: {Owner}, Title: {Title})")]
    public partial class SectionTemplate
    {
        public void PostGet()

        {
            Questions.Sort((q1, q2) => q1.Title.CompareTo(q2.Title));
        }

        [Key]
        public Guid Id { get; set; }

        public string Owner { get; set; }
        public bool? CanEditWho { get; set; }
        public int Weighting { get; set; }
        public bool RequireComments { get; set; } = true;
        public string? CommentsMessage { get; set; } = "";
        public string Who { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; } = null!;

        [StringLength(255)]
        public string? HelpText { get; set; }

        //Navigation
        [InverseProperty("SectionTemplate")]
        public ICollection<JobSectionTemplate> JobsSectionTemplates { get; set; } = new List<JobSectionTemplate>();

        public List<Question> Questions { get; set; } = new List<Question>();
        public Section asSection(Guid appId, Guid sectionId)
        {
            var answers = SafeHelpers.safeList(Questions).Select(q => q.asAnswer(sectionId)).ToList();
            return new Section
            {
                Id = sectionId,
                ApplicationId = appId,
                Title = Title,
                HelpText = HelpText,
                Who = Who,
                CanEditWho = CanEditWho != false,
                RequireComments = RequireComments,
                CommentsMessage = CommentsMessage,
                Weighting = Weighting,
                Comments = "",
                Finished = false,
                Answers = answers
            };
        }
    }

    [ToString, Equals(DoNotAddEqualityOperators = true)]
    public class Question
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid SectionTemplateId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; } = null!;

        [StringLength(255)]
        public string? HelpText { get; set; }

        public bool? ScoreOutOfTen { get; set; } = false;

        public bool? Singleline { get; set; } = false;
        public bool? IsRequired { get; set; } = true;
        public bool? IsNumber { get; set; } = false;
        public bool? IsDate { get; set; } = false;

        public Answer asAnswer(Guid sectionId)
        {
            return new Answer
            {
                SectionId = sectionId,
                Title = Title,
                HelpText = HelpText,
                IsRequired = IsRequired,
                IsNumber = IsNumber,
                IsDate = IsDate,
                ScoreOutOfTen = ScoreOutOfTen,
                Singleline = Singleline,
                AnswerText = ""
            };
        }
    }
}