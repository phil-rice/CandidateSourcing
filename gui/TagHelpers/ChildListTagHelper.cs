using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Reflection;
using xingyi.common;

namespace xingyi.TagHelpers
{

    [HtmlTargetElement("child-list", Attributes = "bind-to,attributes")]
    public class ChildListTagHelper : TagHelper
    {

        [HtmlAttributeName("bind-to")]
        public ModelExpression Items { get; set; }

        [HtmlAttributeName("attributes")]
        public string Attributes { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";  // This will be the outermost div
            output.Attributes.SetAttribute("id", Items.Name + "-group");
            var attributeList = AttributeParser.ParseAttributes(Attributes);

            var items = SafeHelpers.safeEnumerable((Items.Model as IEnumerable<object>));
            if (items.Any())
            {
                   var index = 0;
                foreach (var item in items)
                {

                    output.Content.AppendHtml($"<div class='card'><div class='card-body'><div id='{Items.Name}' class='section-item'>");
                    Type itemType = item.GetType();

                    foreach (var attribute in attributeList)
                    {
                        PropertyInfo prop = itemType.GetProperty(attribute.Key);
                        if (prop != null)
                        {
                            var value = prop.GetValue(item)?.ToString() ?? string.Empty;
                            string inputHtml = InputGenerator.GenerateInputHtml(attribute.Key, attribute.Value, Items, index, value);
                            output.Content.AppendHtml(inputHtml);
                        }
                    }
                    output.Content.AppendHtml("</div></div></div>"); 
                    index += 1;
                }


            }
        }


    }

}