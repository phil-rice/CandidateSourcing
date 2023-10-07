using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using xingyi.job.Models;

namespace xingyi.application
{
    [ToString, Equals(DoNotAddEqualityOperators = true)]
    [DebuggerDisplay("Application: (Id: {Id}, JobId: {JobId}, Candidate: {Candidate})")]
    public class Application
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid JobId { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Candidate { get; set; }

        [MaxLength(500)]
        public string DetailedComments { get; set; }

        // Navigation Properties
        public ICollection<Section> Sections { get; set; } = new List<Section>();
    }

    [ToString, Equals(DoNotAddEqualityOperators = true)]
    [DebuggerDisplay("Section: (Id: {Id}, ApplicationId: {ApplicationId}, Who: {Who})")]

    public class Section
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid ApplicationId { get; set; }

        [MaxLength(100)]
        public string Who { get; set; }

        [MaxLength(300)]
        public string Comments { get; set; }

        public bool Finished { get; set; }

        // Navigation Properties
        [ForeignKey("ApplicationId")]
        public Application Application { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }

    [ToString, Equals(DoNotAddEqualityOperators = true)]
    [DebuggerDisplay("Answer: (Id: {Id}, SectionId: {SectionId}, Title: {Title})")]
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid SectionId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = null!;

        [MaxLength(255)]
        public string? Description { get; set; }

        public bool? ScoreOutOfTen { get; set; }
        public bool? Singleline { get; set; }

        [MaxLength(500)]
        public string AnswerText { get; set; }

        public int Score { get; set; }
    }
}
