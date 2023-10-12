using System;
using System.Collections.Generic;
using System.Linq;

namespace xingyi.common
{
    public static class AttributeParser
    {

        public static Dictionary<string, (string, string)> ParseAttributes(string attributes)
        {
            if (string.IsNullOrWhiteSpace(attributes))
            {
                throw new ArgumentException($"Invalid attribute input: '{attributes}'. The input is null or whitespace.");
            }

            var pairs = attributes.Split(',')
                .Select(a => a.Trim().Split(new char[] { ':' }, 2)) // Split only on the first ':' to handle : in help text
                .ToArray();

            foreach (var pair in pairs)
            {
                if (pair.Length != 2)
                {
                    throw new ArgumentException($"Invalid attribute format in input: '{attributes}'. Each attribute should be of the form 'attribute:value' or 'attribute:value?helptext'.");
                }

            }

            return pairs.ToDictionary(
                k => k[0],
                v => {
                    var valueParts = v[1].Split(new char[] { '?' }, 2);

                    if (string.IsNullOrEmpty(v[0]) || string.IsNullOrEmpty(valueParts[0]))
                    {
                        throw new ArgumentException($"Invalid attribute format in input: '{attributes}'. Neither attribute name nor its value can be empty.");
                    }
                    return (valueParts[0], valueParts.Length > 1 ? valueParts[1] : "");
                });
        }



    }
}
