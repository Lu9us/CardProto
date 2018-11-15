using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Client.System
{
  public static class RandomHelper
    {
        private static Random r = new Random();

        public static int getRandomInt(int min = 0, int max = 1)
        {
            return r.Next(min, max);
        }
        public static double getRandomDouble (int min = 0, int max = 1)
        {
            return r.Next(min, max) + r.NextDouble();
        }


    }
}
