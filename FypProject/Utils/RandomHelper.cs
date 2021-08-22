using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FypProject.Utils
{
    public class RandomHelper
    {
        public static string RandomUniqueString()
        {
            string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(validChars, 16)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
