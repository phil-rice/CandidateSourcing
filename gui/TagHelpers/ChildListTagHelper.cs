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
            output.TagName = "div";
            output.Attributes.SetAttribute("id", Items.Name + "-group");
            var attributeList = AttributeParser.ParseAttributes(Attributes);

            bool itemsExist = false; // Flag to check if items exist
            

            foreach (var item in SafeHelpers.safeEnumerable((Items.Model as IEnumerable<object>)))
            {
                itemsExist = true; // Set the flag to true when items exist
                output.Content.AppendHtml($"<div id='{Items.Name}' class='section-item'>");
                Type itemType = item.GetType();

                foreach (var attribute in attributeList)
                {
                    PropertyInfo prop = itemType.GetProperty(attribute.Key);
                    if (prop != null)
                    {
                        var value = prop.GetValue(item)?.ToString() ?? string.Empty;
                        string inputHtml = InputGenerator.GenerateInputHtml(attribute.Key, attribute.Value, value);
                        output.Content.AppendHtml(inputHtml);
                    }
                }
                output.Content.AppendHtml("</div>"); // Close each item's div
            }
            output.Content.AppendHtml("<!-- closing -->"); // Close each item's div


        }

    }
    public static class InputGenerator
    {
        public static string GenerateInputHtml(string attributeKey, string attributeValue, object value)
        {
            switch (attributeValue.ToLower())
            {
                case "text":
                    return $"<label>{attributeKey}</label><input type='text' name='{attributeKey}' value='{value}' />";

                case "textarea":
                    return $"<label>{attributeKey}</label><textarea name='{attributeKey}'>{value}</textarea>";

                case "checkbox":
                    bool isChecked = Convert.ToBoolean(value);
                    return $"<label>{attributeKey}</label><input type='checkbox' name='{attributeKey}' {(isChecked ? "checked" : string.Empty)} />";

                // Additional types as needed...

                default:
                    return string.Empty;
            }
        }
    }
}