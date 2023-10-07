using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace xingyi.job.Models
{
    public partial class Job
    {

        public bool contains(SectionTemplate st)
        {
            return JobSectionTemplates.Any(jst => jst.SectionTemplate.Id == st.Id);
        }

    }

    [ToString, Equals(DoNotAddEqualityOperators = true)]
    [DebuggerDisplay("Job: (ID: {ID}, Title: {Title})")]
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

        //Navigation
        [InverseProperty("Job")]
        public virtual ICollection<JobSectionTemplate> JobSectionTemplates { get; set; } = new List<JobSectionTemplate>();
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
        public Job? Job { get; set; }

        [ForeignKey("SectionTemplateId")]
        public SectionTemplate? SectionTemplate { get; set; }
    }
    [ToString, Equals(DoNotAddEqualityOperators = true)]
    [DebuggerDisplay("SectionTemplate: (Id: {Id}, owner: {owner}, Title: {Title})")]
    public partial class SectionTemplate
    {
        [Key]
        public Guid Id { get; set; }

        public string owner;
        public bool? CanEditWho { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; } = null!;

        [StringLength(255)]
        public string? Description { get; set; }

        //Navigation
        [InverseProperty("SectionTemplate")]
        public ICollection<JobSectionTemplate> JobsSectionTemplates { get; set; } = new List<JobSectionTemplate>();

        public ICollection<Question> Questions { get; set; } = new List<Question>();
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
    }
}