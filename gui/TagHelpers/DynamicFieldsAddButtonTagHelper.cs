using Microsoft.AspNetCore.Razor.TagHelpers;
namespace gui.TagHelpers
{


    [HtmlTargetElement("dynamic-fields-add-button", Attributes = BindToAttributeName)]
    public class DynamicFieldsAddButtonTagHelper : TagHelper
    {
        private const string BindToAttributeName = "bind-to";

        [HtmlAttributeName(BindToAttributeName)]
        public string BindTo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div"; // Set the outermost tag to div
            output.Attributes.SetAttribute("class", "form-group");

            var buttonId = $"add.{BindTo.ToLower()}.button";
            var buttonText = $"Add {BindTo}";

            output.Content.SetHtmlContent($@"
            <button type='button' id='{buttonId}'>{buttonText}</button>
        ");
        }
    }

}
