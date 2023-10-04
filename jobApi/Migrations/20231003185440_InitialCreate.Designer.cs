﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using xingyi.job.Models;

#nullable disable

namespace jobApi.Migrations
{
    [DbContext(typeof(JobDbContext))]
    [Migration("20231003185440_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("xingyi.job.Models.Job", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

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

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

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

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("SectionTemplates");
                });

            modelBuilder.Entity("xingyi.job.Models.JobSectionTemplate", b =>
                {
                    b.HasOne("xingyi.job.Models.Job", "Job")
                        .WithMany("JobSectionTemplates")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("xingyi.job.Models.SectionTemplate", "SectionTemplate")
                        .WithMany("JobsSectionTemplates")
                        .HasForeignKey("SectionTemplateId")
                        .OnDelete(DeleteBehavior.Restrict)
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

            modelBuilder.Entity("xingyi.job.Models.Job", b =>
                {
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