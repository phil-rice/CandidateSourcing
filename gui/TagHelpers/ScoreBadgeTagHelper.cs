using Microsoft.AspNetCore.Razor.TagHelpers;
namespace gui.TagHelpers
{

    [HtmlTargetElement("score-badge")]
    public class ScoreBadgeTagHelper : TagHelper
    {
        public int Score { get; set; }
        public bool Finished { get; set; }
        public bool ShowText { get; set; } = true;
        public int? Weighting { get; set; }
        private bool weightIsZero => Weighting.HasValue && Weighting.Value == 0;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span"; // Set the tag name to <span>
            output.Attributes.Add("class", GetBadgeClass()); // Set the class attribute
            var text = ShowText ? GetBadgeText() : "";
            var score = weightIsZero ? "" : Score.ToString();
            output.Content.SetContent($"{text}{score}"); // Set the content inside the tag
        }

        private string GetBadgeClass()
        {
            if (Finished)
            {
                if (weightIsZero)
                    return "badge bg-primary";
                if (Score < 4)
                    return "badge bg-danger";
                if (Score < 6)
                    return "badge bg-secondary";
                if (Score < 8)
                    return "badge bg-primary";
                return "badge bg-success";
            }
            return "badge bg-warning";
        }

        private string GetBadgeText()
        {

            if (Finished)
            {
                if (weightIsZero)
                    return "Filled in";
                if (Score < 4)
                    return $"Reject ";
                if (Score < 6)
                    return $"Proficient ";
                if (Score < 8)
                    return $"Proficient ";
                return $"Expert ";
            }
            return $"Waiting ";
        }
    }
}



