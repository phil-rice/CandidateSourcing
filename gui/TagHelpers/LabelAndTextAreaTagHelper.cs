using gui.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using xingyi.common;

namespace xingyi.TagHelpers
{
    [HtmlTargetElement("label-and-textarea", Attributes = ForAttributeName)]
    public class LabelAndTextAreaTagHelper : AbstractLabelAndTextTagHelper
    {

        public LabelAndTextAreaTagHelper(IHtmlGenerator htmlGenerator) : base(htmlGenerator)
        {
        }
        override protected TagBuilder CreateText()
        {
            var textarea = new TagBuilder("textarea");
            textarea.Attributes.Add("id", For.Name);
            textarea.Attributes.Add("name", For.Name);
            textarea.Attributes.Add("class", "form-control");
            var value = For.ModelExplorer.GetSimpleDisplayText();
            textarea.InnerHtml.Append(value);

            if (IsReadonly)
            {
                textarea.Attributes.Add("readonly", "readonly");
            }

            return textarea;
        }
    }
}
