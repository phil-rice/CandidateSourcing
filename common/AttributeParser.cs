using System;
using System.Collections.Generic;
using System.Linq;

namespace xingyi.common
{
    public static class AttributeParser
    {
        public static Dictionary<string, string> ParseAttributes(string attributes)
        {
            if (string.IsNullOrWhiteSpace(attributes))
            {
                throw new ArgumentException($"Invalid attribute input: '{attributes}'. The input is null or whitespace.");
            }

            var pairs = attributes.Split(',')
                .Select(a => a.Trim().Split(':'))
                .ToArray();

            foreach (var pair in pairs)
            {
                if (pair.Length != 2)
                {
                    throw new ArgumentException($"Invalid attribute format in input: '{attributes}'. Each attribute should be of the form 'attribute:value'.");
                }

                if (string.IsNullOrEmpty(pair[0]) || string.IsNullOrEmpty(pair[1]))
                {
                    throw new ArgumentException($"Invalid attribute format in input: '{attributes}'. Neither attribute name nor its value can be empty.");
                }
            }

            return pairs.ToDictionary(k => k[0], v => v[1]);
        }
    }
}
