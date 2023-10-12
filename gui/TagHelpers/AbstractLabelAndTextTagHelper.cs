using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Reflection.PortableExecutable;
using xingyi.common;

namespace gui.TagHelpers
{
    abstract public class AbstractLabelAndTextTagHelper : TagHelper
    {
        protected const string ForAttributeName = "asp-for";

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
        public ModelExpression Score { get; set; }
        [HtmlAttributeName("help-text")]
        public ModelExpression HelpText { get; set; }

        [HtmlAttributeName("message")]
        public ModelExpression? Message { get; set; }
        [HtmlAttributeName("messageText")]
        public string MessageText { get; set; } = "";


        private readonly IHtmlGenerator _htmlGenerator;
        public AbstractLabelAndTextTagHelper(IHtmlGenerator htmlGenerator)
        {
            _htmlGenerator = htmlGenerator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "card");

            output.Content.AppendHtml(CreateCardHeader());
            output.Content.AppendHtml(CreateCardBody());
        }

        private TagBuilder CreateCardHeader()
        {
            var cardHeader = new TagBuilder("div");
            cardHeader.AddCssClass("card-header");

            cardHeader.InnerHtml.AppendHtml(CreateLabel());
            if (HelpText?.Model != null)
                cardHeader.InnerHtml.AppendHtml(CreateTooltip());


            return cardHeader;
        }
        protected TagBuilder CreateTooltip()
        {
            var span = new TagBuilder("span");
            span.Attributes.Add("class", "help-text");
            span.Attributes.Add("data-toggle", "tooltip");
            span.Attributes.Add("data-placement", "top");
            span.Attributes.Add("title", HelpText.Model.ToString());
            span.InnerHtml.AppendHtml(" ?");
            return span;
        }

        private TagBuilder CreateCardBody()
        {
            var cardBody = new TagBuilder("div");
            cardBody.AddCssClass("card-body score-card");

            var messageValue = MessageText.Length > 0 ? MessageText : Message?.Model?.ToString();
            if (!string.IsNullOrEmpty(messageValue))
                cardBody.InnerHtml.AppendHtml(CreateMessageSpan(messageValue));

            if (ShowScoreOutOf10)
            {
                cardBody.InnerHtml.AppendHtml(CreateScoreSpan());
                cardBody.InnerHtml.AppendHtml(CreateScoreSlider());
            }

            cardBody.InnerHtml.AppendHtml(CreateText());
            cardBody.InnerHtml.AppendHtml(CreateValidationSpan());

            return cardBody;
        }

        private TagBuilder CreateLabel()
        {
            var labelName = Label?.Model == null ? Strings.ExtractAndFormatLabel(For.Name) : Strings.ExtractAndFormatLabel(Label.Model.ToString());
            var label = new TagBuilder("label");
            label.Attributes.Add("for", For.Name);
            label.Attributes.Add("class", "control-label");
            label.InnerHtml.Append(labelName);

            return label;
        }

        private TagBuilder CreateMessageSpan(string messageValue)
        {
            var messageSpan = new TagBuilder("div");
            messageSpan.InnerHtml.Append(messageValue);

            return messageSpan;
        }

        abstract protected TagBuilder CreateText();


        private TagBuilder CreateValidationSpan()
        {
            var validationAttributes = new Dictionary<string, object> { { "class", "text-danger" } };
            return _htmlGenerator.GenerateValidationMessage(
                        ViewContext,
                        modelExplorer: For.ModelExplorer,
                        expression: For.Name,
                        message: null,
                        tag: "div",
                        htmlAttributes: validationAttributes);
        }

        private TagBuilder CreateScoreSpan()
        {
            var scoreSpan = new TagBuilder("span");
            scoreSpan.Attributes.Add("class", "score");
            scoreSpan.Attributes.Add("id", "sliderValue-" + Score.Name);
            scoreSpan.InnerHtml.Append(Score.Model.ToString());

            return scoreSpan;
        }

        private TagBuilder CreateScoreSlider()
        {
            var scoreSlider = new TagBuilder("input");
            scoreSlider.Attributes.Add("type", "range");
            scoreSlider.Attributes.Add("min", "0");
            scoreSlider.Attributes.Add("max", "10");
            scoreSlider.Attributes.Add("name", Score.Name);
            scoreSlider.Attributes.Add("value", Score.Model.ToString());
            scoreSlider.Attributes.Add("oninput", $"updateSliderValue(this.value, 'sliderValue-{Score.Name}')");

            return scoreSlider;
        }

    }
}
