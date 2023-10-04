using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xingyi.common
{
    public static class SafeHelpers
    {
        public static IEnumerable<T> safeEnumerable<T>(this IEnumerable<T> list)
        {
            return list == null ? new List<T>() : list;
        }
    }
}
