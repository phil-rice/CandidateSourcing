using Humanizer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using xingyi.common;
using static System.Net.Mime.MediaTypeNames;

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

            var hiddenInput = new TagBuilder("input");
            hiddenInput.Attributes.Add("type", "hidden");
            hiddenInput.Attributes.Add("name", For.Name);
            hiddenInput.Attributes.Add("id", $"{For.Name}_hidden");

            bool? isChecked = For.Model as bool?;
            if (isChecked.HasValue && isChecked.Value)
            {
                hiddenInput.Attributes.Add("value", "true");
            }
            else
            {
                hiddenInput.Attributes.Add("value", "false");
            }

            var checkboxInput = new TagBuilder("input");
            checkboxInput.Attributes.Add("type", "checkbox");
            checkboxInput.Attributes.Add("class", "form-check-input toggle-checkbox");
            checkboxInput.Attributes.Add("data-toggle-target", $"{For.Name}_hidden");
            if (isChecked.HasValue && isChecked.Value)
            {
                checkboxInput.Attributes.Add("checked", "checked");
            }

            output.Content.AppendHtml(hiddenInput);
            output.Content.AppendHtml(checkboxInput);

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
