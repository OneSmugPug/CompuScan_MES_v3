using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuScan_MES_Client
{
    static class StringExtentions
    {
        public static IEnumerable<String> SplitIntoParts(this String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length must be positive.", "partLength");

            for (var i = 0; i < s.Length; i+=partLength)
            {
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
            }
        }
    }
}
