using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accounting
{
    public static class Extensions
    {
        public static bool HasValue(this string s)
        {
            if (s == null) return false;
            if (s.Length == 0) return false;
            return true;
        }

    }
}