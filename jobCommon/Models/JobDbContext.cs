using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace xingyi.job.Models
{
    public class JobDbContext : DbContext
    {
        public JobDbContext(DbContextOptions<JobDbContext> options) : base(options) { }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobSectionTemplate> JobSectionTemplates { get; set; }
        public DbSet<SectionTemplate> SectionTemplates { get; set; }
        public DbSet<Question> Questions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the many-to-many relationship between Job and SectionTemplate
            modelBuilder.Entity<JobSectionTemplate>()
                .HasKey(jst => new { jst.JobId, jst.SectionTemplateId });

            modelBuilder.Entity<JobSectionTemplate>()
                .HasOne(jst => jst.Job)
                .WithMany(job => job.JobSectionTemplates)
                .HasForeignKey(jst => jst.JobId)
                .OnDelete(DeleteBehavior.Restrict); // Restricting cascading delete

            modelBuilder.Entity<JobSectionTemplate>()
                .HasOne(jst => jst.SectionTemplate)
                .WithMany(st => st.JobsSectionTemplates)
                .HasForeignKey(jst => jst.SectionTemplateId)
                .OnDelete(DeleteBehavior.Restrict); // Restricting cascading delete

            // Configuring the one-to-many relationship between SectionTemplate and Question with cascading delete
            modelBuilder.Entity<Question>()
                .HasOne<SectionTemplate>()
                .WithMany(st => st.Questions)
                .HasForeignKey(q => q.SectionTemplateId)
                .OnDelete(DeleteBehavior.Cascade); // Setting up cascading delete for this relationship
        }

    }
}
