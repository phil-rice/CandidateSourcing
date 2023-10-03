using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xingyi.job.Models
{
    [ToString, Equals(DoNotAddEqualityOperators = true)]
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
    public class JobSectionTemplate
    {
        [Required]
        public Guid JobId { get; set; }

        [ForeignKey("JobId")]
        public Job Job { get; set; }

        //Navigation
        [Required]
        public Guid SectionTemplateId { get; set; }

        [ForeignKey("SectionTemplateId")]
        public SectionTemplate SectionTemplate { get; set; }
    }
    [ToString, Equals(DoNotAddEqualityOperators = true)]
    public partial class SectionTemplate
    {
        [Key]
        public Guid Id { get; set; }

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