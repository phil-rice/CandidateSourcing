using System.Collections.Generic;
using System.Linq;

namespace xingyi.common
{
    public static class AttributeParser
    {
        public static Dictionary<string, string> ParseAttributes(string attributes)
        {
            return attributes.Split(',')
                .Select(a => a.Trim().Split(':'))
                .ToDictionary(k => k[0], v => v[1]);
        }
    }
}
