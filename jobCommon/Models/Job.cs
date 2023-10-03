using System;
using System.Collections.Generic;

namespace xingyi.job.Models
{
    [ToString, Equals(DoNotAddEqualityOperators = true)]
    public partial class Job
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string Owner { get; set; } = null!;

        //Navigation

        public virtual ICollection<JobSectionTemplate> JobSectionTemplates { get; set; } = new List<JobSectionTemplate>();
    }
    [ToString, Equals(DoNotAddEqualityOperators = true)]
    public class JobSectionTemplate
    {
        public Guid JobId { get; set; }
        public Job Job { get; set; }

        //Navigation
        public Guid SectionTemplateId { get; set; }
        public SectionTemplate SectionTemplate { get; set; }
    }
    [ToString, Equals(DoNotAddEqualityOperators = true)]
    public partial class SectionTemplate
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public bool? CanEditWho { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public virtual Job Job { get; set; } = null!;

        //Navigation
        public ICollection<JobSectionTemplate> JobsSectionTemplates { get; set; } = new List<JobSectionTemplate>();
        public ICollection<Question> Questions { get; set; } = new List<Question>();

    }
    [ToString, Equals(DoNotAddEqualityOperators = true)]
    public class Question
    {
        public Guid Id { get; set; }
        public Guid SectionTemplateId { get; set; }

        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool? ScoreOutOfTen { get; set; }
        public bool? Singleline { get; set; }

    }
}