using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using xingyi.common;

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
            output.TagName = "div";
            output.Attributes.Add("class", "form-group");

            var checkboxInput = new TagBuilder("input");
            checkboxInput.Attributes.Add("type", "checkbox");
            checkboxInput.Attributes.Add("class", "form-check-input");
            checkboxInput.Attributes.Add("id", For.Name);
            checkboxInput.Attributes.Add("name", For.Name);

            bool? isChecked = For.Model as bool?;
            if (isChecked.HasValue && isChecked.Value)
            {
                checkboxInput.Attributes.Add("checked", "checked");
            }

            var label = new TagBuilder("label");
            label.Attributes.Add("class", "form-check-label");
            label.Attributes.Add("for", For.Name);

            var labelName = Strings.ExtractAndFormatLabel(For.Name);
            label.InnerHtml.Append(labelName);

            output.Content.AppendHtml(checkboxInput);
            output.Content.AppendHtml(label);
        }
    }
}
