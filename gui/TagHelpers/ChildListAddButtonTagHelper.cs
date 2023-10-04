using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
namespace gui.TagHelpers
{


    [HtmlTargetElement("child-list-add-button", Attributes = BindToAttributeName)]
    public class ChildListAddButtonTagHelper : TagHelper
    {
        private const string BindToAttributeName = "bind-to";

        [HtmlAttributeName(BindToAttributeName)]
        public ModelExpression BindTo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            var buttonId = $"add.{BindTo.Name.ToLower()}.button";
            output.TagName = "button";
            output.Content.SetContent(output.GetChildContentAsync().Result.GetContent());
            output.Attributes.SetAttribute("type", "button");
            output.Attributes.SetAttribute("id", buttonId);
        }
    }

}
