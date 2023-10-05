using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;

namespace xingyi.TagHelpers
{
    [HtmlTargetElement("child-list-script")]
    public class ChildListScriptTagHelper : TagHelper
    {
        private const char ATTRIBUTE_SEPARATOR = ',';
        private const char TYPE_SEPARATOR = ':';



        [HtmlAttributeName("items-definition")]
        public string ItemsDefinition { get; set; }

        [HtmlAttributeName("bind-to")]
        public ModelExpression BindTo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "script";
            output.TagMode = TagMode.StartTagAndEndTag;

            var definitions = ParseItemsDefinition(ItemsDefinition);
            var generatedTemplates = definitions.Select((definition) =>
            {
                string attributeName = definition.Key;
                string attributeType = definition.Value;
                return InputGenerator.GenerateInputHtml(attributeName, attributeType,BindTo, "newIndex","");
            });

            string joinedTemplates = string.Join("", generatedTemplates);
            string inCard = $"<div class=\"card-body\">{joinedTemplates}</div>";

            string boundName = BindTo.Name;

            var scriptContent = $"document.addEventListener('DOMContentLoaded', function() {{"
                + $"document.getElementById('add.{boundName.ToLower()}.button').addEventListener('click', function() {{"
                + $"var element = document.getElementById('{boundName}-group');"
                + "console.log('element', element);"
                + "var newIndex = (element && element.children) ? element.children.length : 0;"
                + "console.log('newIndex', newIndex);"
                + "var template = document.createElement('card');"
                + $"template.className = 'card';"
                + $"template.innerHTML = '{inCard}';"
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

     


    }
}
