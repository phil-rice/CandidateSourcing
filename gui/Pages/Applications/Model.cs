using System.ComponentModel.DataAnnotations;

namespace gui.Pages.Applications
{
    public class ApplicationDetails
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public string JobName { get; set; }
        public string Candidate { get; set; }
        public List<SectionDetail> sections { get; set; }
    }
    public class SectionDetail
    {
        public string? Title { get; set; }
        [EmailAddress]
        public string Who { get; set; }
        public bool? CanEditWho { get; set; }

    }

}
