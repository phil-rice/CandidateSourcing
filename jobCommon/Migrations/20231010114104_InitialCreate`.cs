using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jobCommon.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Finished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SectionTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanEditWho = table.Column<bool>(type: "bit", nullable: true),
                    Weighting = table.Column<int>(type: "int", nullable: false),
                    RequireComments = table.Column<bool>(type: "bit", nullable: false),
                    CommentsMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Who = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HelpText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Candidate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Failed = table.Column<bool>(type: "bit", nullable: false),
                    Suceeded = table.Column<bool>(type: "bit", nullable: false),
                    SumOfWeightings = table.Column<int>(type: "int", nullable: false),
                    DetailedComments = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSectionTemplates",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSectionTemplates", x => new { x.JobId, x.SectionTemplateId });
                    table.ForeignKey(
                        name: "FK_JobSectionTemplates_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSectionTemplates_SectionTemplates_SectionTemplateId",
                        column: x => x.SectionTemplateId,
                        principalTable: "SectionTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HelpText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ScoreOutOfTen = table.Column<bool>(type: "bit", nullable: true),
                    Singleline = table.Column<bool>(type: "bit", nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: true),
                    IsNumber = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_SectionTemplates_SectionTemplateId",
                        column: x => x.SectionTemplateId,
                        principalTable: "SectionTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HelpText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Weighting = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    CanEditWho = table.Column<bool>(type: "bit", nullable: false),
                    Who = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    RequireComments = table.Column<bool>(type: "bit", nullable: false),
                    CommentsMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Finished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HelpText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ScoreOutOfTen = table.Column<bool>(type: "bit", nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: true),
                    IsNumber = table.Column<bool>(type: "bit", nullable: true),
                    Singleline = table.Column<bool>(type: "bit", nullable: true),
                    AnswerText = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_SectionId",
                table: "Answers",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_JobId",
                table: "Applications",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_Title",
                table: "Jobs",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobSectionTemplates_SectionTemplateId",
                table: "JobSectionTemplates",
                column: "SectionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SectionTemplateId",
                table: "Questions",
                column: "SectionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ApplicationId",
                table: "Sections",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionTemplates_Title",
                table: "SectionTemplates",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "JobSectionTemplates");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "SectionTemplates");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
