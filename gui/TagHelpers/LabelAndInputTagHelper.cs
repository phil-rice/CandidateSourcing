namespace xingyi.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using xingyi.common;

    [HtmlTargetElement("label-and-input", Attributes = ForAttributeName)]
    public class LabelAndInputTagHelper : TagHelper
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
        public int Score { get; set; } = 0;

        private readonly IHtmlGenerator _htmlGenerator;
        public LabelAndInputTagHelper(IHtmlGenerator htmlGenerator)
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

            var input = new TagBuilder("input");
            input.Attributes.Add("id", For.Name);
            input.Attributes.Add("name", For.Name);
            input.Attributes.Add("class", "form-control");
            input.Attributes.Add("type", "text");

            if (IsReadonly)
            {
                input.Attributes.Add("readonly", "readonly");
            }

            // Get the actual value from the model and set it as the value for the input
            var value = For.ModelExplorer.GetSimpleDisplayText();
            input.Attributes.Add("value", value);

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
                scoreSpan.Attributes.Add("id", "sliderValue-" + For.Name);
                scoreSpan.InnerHtml.Append(Score.ToString());

                // Construct the <input> tag for the slider
                scoreSlider.Attributes.Add("type", "range");
                scoreSlider.Attributes.Add("min", "1");
                scoreSlider.Attributes.Add("max", "10");
                scoreSlider.Attributes.Add("name", For.Name + ".score");
                scoreSlider.Attributes.Add("value", Score.ToString());
                scoreSlider.Attributes.Add("oninput", $"updateSliderValue(this.value, 'sliderValue-{For.Name}')");
            }



            output.Content.AppendHtml(label);
            output.Content.AppendHtml(input);
            output.Content.AppendHtml(validationSpan);
            if (ShowScoreOutOf10)
            {
                output.Content.AppendHtml(scoreSpan);
                output.Content.AppendHtml(scoreSlider);

            }

        }
    }
}
