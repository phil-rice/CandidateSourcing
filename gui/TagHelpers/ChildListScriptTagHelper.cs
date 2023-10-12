using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;
using xingyi.common;

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

            var definitions = AttributeParser.ParseAttributes(ItemsDefinition);
            var generatedTemplates = definitions.Select((definition) =>
            {
                string attributeName = definition.Key;
                var (attributeType, helpText) = definition.Value;
                return InputHelper.GenerateInputHtml(attributeName, attributeType,helpText,BindTo, "newIndex","");
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
                +  @"$('[data-toggle=""tooltip""]').tooltip();"
                + "});"
                + "});";

            output.Content.SetHtmlContent(scriptContent);
        }

       
     


    }
}
