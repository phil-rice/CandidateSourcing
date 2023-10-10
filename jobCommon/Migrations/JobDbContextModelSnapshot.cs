﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using xingyi.job;

#nullable disable

namespace jobCommon.Migrations
{
    [DbContext(typeof(JobDbContext))]
    partial class JobDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("xingyi.application.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("HelpText")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("IsNumber")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<bool?>("ScoreOutOfTen")
                        .HasColumnType("bit");

                    b.Property<Guid>("SectionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Singleline")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("xingyi.application.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Candidate")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DetailedComments")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("Failed")
                        .HasColumnType("bit");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Suceeded")
                        .HasColumnType("bit");

                    b.Property<int>("SumOfWeightings")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("xingyi.application.Section", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("CanEditWho")
                        .HasColumnType("bit");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<bool>("Finished")
                        .HasColumnType("bit");

                    b.Property<string>("HelpText")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("MaxScore")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Weighting")
                        .HasColumnType("int");

                    b.Property<string>("Who")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("xingyi.job.Models.Job", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Finished")
                        .HasColumnType("bit");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("xingyi.job.Models.JobSectionTemplate", b =>
                {
                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SectionTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("JobId", "SectionTemplateId");

                    b.HasIndex("SectionTemplateId");

                    b.ToTable("JobSectionTemplates");
                });

            modelBuilder.Entity("xingyi.job.Models.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HelpText")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("IsNumber")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<bool?>("ScoreOutOfTen")
                        .HasColumnType("bit");

                    b.Property<Guid>("SectionTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Singleline")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("SectionTemplateId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("xingyi.job.Models.SectionTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("CanEditWho")
                        .HasColumnType("bit");

                    b.Property<string>("HelpText")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Weighting")
                        .HasColumnType("int");

                    b.Property<string>("Who")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("SectionTemplates");
                });

            modelBuilder.Entity("xingyi.application.Answer", b =>
                {
                    b.HasOne("xingyi.application.Section", null)
                        .WithMany("Answers")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("xingyi.application.Application", b =>
                {
                    b.HasOne("xingyi.job.Models.Job", "Job")
                        .WithMany("Applications")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");
                });

            modelBuilder.Entity("xingyi.application.Section", b =>
                {
                    b.HasOne("xingyi.application.Application", "Application")
                        .WithMany("Sections")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");
                });

            modelBuilder.Entity("xingyi.job.Models.JobSectionTemplate", b =>
                {
                    b.HasOne("xingyi.job.Models.Job", "Job")
                        .WithMany("JobSectionTemplates")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("xingyi.job.Models.SectionTemplate", "SectionTemplate")
                        .WithMany("JobsSectionTemplates")
                        .HasForeignKey("SectionTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("SectionTemplate");
                });

            modelBuilder.Entity("xingyi.job.Models.Question", b =>
                {
                    b.HasOne("xingyi.job.Models.SectionTemplate", null)
                        .WithMany("Questions")
                        .HasForeignKey("SectionTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("xingyi.application.Application", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("xingyi.application.Section", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("xingyi.job.Models.Job", b =>
                {
                    b.Navigation("Applications");

                    b.Navigation("JobSectionTemplates");
                });

            modelBuilder.Entity("xingyi.job.Models.SectionTemplate", b =>
                {
                    b.Navigation("JobsSectionTemplates");

                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
