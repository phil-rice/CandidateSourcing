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

            var buttonId = $"add.{BindTo.ToLower()}.button";
            output.TagName = "button";
            output.Content.SetContent(output.GetChildContentAsync().Result.GetContent());
            output.Attributes.SetAttribute("type", "button");
            output.Attributes.SetAttribute("id", buttonId);
        }
    }

}
