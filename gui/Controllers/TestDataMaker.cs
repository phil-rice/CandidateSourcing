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
                HelpText = "Information the Candidate fills in about themselves",
                CommentsMessage = "Are there any comments you would like to make (Not Required)?",
                RequireComments = false,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Name",
                        HelpText = "Please enter your name as it appears on your passport",
                        IsRequired = true,
                        Singleline= true
                    },
                      new Question
                    {
                        Title = "DateOfBirth",
                        IsRequired = true,
                        IsDate= true
                    },
                    new Question
                    {
                        Title = "Passport issuing country",
                        IsRequired = true,
                        Singleline= true
                    },
                    new Question
                    {
                        Title = "Passport number",
                        IsNumber= true,
                        IsRequired = true,
                        Singleline= true
                    },
                    new Question
                    {
                        Title = "SAP code",
                        HelpText = "If it exists otherwise leave blank",
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
                Title = "Interview 1",
                Owner = owner,
                CanEditWho = true,
                Who = "alyson.rice@googlemail.com",
                Weighting = 40,
                RequireComments = true,
                HelpText = "Interview 1 is a technical interview with a senior developer",
                CommentsMessage = "Please summarise how the Candidate performed in this interview",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Verbal Communication skills",
                        ScoreOutOfTen = true,
                        IsRequired = false

                    },
                    new Question
                    {
                        Title = "Understanding of the basics of C#",
                        ScoreOutOfTen = true,
                        IsRequired = false

                    },
                    new Question
                    {
                        Title = "Understanding of the basics of Javascript",
                        ScoreOutOfTen = true,
                        IsRequired = false
                    },
                    new Question
                    {
                        Title = "Understanding of the basics of MVC",
                        ScoreOutOfTen = true,
                        IsRequired = false

                    },
                    new Question
                    {
                        Title = "Can explain the difference between Code first and Database first",
                        ScoreOutOfTen = true,
                        IsRequired = false
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
                Title = "Interview 2",
                Owner = owner,
                Who = "stave.escura@gmail.com",
                CanEditWho = true,
                CommentsMessage = "Please summarise how the Candidate performed in this interview",
                RequireComments = true,
                Weighting = 50,
                HelpText = "Interview 2 is a technical interview with a senior developer",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Verbal Communication skills",
                        ScoreOutOfTen = true,
                        IsRequired = false

                    },
                    new Question
                    {
                        Title = "Can explain the benefits of stateless servers",
                        ScoreOutOfTen = true,
                        IsRequired = false
                    },
                    new Question
                    {
                        Title = "Understands the basics of deployment",
                        ScoreOutOfTen = true,
                        IsRequired = false
                    },
                    new Question
                    {
                        Title = "Have they any experience with Amazon, Azure or Google cloud",
                        ScoreOutOfTen = true,
                        IsRequired = false
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
                Who = "phil.rice@validoc.org",
                CanEditWho = true,
                Weighting = 10,
                CommentsMessage = "Please summarise how the Candidate performed in this interview",
                RequireComments = true,
                HelpText = "HR interview is a non technical interview with the 'Human Resources' department",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Verbal Communication skills",
                        ScoreOutOfTen = true,
                        IsRequired = false

                    },
                    new Question
                    {
                        Title = "Is the candidate willing to relocate",
                        IsRequired = false

                    },
                    new Question
                    {
                        Title = "What are the candidates expectations",
                        IsRequired = false
                    },
                    new Question
                    {
                        Title = "Would you be happy with the Candidate representing HCL on customer premises",
                        ScoreOutOfTen = true,
                        IsRequired = false
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
                Owner = owner,


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
