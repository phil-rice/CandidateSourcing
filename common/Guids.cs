using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xingyi.common
{
    using System.Security.Cryptography;
    using System.Text;

    public static class Guids
    {
        public static Guid from(string input)
        {
            if (string.IsNullOrEmpty(input)) return Guid.Empty; // Return a GUID with all zero values.
           
            // Use MD5 to get a 16-byte hash of the string . And... it's OK to use MD5 here even though thats a weak hash. This is just for tests to get repeatable guids
            using (var provider = MD5.Create())
            {
                byte[] inputBytes = Encoding.Default.GetBytes(input);
                byte[] hashBytes = provider.ComputeHash(inputBytes);
                return new Guid(hashBytes);
            }
        }
    }

}
