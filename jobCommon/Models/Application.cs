using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using xingyi.common;
using xingyi.job.Models;
using static System.Collections.Specialized.BitVector32;

namespace xingyi.application
{
    [ToString, Equals(DoNotAddEqualityOperators = true)]
    [DebuggerDisplay("Application: (Id: {Id}, JobId: {JobId}, Candidate: {Candidate})")]
    public class Application
    {
        public void PostGet()

        {
            Sections.Sort((s1, s2) => s1.Title.CompareTo(s2.Title));
        }
        public int calcScore()
        {
            var max = Sections.Sum(s => s.Weighting * 10);
            var value = Sections.Sum(s => s.Score * s.Weighting);
            var percent = 100 * value / max;
            return percent;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid JobId { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Candidate { get; set; }

        public bool Failed { get; set; } = false;
        public bool Suceeded { get; set; } = false;

        public Boolean inProcess()
        {
            return !Failed && !Suceeded;
        }

        public int SumOfWeightings { get; set;}

        [MaxLength(500)]
        public string DetailedComments { get; set; } = "";

        // Navigation Properties
        [ForeignKey("JobId")]
        public Job? Job { get; set; } = null;
        public List<Section> Sections { get; set; } = new List<Section>();
    }

    [ToString, Equals(DoNotAddEqualityOperators = true)]
    [DebuggerDisplay("Section: (Id: {Id}, ApplicationId: {ApplicationId}, Who: {Who})")]

    public class Section
    {
        public void PostGet()
        {
            Answers.Sort((a1, a2) => a1.Title.CompareTo(a2.Title));
        }
        public int calcScore()
        {
            return Answers.Sum(a => a.Score);
        }
        public int displayScore(int full)
        {
            return Ints.safeDiv( Score*MaxScore, Weighting* full);
                
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid ApplicationId { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [StringLength(255)]
        public string? HelpText { get; set; } = "";
        public int Weighting { get; set; }
        public int Score { get; set; }
        public int MaxScore { get; set; }
        public bool CanEditWho { get; set; }
        [MaxLength(100)]
        public string Who { get; set; }

        [MaxLength(300)]
        public string Comments { get; set; }

        public bool Finished { get; set; }

        // Navigation Properties
        [ForeignKey("ApplicationId")]
        [IgnoreDuringToString]
        public Application? Application { get; set; }
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
        public string? HelpText { get; set; }

        public bool? ScoreOutOfTen { get; set; }
        public bool? IsRequired { get; set; } = true;
        public bool? IsNumber { get; set; }
        public bool? Singleline { get; set; }

        [MaxLength(2048)]
        public string AnswerText { get; set; }

        public int Score { get; set; }
    }
}
