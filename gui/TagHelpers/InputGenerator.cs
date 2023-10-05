﻿using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace xingyi.TagHelpers
{
    public static class InputGenerator
    {
        public static string GenerateInputHtml(string attributeName, string attributeType, ModelExpression BindTo, object index, object value)
        {
            string thisId = (index is int)?
                $"{BindTo.Name}[{index}].{attributeName}":
                $"{BindTo.Name}['+{index}+'].{attributeName}";

            string parentClass = attributeType == "checkbox" ? "form-check" : "form-group";
            string templateStart = $"<div class=\"{parentClass}\"><label for=\"{value}\">{attributeName}</label>";
            string templateBody = string.Empty;

            string valueAttribute = $"value=\"{value}\"";

            switch (attributeType)
            {
                case "text":
                    templateBody = $"<input type=\"text\" name=\"{thisId}\" id=\"{thisId}\" class=\"form-control\" {valueAttribute} />";
                    break;
                case "hidden":
                    templateBody = $"<input type=\"hidden\" name=\"{thisId}\" id=\"{thisId}\" {valueAttribute} />";
                    break;
                case "textarea":
                    templateBody = $"<textarea name=\"{thisId}\" id=\"{thisId}\" class=\"form-control\">{value}</textarea>";
                    break;
                case "checkbox":
                    bool isChecked = value != null && value != "" && Convert.ToBoolean(value);
                    templateBody = $"<input type=\"checkbox\" name=\"{thisId}\" id=\"{thisId}\" class=\"form-check-input\" {(isChecked ? "checked" : string.Empty)} />";
                    break;
                default:
                    throw new InvalidOperationException($"Cannot handle attribute type {attributeType} for {attributeName}");
            }
            return templateStart + templateBody + "</div>";
        }

    }
}
