using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Reflection.PortableExecutable;
using xingyi.common;

namespace xingyi.TagHelpers
{
    [HtmlTargetElement("label-and-boolean", Attributes = ForAttributeName )]
    public class LabelAndBooleanTagHelper : TagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string LabelAttributeName = "asp-label";


        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("asp-label")]
        public ModelExpression Label { get; set; }

        [HtmlAttributeName("help-text")]
        public ModelExpression HelpText { get; set; }

        [HtmlAttributeName("help-text-static")]
        public string HelpTextStatic { get; set; }



        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "form-group");

            bool? isChecked = For.Model as bool?;

            var hiddenInput = CreateHiddenInput(isChecked);
            var checkboxInput = CreateCheckboxInput(isChecked);
            var label = CreateLabel();

            output.Content.AppendHtml(hiddenInput);
            output.Content.AppendHtml(checkboxInput);
            output.Content.AppendHtml(label);
            if (HelpText?.Model != null || !string.IsNullOrEmpty(HelpTextStatic))
                output.Content.AppendHtml(CreateTooltip());
        }
        protected TagBuilder CreateTooltip()
        {
            var helpText = HelpTextStatic == null ? HelpText.Model.ToString() : HelpTextStatic;
            var span = new TagBuilder("span");
            span.Attributes.Add("class", "help-text");
            span.Attributes.Add("data-toggle", "tooltip");
            span.Attributes.Add("data-placement", "top");
            span.Attributes.Add("title", helpText);
            span.InnerHtml.AppendHtml(" (help)");
            return span;
        }
        private TagBuilder CreateHiddenInput(bool? isChecked)
        {
            var hiddenInput = new TagBuilder("input");
            hiddenInput.Attributes.Add("type", "hidden");
            hiddenInput.Attributes.Add("name", For.Name);
            hiddenInput.Attributes.Add("id", $"{For.Name}_hidden");
            hiddenInput.Attributes.Add("value", isChecked.HasValue && isChecked.Value ? "true" : "false");
            return hiddenInput;
        }

        private TagBuilder CreateCheckboxInput(bool? isChecked)
        {
            var checkboxInput = new TagBuilder("input");
            checkboxInput.Attributes.Add("type", "checkbox");
            checkboxInput.Attributes.Add("class", "form-check-input toggle-checkbox");
            checkboxInput.Attributes.Add("data-toggle-target", $"{For.Name}_hidden");
            if (isChecked.HasValue && isChecked.Value)
            {
                checkboxInput.Attributes.Add("checked", "checked");
            }
            return checkboxInput;
        }

        private TagBuilder CreateLabel()
        {
            var label = new TagBuilder("label");
            label.Attributes.Add("class", "form-check-label");
            label.Attributes.Add("for", For.Name);

            // Use provided Label value, if not use default
            var labelName = Label?.Model == null ? Strings.ExtractAndFormatLabel(For.Name) : Strings.ExtractAndFormatLabel(Label.Model.ToString());
            label.InnerHtml.Append(labelName);

            return label;
        }
    }
}
