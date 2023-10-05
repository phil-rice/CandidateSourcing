namespace xingyi.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement("label-and-input", Attributes = ForAttributeName)]
    public class LabelAndInputTagHelper : TagHelper
    {
        private const string ForAttributeName = "asp-for";

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("readonly")]
        public bool IsReadonly { get; set; } = false;  // Default value is false

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", For.Name);
            label.Attributes.Add("class", "control-label");
            label.InnerHtml.Append(For.Metadata.DisplayName ?? For.Metadata.Name);

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

            var validationSpan = new TagBuilder("span");
            validationSpan.Attributes.Add("class", "text-danger");
            validationSpan.Attributes.Add("data-valmsg-for", For.Name);

            output.Content.AppendHtml(label);
            output.Content.AppendHtml(input);
            output.Content.AppendHtml(validationSpan);
        }
    }
}
