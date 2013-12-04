using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class Utils
    {
        static private readonly Random RandomGenerator = new Random();
        public static double RandomAmount()
        {
            return ((int) (Math.Pow(RandomGenerator.NextDouble(), 4)*100000*100))/100.0;
        }

        public static int RandomStoreNumber()
        {
            return RandomGenerator.Next() % 100;
        }
    }
}
