using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace xingyi.TagHelpers
{
    public static class InputHelper
    {
        public static object getValue(string attributeName, string attributeType, object value)
        {
            if (attributeName=="Id" && attributeType=="hidden" && (value==null||value == "") )
            {
                return Guid.Empty;
            }
            return value;
        }

        public static string GenerateInputHtml(string attributeName, string attributeType, ModelExpression BindTo, object index, object value)
        {
            string thisId = (index is int)?
                $"{BindTo.Name}[{index}].{attributeName}":
                $"{BindTo.Name}['+{index}+'].{attributeName}";

            string parentClass = attributeType == "checkbox" ? "form-check" : "form-group";
            string labelString = attributeType == "hidden" ? "" : $"<label for=\"{thisId}\">{attributeName}</label>";
            string templateStart = $"<div class=\"{parentClass}\">{labelString}";
            string templateBody = string.Empty;

            string valueAttribute = $"value=\"{getValue(attributeName, attributeType, value)}\"";

            switch (attributeType)
            {
                case "text":
                    templateBody = $"<input type=\"text\" name=\"{thisId}\" id=\"{thisId}\" class=\"form-control\" {valueAttribute} />";
                    break;
                case "readonly":
                    templateBody = $"<input type=\"text\" name=\"{thisId}\" id=\"{thisId}\" class=\"form-control\" {valueAttribute} readyonly />";
                    break;
                case "hidden":
                    templateBody = $"<input type=\"hidden\" name=\"{thisId}\" id=\"{thisId}\" {valueAttribute} />";
                    break;
                case "textarea":
                    templateBody = $"<textarea name=\"{thisId}\" id=\"{thisId}\" class=\"form-control\">{value}</textarea>";
                    break;
                case "checkbox":
                    bool isChecked = value != null && value != "" && Convert.ToBoolean(value);
                    string hidden = $"<input type=\"hidden\" name=\"{thisId}\" id=\"{thisId}\" class=\"form-check-input\" {(isChecked ? "checked" : string.Empty)} />";
                    string checkbox = $"<input type=\"checkbox\"  class=\"form-check-input toggle-checkbox\" data-toggle-target=\"{thisId}\" {(isChecked ? "checked" : string.Empty)} />";
                    templateBody = hidden + checkbox;
                    break;
                default:
                    throw new InvalidOperationException($"Cannot handle attribute type {attributeType} for {attributeName}");
            }
            return templateStart + templateBody + "</div>";
        }

    }
}

