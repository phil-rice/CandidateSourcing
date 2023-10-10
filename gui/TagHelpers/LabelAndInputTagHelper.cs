namespace xingyi.TagHelpers
{
    using global::gui.TagHelpers;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Newtonsoft.Json.Linq;
    using xingyi.common;

    [HtmlTargetElement("label-and-input", Attributes = ForAttributeName)]
    public class LabelAndInputTagHelper : AbstractLabelAndTextTagHelper
    {


        public LabelAndInputTagHelper(IHtmlGenerator htmlGenerator) : base(htmlGenerator) { }
        override protected TagBuilder CreateText()
        {
            var input = new TagBuilder("input");
            input.Attributes.Add("id", For.Name);
            input.Attributes.Add("name", For.Name);
            input.Attributes.Add("class", "form-control");
            input.Attributes.Add("type", "text");
            input.Attributes.Add("value", For.ModelExplorer.GetSimpleDisplayText());
            if (IsReadonly)
            {
                input.Attributes.Add("readonly", "readonly");
            }
            return input;

        }

    }
}
