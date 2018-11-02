using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TPM3.zxd.Helper
{
    public static class Extensions
    {
        public static IEnumerable<string> Lines(this StreamReader source)
        {
            if(source == null)
                throw new ArgumentNullException("source");
            string line;
            while ((line = source.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
}
