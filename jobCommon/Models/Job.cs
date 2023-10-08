using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using xingyi.application;
using xingyi.common;

namespace xingyi.job.Models
{

    [ToString, Equals(DoNotAddEqualityOperators = true)]
    [DebuggerDisplay("Job: (Id: {Id}, Title: {Title})")]
    public partial class Job
    {
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

        [InverseProperty("Job")]
        public List<Application> Applications { get; set; } = new List<Application>();
        public bool contains(SectionTemplate st)
        {
            return JobSectionTemplates.Any(jst => jst.SectionTemplate.Id == st.Id);
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
    [DebuggerDisplay("SectionTemplate: (Id: {Id}, owner: {owner}, Title: {Title})")]
    public partial class SectionTemplate
    {
        [Key]
        public Guid Id { get; set; }

        public string Owner { get; set; }
        public bool? CanEditWho { get; set; }

        public string Who  { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; } = null!;

        [StringLength(255)]
        public string? Description { get; set; }

        //Navigation
        [InverseProperty("SectionTemplate")]
        public ICollection<JobSectionTemplate> JobsSectionTemplates { get; set; } = new List<JobSectionTemplate>();

        public List<Question> Questions { get; set; } = new List<Question>();
        public Section asSection(Guid appId,Guid sectionId)
        {
            var answers = SafeHelpers.safeList(Questions).Select(q => q.asAnswer(sectionId)).ToList();
            return new Section
            {
                Id = sectionId,
                ApplicationId = appId,
                Title = Title,
                Description = Description,
                Who = Who,
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
        public string? Description { get; set; }

        public bool? ScoreOutOfTen { get; set; }

        public bool? Singleline { get; set; }

        public Answer asAnswer(Guid sectionId)
        {
            return new Answer
            {
                SectionId = sectionId,
                Title = Title,
                Description = Description,
                ScoreOutOfTen = ScoreOutOfTen,
                Singleline = Singleline,
                AnswerText=""
            };
        }
    }
}