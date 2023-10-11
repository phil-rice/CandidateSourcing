using gui.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using xingyi.common;

namespace xingyi.TagHelpers
{
    [HtmlTargetElement("label-and-date", Attributes = ForAttributeName)]
    public class LabelAndDateTagHelper : AbstractLabelAndTextTagHelper
    {

        public LabelAndDateTagHelper(IHtmlGenerator htmlGenerator) : base(htmlGenerator)
        {
        }
        override protected TagBuilder CreateText()
        {
            var input = new TagBuilder("input");
            input.Attributes.Add("id", For.Name);
            input.Attributes.Add("name", For.Name);
            input.Attributes.Add("class", "form-control");
            input.Attributes.Add("type", "date");
            input.Attributes.Add("value", For.ModelExplorer.GetSimpleDisplayText());
            if (IsReadonly)
            {
                input.Attributes.Add("readonly", "readonly");
            }
            return input;
        }
    }
}
