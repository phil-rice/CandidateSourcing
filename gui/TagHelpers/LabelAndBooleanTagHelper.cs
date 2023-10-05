using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace xingyi.TagHelpers
{
    [HtmlTargetElement("label-and-boolean", Attributes = ForAttributeName)]
    public class LabelAndBooleanTagHelper : TagHelper
    {
        private const string ForAttributeName = "asp-for";

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div"; // Set to div as outermost tag
            output.Attributes.Add("class", "form-group");

            // Build the inner HTML structure
            var innerHtml = new TagBuilder("div");
            innerHtml.AddCssClass("form-check");

            // Checkbox input
            var checkboxInput = new TagBuilder("input");
            checkboxInput.AddCssClass("form-check-input");
            checkboxInput.Attributes.Add("type", "checkbox");
            checkboxInput.Attributes.Add("id", For.Name);
            checkboxInput.Attributes.Add("name", For.Name);
            bool? isChecked = For.Model as bool?;

            if (isChecked.HasValue && isChecked.Value)
            {
                checkboxInput.Attributes.Add("checked", "checked");
            }


            // Label
            var label = new TagBuilder("label");
            label.AddCssClass("form-check-label");
            label.Attributes.Add("for", For.Name);
            label.InnerHtml.Append(For.Metadata.DisplayName ?? For.Name);

            // Append everything to innerHtml
            innerHtml.InnerHtml.AppendHtml(checkboxInput);
            innerHtml.InnerHtml.AppendHtml(label);

            output.Content.AppendHtml(innerHtml);
        }
    }
}
