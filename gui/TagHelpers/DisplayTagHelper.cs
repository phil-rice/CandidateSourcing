using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace xingyi.TagHelpers
{
    [HtmlTargetElement("display")]
    public class DisplayTagHelper : TagHelper
    {
        private readonly IHtmlGenerator _generator;

        public DisplayTagHelper(IHtmlGenerator generator)
        {
            _generator = generator;
        }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "p"; // This sets the wrapping tag to <pre>

            if (For?.Model != null)
            {
                output.Content.SetContent(For.Model.ToString());
            }
        }
    }
}
