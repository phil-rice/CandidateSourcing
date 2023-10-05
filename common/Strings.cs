using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace xingyi.common
{
    public static class Strings

    {
        public static string ExtractAndFormatLabel(string fullName)
        {
            // Extract the last part of the property name
            string propName = fullName.Split('.').Last();

            // Uppercase the first character
            propName = char.ToUpper(propName[0]) + propName.Substring(1);

            // Convert camel case into words separated by spaces
            propName = Regex.Replace(propName, "(\\B[A-Z])", " $1");

            return propName;
        }
    }
}
