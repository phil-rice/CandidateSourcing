using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using xingyi.common;

namespace xingyi.TagHelpers
{
    [HtmlTargetElement("label-and-textarea", Attributes = ForAttributeName)]
    public class LabelAndTextAreaTagHelper : TagHelper
    {
        private const string ForAttributeName = "asp-for";

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("asp-label")]
        public ModelExpression Label { get; set; }

        [HtmlAttributeName("readonly")]
        public bool IsReadonly { get; set; } = false;  // Default value is false

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName("show-score")]
        public bool ShowScoreOutOf10 { get; set; } = false;  // Default value is false

        [HtmlAttributeName("score")]
        public ModelExpression? Score { get; set; }


        private readonly IHtmlGenerator _htmlGenerator;
        public LabelAndTextAreaTagHelper(IHtmlGenerator htmlGenerator)
        {
            _htmlGenerator = htmlGenerator;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "form-group");

            var labelName = Label?.Model == null ? Strings.ExtractAndFormatLabel(For.Name) : Strings.ExtractAndFormatLabel(Label.Model.ToString());

            var label = new TagBuilder("label");
            label.Attributes.Add("for", For.Name);
            label.Attributes.Add("class", "control-label");
            label.InnerHtml.Append(labelName);

            var textarea = new TagBuilder("textarea");
            textarea.Attributes.Add("id", For.Name);
            textarea.Attributes.Add("name", For.Name);
            textarea.Attributes.Add("class", "form-control");

            // Get the actual value from the model and set it as the content for the textarea
            var value = For.ModelExplorer.GetSimpleDisplayText();
            textarea.InnerHtml.Append(value);

            if (IsReadonly)
            {
                textarea.Attributes.Add("readonly", "readonly");
            }
            var validationSpan = _htmlGenerator.GenerateValidationMessage(
                        ViewContext,
                        modelExplorer: For.ModelExplorer,
                        expression: For.Name,
                        message: null,
                        tag: "span",
                        htmlAttributes: null);

            var scoreSpan = new TagBuilder("span");
            var scoreSlider = new TagBuilder("input");
            if (ShowScoreOutOf10)
            {
                // Construct the <span> tag for displaying score
                scoreSpan.Attributes.Add("id", "sliderValue-" + Score.Name);
                scoreSpan.InnerHtml.Append(Score.Model.ToString());

                // Construct the <input> tag for the slider
                scoreSlider.Attributes.Add("type", "range");
                scoreSlider.Attributes.Add("min", "0");
                scoreSlider.Attributes.Add("max", "10");
                scoreSlider.Attributes.Add("name", Score.Name);
                scoreSlider.Attributes.Add("value", Score.Model.ToString());
                scoreSlider.Attributes.Add("oninput", $"updateSliderValue(this.value, 'sliderValue-{Score.Name}')");
            }

            output.Content.AppendHtml(label);
            output.Content.AppendHtml(textarea);
            output.Content.AppendHtml(validationSpan);
            if (ShowScoreOutOf10)
            {
                output.Content.AppendHtml(scoreSpan);
                output.Content.AppendHtml(scoreSlider);

            }
        }
    }
}
