using xingyi.common;
using xingyi.job.Models;
using xingyi.job.Repository;

namespace gui.Controllers
{
    public static class TestDataMaker
    {

        public static SectionTemplate candidateSt(string owner)
        {
            Guid testGuid = Guids.from("Candidate for " + owner);
            SectionTemplate testJob = new SectionTemplate
            {
                Id = testGuid,
                Title = "CandidateDetails",
                Owner = owner,
                Who = "The Candidate",
                CanEditWho = false,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Name",
                        Singleline= true
                    },
                    new Question
                    {
                        Title = "Passport issuing country",
                        Singleline= true
                    },
                    new Question
                    {
                        Title = "Passport number",
                        IsNumber= true,
                        Singleline= true
                    },
                    new Question
                    {
                        Title = "SAP code (if exists, otherwise leave blank)",
                        Singleline= true,
                        IsRequired = false
                    }
                }


            };
            return testJob;

        }
        public static SectionTemplate interview1(string owner)
        {
            Guid testGuid = Guids.from("interview 1 for " + owner);
            SectionTemplate result = new SectionTemplate
            {
                Id = testGuid,
                Title = "Dotnet full stack - Interview 1",
                Owner = owner,
                CanEditWho = true,
                Who = "Please Specify",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Verbal Communication skills",
                        ScoreOutOfTen = true

                    },
                    new Question
                    {
                        Title = "Understanding of the basics of C#",
                        ScoreOutOfTen = true
                    },
                    new Question
                    {
                        Title = "Understanding of the basics of Javascript",
                        ScoreOutOfTen = true
                    },
                    new Question
                    {
                        Title = "Understanding of the basics of MVC",
                        ScoreOutOfTen = true
                    },
                    new Question
                    {
                        Title = "Can explain the difference between Code first and Database first",
                        ScoreOutOfTen = true
                    }
                }


            };
            return result;

        }

        public static SectionTemplate interview2(string owner)
        {
            Guid testGuid = Guids.from("interview 2 for " + owner);
            SectionTemplate result = new SectionTemplate
            {
                Id = testGuid,
                Title = "Dotnet full stack - Interview 2",
                Owner = owner,
                Who = "Please Specify",
                CanEditWho = true,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Verbal Communication skills",
                        ScoreOutOfTen = true

                    },
                    new Question
                    {
                        Title = "Can explain the benefits of stateless servers",
                        ScoreOutOfTen = true
                    },
                    new Question
                    {
                        Title = "Understands the basics of deployment",
                        ScoreOutOfTen = true
                    },
                    new Question
                    {
                        Title = "Have they any experience with Amazon, Azure or Google cloud",
                        ScoreOutOfTen = true
                    }
                }
            };
            return result;
        }
        public static SectionTemplate hrInterview(string owner)
        {
            Guid testGuid = Guids.from("HR interview for " + owner);
            SectionTemplate result = new SectionTemplate
            {
                Id = testGuid,
                Title = "HR interview",
                Owner = owner,
                Who = "Please Specify",
                CanEditWho = true,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Verbal Communication skills",
                        ScoreOutOfTen = true

                    },
                    new Question
                    {
                        Title = "Is the candidate willing to relocate",

                    },
                    new Question
                    {
                        Title = "What are the candidates expectations"
                    },
                    new Question
                    {
                        Title = "Would you be happy with the Candidate representing HCL on customer premises",
                        ScoreOutOfTen = true
                    }
                }
            };
            return result;
        }

        public static Job testJob(string owner, List<SectionTemplate> sts)
        {
            Guid testGuid = Guids.from("testJobGuid for " + owner);
            Job testJob = new Job
            {
                Id = testGuid,
                Title = "An example Vacancy",
                Owner = "testOwner",


            };
            testJob.JobSectionTemplates = sts.Select(st =>
            new JobSectionTemplate
            {
                JobId = testJob.Id,
                Job = testJob,
                SectionTemplateId = st.Id,
                SectionTemplate = st,
            }).ToList();
            return testJob;
        }

    }
}
