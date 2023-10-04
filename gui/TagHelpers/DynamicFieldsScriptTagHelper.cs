using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;

namespace xingyi.TagHelpers
{
    [HtmlTargetElement("dynamic-fields-script")]
    public class DynamicFieldsScriptTagHelper : TagHelper
    {
        private const char ATTRIBUTE_SEPARATOR = ',';
        private const char TYPE_SEPARATOR = ':';



        [HtmlAttributeName("items-definition")]
        public string ItemsDefinition { get; set; }

        [HtmlAttributeName("bind-to")]
        public string BindTo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "script";
            output.TagMode = TagMode.StartTagAndEndTag;

            var definitions = ParseItemsDefinition(ItemsDefinition);
            var generatedTemplates = definitions.Select((definition) =>
            {
                string attributeName = definition.Key;
                string attributeType = definition.Value;

                return GenerateInputElement(attributeName, attributeType);
            });

            string joinedTemplates = string.Join("", generatedTemplates);

            var scriptContent = $"document.addEventListener('DOMContentLoaded', function() {{"
                + $"document.getElementById('add.{BindTo.ToLower()}.button').addEventListener('click', function() {{"
                + $"var element = document.getElementById('{BindTo}-group');"
                + "console.log('element', element);"
                + "var newIndex = (element && element.children) ? element.children.length : 0;"
                + "console.log('newIndex', newIndex);"
                + "var template = document.createElement('div');"
                + $"template.className = '{BindTo}-group';"
                + $"template.innerHTML = '{joinedTemplates}';"
                + $"element.appendChild(template);"
                + "});"
                + "});";

            output.Content.SetHtmlContent(scriptContent);
        }

        private Dictionary<string, string> ParseItemsDefinition(string itemsDefinition)
        {
            var attributes = itemsDefinition.Split(ATTRIBUTE_SEPARATOR)
                                           .Select(attr => attr.Split(TYPE_SEPARATOR))
                                           .ToDictionary(key => key[0], value => value[1]);
            return attributes;
        }

        private string GenerateInputElement(string attributeName, string attributeType)
        {
            string thisId = $"{BindTo}['+ newIndex + '].{attributeName}";


            string templateStart = $"<div class=\"form-group\"><label for=\"{thisId}\">{attributeName}</label>";
            string templateBody = string.Empty;

            switch (attributeType)
            {
                case "text":
                    templateBody = $"<input type=\"text\" name=\"{thisId}\" id=\"{thisId}\" class=\"form-control\" />";
                    break;
                case "textarea":
                    templateBody = $"<textarea name=\"{thisId}\" id=\"{thisId}\" class=\"form-control\"></textarea>";
                    break;
                case "checkbox":
                    templateBody = $"<input type=\"checkbox\" name=\"{thisId}\" id=\"{thisId}\" class=\"form-control\" />";
                    break;
                default:
                    throw new InvalidOperationException($"Cannot handle attribute type {attributeType} for {attributeName}");
            }

            return templateStart + templateBody + "</div>";
        }



    }
}
