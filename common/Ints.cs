using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xingyi.common
{
    public static class Ints
    {
        public static int safeDiv(int top, int divisor)
        {
            return divisor == 0 ? 0 : top / divisor;
        }
    }
}
